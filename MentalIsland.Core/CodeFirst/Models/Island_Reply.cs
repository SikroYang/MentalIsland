using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 岛群帖子回复
/// </summary>
[SugarTable("Island_Replies")]
[Description("回复表")]
public class Island_Reply : Entity
{
    /// <summary>
    /// 回复内容
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// 帖子Id
    /// </summary>
    public int PostId { get; set; }

}
