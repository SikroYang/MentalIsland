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
using System.ComponentModel.DataAnnotations;
using Furion.DataEncryption;
using NETCore.MailKit.Core;
using System.Text;
using MentalIsland.Migrations.Extensions.ControllerEx;
using MentalIsland.Migrations.Extensions.Auth;
using Furion.DataValidation;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 注册授权管理
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[AllowAnonymous]
public class AuthController : WebApiBaseController<AuthController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 用户操作仓库
    /// </summary>
    public readonly SqlSugarRepository<User> userRepository;

    /// <summary>
    /// 邮件发送服务
    /// </summary>
    public readonly IEmailService EmailService;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 发送验证码到邮箱
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<string> VerifyCode(
        [Required(ErrorMessage = "电子邮箱不能为空")]
        [EmailAddress(ErrorMessage = "不是有效的电子邮箱")]
        string MailAddr
    )
    {
        var code = Random.Shared.Next(1000, 999999).ToString().PadLeft(6, '0');
        HttpContext.Session.SetString("VERIFYCODE", code);
        var msg = $"您的验证码为{code}";
        var Title = "MentalIsland";
        await EmailService.SendAsync(MailAddr, Title, msg, Encoding.UTF8);
        return code;//"发送成功!";
    }

    /// <summary>
    /// 用户注册
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Register(UserRegistryInput user)
    {
        if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("VERIFYCODE")) || HttpContext.Session.GetString("VERIFYCODE") != user.VerifyCode)
            throw Oops.Bah("请检查验证码是否正确").StatusCode();
        HttpContext.Session.Remove("VERIFYCODE");

        if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && !user.PhoneNumber.TryValidate(ValidationTypes.PhoneNumber).IsValid) throw Oops.Bah("不是有效的手机号码格式").StatusCode();

        var userRes = user.Adapt<User>();
        userRes.UserName = string.IsNullOrWhiteSpace(user.PhoneNumber) ? user.Email : user.PhoneNumber;
        bool isSuccess;
        user.Id = 0;
        userRes.PasswordHash = MD5Encryption.Encrypt(user.Password);
        user.Id = await userRepository.AsInsertable(userRes).ExecuteReturnIdentityAsync();
        isSuccess = user.Id > 0;

        if (!isSuccess) throw Oops.Bah("注册失败,请联系管理员!").StatusCode();
        return user.Id.Value;
    }


    /// <summary>
    /// 忘记密码
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> ForgetPwd(UserForgetPwdInput model)
    {
        var user = await userRepository.AsQueryable().FirstAsync(u => u.Email == model.Email);
        if (user == null || user.IsDeleted) throw Oops.Bah("该用户不存在或已删除").StatusCode();

        if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("VERIFYCODE")) || HttpContext.Session.GetString("VERIFYCODE") != model.VerifyCode)
            throw Oops.Bah("请检查验证码是否正确").StatusCode();
        HttpContext.Session.Remove("VERIFYCODE");

        var PasswordHash = MD5Encryption.Encrypt(string.IsNullOrWhiteSpace(model.NewPassword) ? "Aa123456" : model.NewPassword);
        var status = new { user.Id, PasswordHash };
        var isSuccess = await userRepository.AsUpdateable(status.Adapt<User>()).IgnoreColumns(true).UpdateColumns(
            x => new { x.PasswordHash, x.UpdatedTime, x.UpdatedUserId, x.UpdatedUserName }).ExecuteCommandHasChangeAsync();
        if (!isSuccess) throw Oops.Bah("重置密码失败,请检查后重新尝试!").StatusCode();
        return "重置密码成功!";
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserOutput> Login(UserLoginInput userLogin)
    {
        if (User?.Identity != null && User.Identity.IsAuthenticated) throw Oops.Bah("请勿重复登录!").StatusCode(500);
        var passwordHash = MD5Encryption.Encrypt(userLogin.Password);

        var user = await userRepository.GetFirstAsync(u => (u.UserName == userLogin.UserName || u.PhoneNumber == userLogin.UserName || u.Email == userLogin.UserName) && u.PasswordHash == passwordHash);

        if (user == null) throw Oops.Bah("账号密码错误或该用户不存在").StatusCode(1001);

        await SignInManager.SignInAsync(HttpContext, user);

        return user.Adapt<UserOutput>();
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [MentalIslandAuthorize]
    public async Task<string> Logout()
    {
        await HttpContext.SignOutAsync();
        return "退出登录成功!";
    }
}
