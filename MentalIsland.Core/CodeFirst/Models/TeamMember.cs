using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 团队成员
/// </summary>
[SugarTable("TeamMembers")]
[Description("团队成员表")]
public class TeamMember : Entity
{
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = default!;
    /// <summary>
    /// 照片地址
    /// </summary>
    public string PhotoUrl { get; set; } = default!;

}
