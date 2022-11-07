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
    /// <summary>
    /// 岛群描述
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 岛群QQ群号
    /// </summary>
    public string? QQunNumber { get; set; }
    /// <summary>
    /// 岛群QQ群二维码
    /// </summary>
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
    public DateTime? LastPostTime { get; set; }
    /// <summary>
    /// 岛群最后回复时间
    /// </summary>
    public DateTime? LastReplyTime { get; set; }

}
