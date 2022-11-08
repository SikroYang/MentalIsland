using System;
using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 帖子信息
/// </summary>
public class PostInput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int? Id { get; set; }
    /// <summary>
    /// 岛群Id
    /// </summary>
    [Required(ErrorMessage = "岛群Id不能为空")]
    [RegularExpression(@"^\d+$", ErrorMessage = "岛群Id只能为数字")]
    public int IslandId { get; set; } = default!;

    /// <summary>
    /// 帖子标题
    /// </summary>
    [Required(ErrorMessage = "帖子标题不能为空")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// 帖子内容
    /// </summary>
    [Required(ErrorMessage = "帖子内容不能为空")]
    public string Content { get; set; } = default!;

}

/// <summary>
/// 帖子信息
/// </summary>
public class PostSearchInput
{
    /// <summary>
    /// 岛群Id
    /// </summary>
    public int IslandId { get; set; } = default!;

    /// <summary>
    /// 帖子名称
    /// </summary>
    public string Title { get; set; } = default!;

}
