using Furion.FriendlyException;
using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using MentalIsland.Migrations.Extensions;
using MentalIsland.Web.Models.OutPubModels;
using MentalIsland.Web.Models.InputModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

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
    public readonly SqlSugarRepository<User> userRepository;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<UserOutput> Login([Required] UserLoginInput userLogin)
    {
        if (User?.Identity != null && User.Identity.IsAuthenticated) throw Oops.Bah("请勿重复登录!").StatusCode(500);
        var passwordHash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password))).Replace("-", "").ToLower();

        var user = await userRepository.GetFirstAsync(u => u.UserName == userLogin.UserName && u.PasswordHash == passwordHash);
        // var user = new User() { Id = 1L, UserName = UserName, PasswordHash = passwordHash };

        if (user == null) throw Oops.Bah("账号密码错误或该用户不存在").StatusCode(1001);

        await SignInManager.SignInAsync(HttpContext, user);

        return user.Adapt<UserOutput>();
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

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserOutput> UserInfo()
    {
        var user = await userRepository.GetByIdAsync(User.Id);
        return user.Adapt<UserOutput>();
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("[action]/{Id?}")]
    public async Task<UserOutput> UserInfo(int? Id)
    {
        if (!Id.HasValue) throw Oops.Bah("Id不能为空").StatusCode();
        var user = await userRepository.GetByIdAsync(Id);
        if (user == null) throw Oops.Bah("该用户不存在!").StatusCode();
        return user.Adapt<UserOutput>();
    }
}
