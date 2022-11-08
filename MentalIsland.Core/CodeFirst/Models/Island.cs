using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 岛群
/// </summary>
[SugarTable("Islands")]
[Description("岛群表")]
[SugarIndex("unique_Name_1", nameof(Name), OrderByType.Asc, true)]
public class Island : Entity
{
    /// <summary>
    /// 岛群名称
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 岛群描述
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? Description { get; set; }
    /// <summary>
    /// 岛群QQ群号
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? QQunNumber { get; set; }
    /// <summary>
    /// 岛群QQ群二维码
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? QQunQRCodeFile { get; set; }
    /// <summary>
    /// 岛群人数
    /// </summary>
    public int PersonNumber { get; set; } = 0;
    /// <summary>
    /// 岛群发帖数
    /// </summary>
    public int PostNumber { get; set; } = 0;
    /// <summary>
    /// 岛群最后发帖时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? LastPostTime { get; set; }
    /// <summary>
    /// 岛群最后回复时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? LastReplyTime { get; set; }

    /// <summary>
    /// 已关注岛群用户
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(typeof(User_Island), nameof(User_Island.IslandId), nameof(User_Island.UserId))]
    public virtual List<User> Users { get; set; }

    /// <summary>
    /// 帖子列表
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(Island_Post.IslandId))]
    public virtual List<Island_Post> Posts { get; set; }

}
