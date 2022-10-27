using System.Text;
using Furion;
using Furion.UnifyResult;
using MentalIsland.Migrations.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace MentalIsland.Web.Controllers;

/// <summary>
/// 首页
/// </summary>
[Route("[controller]")]
[Authorize4MentalIsland]
public class HomeController : WebApiBaseController<HomeController>
{

    #pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 邮件发送服务
    /// </summary>
    public readonly IEmailService EmailService;

    #pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
    /// <summary>
    /// 首页(需认证)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public List<string> Index()
    {
        var list = new List<string> { "心理", "咨询", "心理咨询" };
        return list;
    }

    /// <summary>
    /// 首页
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public List<string> List()
    {
        var list = new List<string> { "心理", "咨询", "心理咨询" };
        return list;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="MailAddr">邮箱地址</param>
    /// <param name="Title">邮件标题</param>
    /// <param name="Content">邮件内容</param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<string> SendEmail(string MailAddr, string Title, string Content)
    {
        await EmailService.SendAsync(MailAddr, Title, Content, Encoding.UTF8);

        return @$"to: {MailAddr} 发送成功
                By MentalIsland";
    }
}

