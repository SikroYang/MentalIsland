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
using System.Security.Claims;
using Furion;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 岛群管理
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[MentalIslandAuthorize]
public class IslandController : WebApiBaseController<IslandController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 岛群操作仓库
    /// </summary>
    public readonly SqlSugarRepository<User> userRepository;
    /// <summary>
    /// 岛群操作仓库
    /// </summary>
    public readonly SqlSugarRepository<Island> islandRepository;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 岛群列表查询
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<IslandOutput>> List(IslandSearchInput searchInfo)
    {
        var exp = new Expressionable<Island>().And(i => !i.IsDeleted);
        if (!string.IsNullOrWhiteSpace(searchInfo.Name))
            exp.And(i => i.Name.Contains(searchInfo.Name));
        if (!string.IsNullOrWhiteSpace(searchInfo.Description))
            exp.And(i => i.Description.Contains(searchInfo.Description));
        var result = await islandRepository.AsQueryable().Where(exp.ToExpression()).Includes(wa => wa.Users).ToListAsync();
        var userId = App.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var data = result.Select(wa =>
        {
            var o = wa.Adapt<IslandOutput>();
            o.IsFollow = wa.Users.Any(u => u.Id.ToString() == userId);
            return o;
        }).ToList();
        return data;
    }

    /// <summary>
    /// 添加或修改
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [UnitOfWork]
    public async Task<int> AddOrUpdateIsland(IslandInput island)
    {
        if (island.Name.ContainsKeyWords() || island.Description.ContainsKeyWords()) throw Oops.Bah("您发表的内容包含敏感词").StatusCode();
        var islandRes = await islandRepository.AsQueryable().Includes(l => l.Posts).FirstAsync(wa => wa.Id == island.Id && !wa.IsDeleted);
        if (island == null) throw Oops.Bah("当前岛群不存在或已删除").StatusCode();
        bool isSuccess;
        int Id = island.Id ?? 0;
        if (Id == 0)
        {
            var islandRep = island.Adapt<Island>();
            islandRep.PersonNumber = 1;
            Id = await islandRepository.AsInsertable(islandRep).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;

            var opIsland = new Island
            {
                Id = Id,
                Users = new List<User> { new User { Id = Convert.ToInt32(User.Id) } }
            };

            //默认模式：只更新关系表 （删除添加）
            isSuccess = await islandRepository.Context.UpdateNav(opIsland)
                            .Include(l => l.Users)
                            .ExecuteCommandAsync();//技巧：只更新中间表可以只传A和B表的主键其他不用赋值
        }
        else
        {
            isSuccess = await islandRepository.AsUpdateable(island.Adapt<Island>()).IgnoreColumns(true)
                                .IgnoreColumns(l => new { l.CreatedTime, l.LastReplyTime, l.PostNumber, l.LastPostTime, l.PersonNumber })
                                .ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 删除岛屿
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeleteIsland(OnlyId model)
    {
        var deleteIsland = await islandRepository.GetByIdAsync(model.Id);
        if (deleteIsland == null || deleteIsland.IsDeleted) throw Oops.Bah("该岛群不存在或已删除").StatusCode();
        var isSuccess = await islandRepository.RecycleByIdAsync<Island>(deleteIsland.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }

    /// <summary>
    /// 关注岛群
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> FollowIsland(OnlyId model)
    {
        var island = await islandRepository.GetByIdAsync(model.Id);
        if (island == null || island.IsDeleted) throw Oops.Bah("该岛群不存在或已删除").StatusCode();

        var opIsland = new Island
        {
            Id = island.Id,
            Users = new List<User> { new User { Id = Convert.ToInt32(User.Id) } }
        };

        //默认模式：只更新关系表 （删除添加）
        var isSuccess = await islandRepository.Context.UpdateNav(opIsland)
                        .Include(l => l.Users)
                        .ExecuteCommandAsync();//技巧：只更新中间表可以只传A和B表的主键其他不用赋值

        isSuccess = await islandRepository.AsUpdateable()
                            .SetColumns(wa => wa.PersonNumber == wa.PersonNumber + 1)
                            .Where(l => l.Id == island.Id)
                            .ExecuteCommandHasChangeAsync();

        // var isSuccess = await islandRepository.RecycleByIdAsync<Island>(island.Id);
        if (!isSuccess) throw Oops.Bah("关注失败,请检查后重新尝试!").StatusCode();
        return "关注成功!";
    }

    /// <summary>
    /// 取消关注岛群
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> CancelFollowIsland(OnlyId model)
    {
        var island = await islandRepository.AsQueryable().Includes(wa => wa.Users).FirstAsync(l => l.Id == Convert.ToInt32(model.Id));
        if (island == null || island.IsDeleted) throw Oops.Bah("该岛群不存在或已删除").StatusCode();

        var isFollow = island.Users.Any(u => u.Id == User.Id);
        if (!isFollow) throw Oops.Bah("您未关注当前岛群").StatusCode();

        var opIsland = new Island
        {
            Id = island.Id,
            Users = new List<User> { new User { Id = Convert.ToInt32(User.Id) } }
        };

        var isSuccess = await islandRepository.Context.DeleteNav(opIsland)
                        .Include(l => l.Users)
                        .ExecuteCommandAsync();

        isSuccess = await islandRepository.AsUpdateable()
                            .SetColumns(wa => wa.PersonNumber == wa.PersonNumber - 1)
                            .Where(l => l.Id == island.Id)
                            .ExecuteCommandHasChangeAsync();

        // var isSuccess = await islandRepository.RecycleByIdAsync<Island>(island.Id);
        if (!isSuccess) throw Oops.Bah("取消关注失败,请检查后重新尝试!").StatusCode();
        return "取消关注成功!";
    }

    /// <summary>
    /// 获取用户关注的岛屿
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<IslandOutput>> GetUserFollowIsland()
    {
        var user = await userRepository.AsQueryable().Includes(wa => wa.Islands).FirstAsync(wa => wa.Id == User.Id);
        if (user == null || user.IsDeleted) throw Oops.Bah("该用户不存在或已删除").StatusCode();

        return user.Islands.Where(wa => !wa.IsDeleted).Adapt<List<IslandOutput>>();
    }

    /// <summary>
    /// 获取用户创建的岛屿
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<IslandOutput>> GetUserCreatedIsland()
    {
        var list = await islandRepository.AsQueryable().Where(wa => !wa.IsDeleted && wa.CreatedUserId == User.Id).ToListAsync();
        return list.Adapt<List<IslandOutput>>() ?? new List<IslandOutput>();
    }
}
