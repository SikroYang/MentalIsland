using Furion.FriendlyException;
using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using MentalIsland.Migrations.Extensions.Auth;
using MentalIsland.Migrations.Extensions.ControllerEx;
using MentalIsland.Web.Models.InputModels;
using MentalIsland.Web.Models.OutPubModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 科普文章
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[MentalIslandAuthorize]
public class TeamMemberController : WebApiBaseController<TeamMemberController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 团队成员仓库
    /// </summary>
    public readonly SqlSugarRepository<TeamMember> teamMemberRepository;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 团队成员列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<PagedList<TeamMemberOutput>> List(Paged page)
    {
        var result = teamMemberRepository.AsQueryable();

        var Total = await result.CountAsync();
        var list = page.Page > 0 ? await result.ToPageListAsync(page.Page, page.Size) : await result.ToListAsync();
        if (page.Page > 0)
            return new PagedList<TeamMemberOutput> { Page = page.Page, Size = page.Size, Total = Total, List = list.Adapt<List<TeamMemberOutput>>() };
        return new PagedList<TeamMemberOutput> { Total = Total, List = list.Adapt<List<TeamMemberOutput>>() };
    }

    /// <summary>
    /// 添加或修改团队成员
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddOrUpdate(TeamMemberInput teamMember)
    {
        var teamMemberRes = teamMember.Adapt<TeamMember>();
        bool isSuccess;
        int Id = teamMember.Id ?? 0;
        if (Id == 0)
        {
            Id = await teamMemberRepository.AsInsertable(teamMemberRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            isSuccess = await teamMemberRepository.AsUpdateable(teamMemberRes).IgnoreColumns(true).IgnoreColumns(l => new { l.CreatedTime }).ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 根据Id获取团队成员
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<TeamMemberOutput> TeamMemberById(OnlyId model)
    {
        var teamMember = await teamMemberRepository.GetByIdAsync(model.Id);
        if (teamMember == null || teamMember.IsDeleted) throw Oops.Bah("该团队成员不存在或已删除").StatusCode();

        return teamMember.Adapt<TeamMemberOutput>();
    }

    /// <summary>
    /// 删除团队成员
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeleteTeamMember(OnlyId model)
    {
        var deleteTeamMember = await teamMemberRepository.GetByIdAsync(model.Id);
        if (deleteTeamMember == null || deleteTeamMember.IsDeleted) throw Oops.Bah("该团队成员不存在或已删除").StatusCode();
        var isSuccess = await teamMemberRepository.RecycleByIdAsync<TeamMember>(deleteTeamMember.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }

}
