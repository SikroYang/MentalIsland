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
    public async Task<List<PostOutput>> List(PostSearchInput searchInfo)
    {
        var exp = new Expressionable<Island_Post>().And(i => !i.IsDeleted);

        if (searchInfo.IslandId > 0)
            exp.And(i => i.IslandId == searchInfo.IslandId);
        if (!string.IsNullOrWhiteSpace(searchInfo.Title))
            exp.And(i => i.Title.Contains(searchInfo.Title));
        var result = await postRepository.AsQueryable().Includes(l => l.Replies).Where(exp.ToExpression()).ToListAsync();
        return result.Adapt<List<PostOutput>>();
    }

    /// <summary>
    /// 添加或修改
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddOrUpdatePost(PostInput post)
    {
        if (post.IslandId <= 0) Oops.Bah("岛群Id必须大于0").StatusCode();
        var postRes = post.Adapt<Island_Post>();
        bool isSuccess;
        int Id = post.Id ?? 0;
        if (Id == 0)
        {
            Id = await postRepository.AsInsertable(postRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            isSuccess = await postRepository.AsUpdateable(postRes).IgnoreColumns(true).IgnoreColumns(l => new { l.CreatedTime }).ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 帖子根据Id查询
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<PostOutput> PostById(OnlyId model)
    {
        if (model.Id <= 0) throw Oops.Bah("Id必须大于0").StatusCode();
        var result = await postRepository.AsQueryable().Includes(l => l.Replies).FirstAsync(l => l.Id == model.Id);
        if (result == null || result.IsDeleted) throw Oops.Bah("该帖子不存在或已删除").StatusCode();
        return result.Adapt<PostOutput>();
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
    public async Task<int> AddOrUpdateReply(ReplyInput reply)
    {
        if (reply.PostId <= 0) Oops.Bah("帖子Id必须大于0").StatusCode();
        var postRes = reply.Adapt<Island_Reply>();
        bool isSuccess;
        int Id = reply.Id ?? 0;
        if (Id == 0)
        {
            Id = await replyRepository.AsInsertable(postRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            isSuccess = await replyRepository.AsUpdateable(postRes).IgnoreColumns(true).IgnoreColumns(l => new { l.CreatedTime }).ExecuteCommandHasChangeAsync();
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
