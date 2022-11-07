using Furion.FriendlyException;
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

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 岛群管理
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[Authorize4MentalIsland]
public class IslandController : WebApiBaseController<IslandController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

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
    public async Task<List<UserOutput>> List(UserSearchInput searchInfo)
    {
        var exp = new Expressionable<Island>().And(i => !i.IsDeleted);
        if (!string.IsNullOrWhiteSpace(searchInfo.UserName))
            exp.And(i => i.CreatedUserName.Contains(searchInfo.UserName));
        if (!string.IsNullOrWhiteSpace(searchInfo.UserName))
            exp.And(i => i.Name.Contains(searchInfo.UserName));
        if (!string.IsNullOrWhiteSpace(searchInfo.PhoneNumber))
            exp.And(i => i.Description.Contains(searchInfo.PhoneNumber));
        var result = await islandRepository.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return result.Adapt<List<UserOutput>>();
    }

    /// <summary>
    /// 添加或修改
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddOrUpdateUser(UserInput user)
    {
        var userRes = user.Adapt<Island>();
        bool isSuccess;
        int Id = user.Id ?? 0;
        if (Id == 0)
        {
            Id = await islandRepository.AsInsertable(userRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            isSuccess = await islandRepository.AsUpdateable(userRes).IgnoreColumns(true).IgnoreColumns(u => new { u.CreatedTime }).ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 删除岛屿
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeleteUser(OnlyId model)
    {
        var deleteUser = await islandRepository.GetByIdAsync(model.Id);
        if (deleteUser == null || deleteUser.IsDeleted) throw Oops.Bah("该用户不存在或已删除").StatusCode();
        var isSuccess = await islandRepository.RecycleByIdAsync<Island>(deleteUser.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }
}
