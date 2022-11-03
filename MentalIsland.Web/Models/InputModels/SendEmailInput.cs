using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 发送邮件
/// </summary>
public class SendEmailInput
{
    /// <summary>
    /// 邮箱地址
    /// </summary>
    [Required(ErrorMessage = "电子邮箱不能为空")]
    [EmailAddress(ErrorMessage = "不是有效的电子邮箱")]
    public string MailAddr { get; set; } = default!;
    /// <summary>
    /// 邮件标题
    /// </summary>
    public string? Title { get; set; } = default!;

    /// <summary>
    /// 邮件内容
    /// </summary>
    [Required(ErrorMessage = "电子邮件内容不能为空")]
    [MinLength(6, ErrorMessage = "内容不得少于6个字符")]
    public string Content { get; set; } = default!;
}