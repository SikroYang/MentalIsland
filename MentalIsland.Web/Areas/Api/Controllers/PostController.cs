using Furion.FriendlyException;
using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using MentalIsland.Web.Models.OutPubModels;
using MentalIsland.Web.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using MentalIsland.Migrations.Extensions.ControllerEx;
using MentalIsland.Migrations.Extensions.Auth;
using MentalIsland.Web.Models.Extensions;
using Furion.DatabaseAccessor;
using System.Linq;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 帖子管理
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[MentalIslandAuthorize]
public class PostController : WebApiBaseController<PostController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 用户操作仓库
    /// </summary>
    public readonly SqlSugarRepository<User> userRepository;
    /// <summary>
    /// 岛群操作仓库
    /// </summary>
    public readonly SqlSugarRepository<Island> islandRepository;
    /// <summary>
    /// 帖子操作仓库
    /// </summary>
    public readonly SqlSugarRepository<Island_Post> postRepository;
    /// <summary>
    /// 评论操作仓库
    /// </summary>
    public readonly SqlSugarRepository<Island_Reply> replyRepository;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 帖子列表查询
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<PagedList<PostOutput>> List(PostSearchInput searchInfo)
    {
        var exp = new Expressionable<Island_Post>().And(i => !i.IsDeleted);

        if (searchInfo.IslandId > 0)
            exp.And(i => i.IslandId == searchInfo.IslandId);
        if (!string.IsNullOrWhiteSpace(searchInfo.Title))
            exp.And(i => i.Title.Contains(searchInfo.Title));
        var result = postRepository.AsQueryable().Where(exp.ToExpression()).Select<PostOutput>();
        var Total = await result.CountAsync();
        var list = searchInfo.Page > 0 ? await result.ToPageListAsync(searchInfo.Page, searchInfo.Size) : await result.ToListAsync();
        if (searchInfo.Page > 0)
            return new PagedList<PostOutput> { Page = searchInfo.Page, Size = searchInfo.Size, Total = Total, List = list };
        return new PagedList<PostOutput> { Total = Total, List = list };
    }

    /// <summary>
    /// 添加或修改
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [UnitOfWork]
    public async Task<int> AddOrUpdatePost(PostInput post)
    {
        if (post.Title.ContainsKeyWords() || post.Content.ContainsKeyWords()) throw Oops.Bah("您发表的内容包含敏感词").StatusCode();
        if (post.IslandId <= 0) throw Oops.Bah("岛群Id必须大于0").StatusCode();
        var island = await islandRepository.AsQueryable().Includes(l => l.Posts).FirstAsync(wa => wa.Id == post.IslandId && !wa.IsDeleted);
        if (island == null) throw Oops.Bah("当前岛群不存在或已删除").StatusCode();
        bool isSuccess;
        int Id = post.Id ?? 0;
        if (Id == 0)
        {
            var entity = await postRepository.AsInsertable(post.Adapt<Island_Post>()).ExecuteReturnEntityAsync();
            Id = entity.Id;
            isSuccess = Id > 0;
            if (isSuccess)
            {
                isSuccess = await islandRepository.AsUpdateable()
                                    .SetColumns(wa => wa.PostNumber == island.Posts.Count + 1)
                                    .SetColumns(wa => wa.LastPostTime == entity.CreatedTime)
                                    .Where(wa => wa.Id == entity.IslandId)
                                    .ExecuteCommandHasChangeAsync();
            }
        }
        else
        {
            isSuccess = await postRepository.AsUpdateable(post.Adapt<Island_Post>()).IgnoreColumns(true)
                                .IgnoreColumns(l => new { l.CreatedTime, l.LastReplyTime, l.ReplyNumber })
                                .ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 帖子根据Id查询
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<PostReplyOutput> PostById(ReplySearchInput searchInfo)
    {
        if (searchInfo.Id <= 0) throw Oops.Bah("Id必须大于0").StatusCode();
        var result = await postRepository.AsQueryable().Includes(l => l.Replies.Where(wa => !wa.IsDeleted).ToList()).FirstAsync(l => l.Id == searchInfo.Id);
        if (result == null || result.IsDeleted) throw Oops.Bah("该帖子不存在或已删除").StatusCode();
        var data = result.Adapt<PostReplyOutput>();
        var Total = result.Replies.Count;
        if (searchInfo.Page > 0)
        {
            data.Replies.Page = searchInfo.Page;
            data.Replies.Size = searchInfo.Size;
            data.Replies.Total = Total;
            data.Replies.List = data.Replies.List.Skip((searchInfo.Page - 1) * searchInfo.Size).Take(searchInfo.Size).ToList();
        }
        else
            data.Replies.Total = Total;
        return data;
    }

    /// <summary>
    /// 删除帖子
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeletePost(OnlyId model)
    {
        var deletePost = await postRepository.GetByIdAsync(model.Id);
        if (deletePost == null || deletePost.IsDeleted) throw Oops.Bah("该帖子不存在或已删除").StatusCode();
        var isSuccess = await postRepository.RecycleByIdAsync<Island_Post>(deletePost.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }

    /// <summary>
    /// 添加或修改评论
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [UnitOfWork]
    public async Task<int> AddOrUpdateReply(ReplyInput reply)
    {
        if (reply.Content.ContainsKeyWords()) throw Oops.Bah("您发表的内容包含敏感词").StatusCode();
        if (reply.PostId <= 0) throw Oops.Bah("帖子Id必须大于0").StatusCode();
        var post = await postRepository.AsQueryable().Includes(p => p.Replies).FirstAsync(wa => wa.Id == reply.PostId && !wa.IsDeleted);
        if (post == null) throw Oops.Bah("当前帖子不存在或已删除").StatusCode();
        bool isSuccess;
        int Id = reply.Id ?? 0;
        if (Id == 0)
        {
            var entity = await replyRepository.AsInsertable(reply.Adapt<Island_Reply>()).ExecuteReturnEntityAsync();
            Id = entity.Id;
            isSuccess = Id > 0;
            if (isSuccess)
            {
                isSuccess = await postRepository.AsUpdateable()
                                    .SetColumns(wa => wa.LastReplyTime == entity.CreatedTime)
                                    .SetColumns(wa => wa.ReplyNumber == post.Replies.Count + 1)
                                    .Where(wa => wa.Id == post.Id)
                                    .ExecuteCommandHasChangeAsync();

                isSuccess = await islandRepository.AsUpdateable()
                                    .SetColumns(wa => wa.LastReplyTime == entity.CreatedTime)
                                    .Where(l => l.Id == post.IslandId)
                                    .ExecuteCommandHasChangeAsync();
            }
        }
        else
        {
            isSuccess = await replyRepository.AsUpdateable().UpdateColumns(wa => reply).IgnoreColumns(true)
                                .IgnoreColumns(l => new { l.CreatedTime })
                                .ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 删除评论
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeleteReply(OnlyId model)
    {
        var deleteReply = await replyRepository.GetByIdAsync(model.Id);
        if (deleteReply == null || deleteReply.IsDeleted) throw Oops.Bah("该评论不存在或已删除").StatusCode();
        var isSuccess = await replyRepository.RecycleByIdAsync<Island_Reply>(deleteReply.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }

}
