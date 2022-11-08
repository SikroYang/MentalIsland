using System;
using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 评论信息
/// </summary>
public class ReplyInput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 帖子Id
    /// </summary>
    [Required(ErrorMessage = "帖子Id不能为空")]
    [RegularExpression(@"^\d+$", ErrorMessage = "帖子Id只能为数字")]
    public int PostId { get; set; } = default!;

    /// <summary>
    /// 评论内容
    /// </summary>
    [Required(ErrorMessage = "评论内容不能为空")]
    public string Content { get; set; } = default!;

}
