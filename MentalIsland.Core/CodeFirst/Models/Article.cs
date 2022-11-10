using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 科普文章
/// </summary>
[SugarTable("Articles")]
[Description("科普文章表")]
public class Article : Entity
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = default!;
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = default!;
    /// <summary>
    /// 图片地址
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? ImageUrl { get; set; } = default!;

    /// <summary>
    /// 文章类型外键
    /// </summary>
    public int ArticleTypeId { get; set; }
}
