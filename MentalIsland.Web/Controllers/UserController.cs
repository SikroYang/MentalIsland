using Furion.FriendlyException;
using MentalIsland.Core.CodeFirst.Identity;
using MentalIsland.Core.CodeFirst.Identity.Models;
using MentalIsland.Migrations.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MentalIsland.Web.Controllers;

/// <summary>
/// 首页
/// </summary>
[Route("[controller]")]
[Authorize4MentalIsland]
public class UserController : WebApiBaseController<UserController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 用户操作仓库
    /// </summary>
    public readonly ISqlSugarClient client;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<User> Login(string UserName, string Password)
    {
        if (User?.Identity != null && User.Identity.IsAuthenticated) throw Oops.Bah("请勿重复登录!").StatusCode(500);
        var passwordHash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password))).Replace("-", "").ToLower();

        // client.Queryable<User>()
        var user = new User() { Id = 1L, UserName = UserName, PasswordHash = passwordHash };

        await SignInManager.SignInAsync(HttpContext, user);

        return user;
    }

    /// <summary>
    /// 首页
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public List<string> List()
    {
        var list = new List<string> { "心理", "咨询", "心理咨询", User.FindFirstValue(ClaimTypes.NameIdentifier), User.FindFirstValue(ClaimTypes.Name) };
        return list;
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> Logout()
    {
        await HttpContext.SignOutAsync();
        return "退出登录成功!";
    }
}
