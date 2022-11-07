using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 岛群帖子
/// </summary>
[SugarTable("Island_Posts")]
[Description("帖子表")]
public class Island_Post : Entity
{
    /// <summary>
    /// 帖子标题
    /// </summary>
    public string Title { get; set; } = default!;
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = default!;
    /// <summary>
    /// 回复数
    /// </summary>
    public int ReplyNumber { get; set; }
    /// <summary>
    /// 最后回复时间
    /// </summary>
    public DateTime LastReplyTime { get; set; }
    /// <summary>
    /// 岛群Id
    /// </summary>
    public int IslandId { get; set; }

    /// <summary>
    /// 岛群导航属性
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(IslandId))]
    public virtual Island Island { get; set; }

}
