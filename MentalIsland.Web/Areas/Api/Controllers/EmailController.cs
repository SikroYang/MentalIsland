using System.Text;
using Furion;
using MentalIsland.Migrations.Extensions;
using MentalIsland.Web.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 邮件控制器
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[Authorize4MentalIsland]
public class EmailController : WebApiBaseController<EmailController>
{

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 邮件发送服务
    /// </summary>
    public readonly IEmailService EmailService;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [SwaggerHideApiFilter]
    public async Task<string> SendEmail([FromQuery] SendEmailInput sendEmail)
    {
        var Title = string.IsNullOrWhiteSpace(sendEmail.Title) ? "(无标题)" : sendEmail.Title;
        await EmailService.SendAsync(sendEmail.MailAddr, Title, sendEmail.Content, Encoding.UTF8);

        var serverMail = App.GetConfig<string>("MailKitSettings:163:SenderEmail");

        return @$"To: {sendEmail.MailAddr} 发送成功
                From: {serverMail}
                By MentalIsland";
    }
}

