using System.ComponentModel;
using MentalIsland.Core.CodeFirst.Enums;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 用户关注岛群
/// </summary>
[SugarTable("User_Islands")]
[Description("用户关注岛群表")]
public class User_Island
{
    /// <summary>
    /// 用户外键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int UserId { get; set; }

    /// <summary>
    /// 岛群外键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int IslandId { get; set; }

}
