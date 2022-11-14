using System.ComponentModel;
using MentalIsland.Core.CodeFirst.Enums;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 用户
/// </summary>
[SugarTable("UserMoonNotes")]
[Description("用户心情和笔记表")]
public class UserMoonNote
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, IsIdentity = true)]
    public virtual int Id { get; set; }
    /// <summary>
    /// 用户外键Id
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// 今日心情
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? Moon { get; set; }
    /// <summary>
    /// 今日笔记
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? Note { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime Today { get; set; } = DateTime.Today;

}
