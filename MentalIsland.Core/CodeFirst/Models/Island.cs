using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 岛群
/// </summary>
[SugarTable("Islands")]
[Description("岛群表")]
public class Island : Entity
{
    /// <summary>
    /// 岛群名称
    /// </summary>
    public string Name { get; set; } = default!;

}
