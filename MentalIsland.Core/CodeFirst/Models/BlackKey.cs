using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 屏蔽词
/// </summary>
[SugarTable("BlackKeys")]
[Description("屏蔽词表")]
public class BlackKey
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, IsIdentity = true)]
    public virtual int Id { get; set; }

    /// <summary>
    /// 屏蔽词
    /// </summary>
    public string Keyword { get; set; } = default!;
}
