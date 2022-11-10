using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 科普文章类型
/// </summary>
[SugarTable("ArticleTypes")]
[Description("文章类型表")]
public class ArticleType
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 类型名
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 该类别下文章
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(Article.ArticleTypeId))]
    public virtual List<Article> Articles { get; set; }

    /// <summary>
    /// 逻辑删除
    /// </summary>
    public virtual bool IsDeleted { get; set; } = false;
}
