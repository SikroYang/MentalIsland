using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 科普文章
/// </summary>
public class ArticleInput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage = "文章标题不能为空")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// 文章内容
    /// </summary>
    [Required(ErrorMessage = "文章内容不能为空")]
    public string Content { get; set; } = default!;
    /// <summary>
    /// 图片地址
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 类型Id
    /// </summary>
    [Required(ErrorMessage = "类型Id不能为空")]
    [RegularExpression(@"^\d+$", ErrorMessage = "类型Id只能为数字")]
    public int ArticleTypeId { get; set; } = default!;

}

/// <summary>
/// 查询文章信息
/// </summary>
public class ArticleSearchInput : Paged
{
    /// <summary>
    /// 类型Id
    /// </summary>
    public int ArticleTypeId { get; set; } = default!;
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = default!;
    // /// <summary>
    // /// 文章内容
    // /// </summary>
    // public string Content { get; set; } = default!;

}
