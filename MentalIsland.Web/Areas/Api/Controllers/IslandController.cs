﻿using Furion.FriendlyException;
using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using MentalIsland.Migrations.Extensions;
using MentalIsland.Web.Models.OutPubModels;
using MentalIsland.Web.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;

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
        var result = await islandRepository.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return result.Adapt<List<IslandOutput>>();
    }

    /// <summary>
    /// 添加或修改
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddOrUpdateIsland(IslandInput island)
    {
        var islandRes = island.Adapt<Island>();
        bool isSuccess;
        int Id = island.Id ?? 0;
        if (Id == 0)
        {
            Id = await islandRepository.AsInsertable(islandRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            isSuccess = await islandRepository.AsUpdateable(islandRes).IgnoreColumns(true).IgnoreColumns(l => new { l.CreatedTime }).ExecuteCommandHasChangeAsync();
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
        if (deleteIsland == null || deleteIsland.IsDeleted) throw Oops.Bah("该岛屿不存在或已删除").StatusCode();
        var isSuccess = await islandRepository.RecycleByIdAsync<Island>(deleteIsland.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }

    /// <summary>
    /// 关注岛屿
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> FollowIsland(OnlyId model)
    {
        var island = await islandRepository.GetByIdAsync(model.Id);
        if (island == null || island.IsDeleted) throw Oops.Bah("该岛屿不存在或已删除").StatusCode();

        var list = new List<Island>{
            new Island{
                Id = island.Id,
                Users= new List<User> { new User { Id = Convert.ToInt32(User.Id) } }
            }
        };

        //默认模式：只更新关系表 （删除添加）
        var isSuccess = await islandRepository.Context.UpdateNav(list)
                        .Include(l => l.Users)
                        .ExecuteCommandAsync();//技巧：只更新中间表可以只传A和B表的主键其他不用赋值

        // var isSuccess = await islandRepository.RecycleByIdAsync<Island>(island.Id);
        if (!isSuccess) throw Oops.Bah("关注失败,请检查后重新尝试!").StatusCode();
        return "关注成功!";
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

        return user.Islands.Adapt<List<IslandOutput>>();
    }
}
