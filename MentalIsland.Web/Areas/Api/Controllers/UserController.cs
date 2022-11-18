using Furion.FriendlyException;
using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using MentalIsland.Web.Models.OutPubModels;
using MentalIsland.Web.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Furion.DataEncryption;
using MentalIsland.Migrations.Extensions.ControllerEx;
using MentalIsland.Migrations.Extensions.Auth;
using MentalIsland.Web.Models.Extensions;
using Furion.DataValidation;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 用户管理
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[MentalIslandAuthorize]
public class UserController : WebApiBaseController<UserController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 用户操作仓库
    /// </summary>
    public readonly SqlSugarRepository<User> userRepository;
    /// <summary>
    /// 用户心情笔记操作仓库
    /// </summary>
    public readonly SqlSugarRepository<UserMoonNote> userMoonNoteRepository;
    /// <summary>
    /// 屏蔽词操作仓库
    /// </summary>
    public readonly SqlSugarRepository<BlackKey> blackkeyRepository;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 用户列表查询
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<PagedList<UserOutput>> List(UserSearchInput searchInfo)
    {
        var exp = new Expressionable<User>().And(u => !u.IsDeleted);
        if (!string.IsNullOrWhiteSpace(searchInfo.IsLocked))
            exp.And(u => u.IsLocked == (searchInfo.IsLocked == "Y"));
        if (!string.IsNullOrWhiteSpace(searchInfo.UserName))
            exp.And(u => u.UserName.Contains(searchInfo.UserName));
        if (!string.IsNullOrWhiteSpace(searchInfo.PhoneNumber))
            exp.And(u => u.PhoneNumber.Contains(searchInfo.PhoneNumber));

        var result = userRepository.AsQueryable().Where(exp.ToExpression());
        var list = searchInfo.Page <= 0 ? await result.ToListAsync() : await result.ToPageListAsync(searchInfo.Page, searchInfo.Size);
        var Total = await result.CountAsync();
        if (searchInfo.Page > 0)
            return new PagedList<UserOutput> { Page = searchInfo.Page, Size = searchInfo.Size, Total = Total, List = list.Adapt<List<UserOutput>>() };
        return new PagedList<UserOutput> { Total = Total, List = list.Adapt<List<UserOutput>>() };
    }

    /// <summary>
    /// 当前登录用户
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
    [HttpPost]
    public async Task<UserOutput> UserDetail(OnlyId model)
    {
        var user = await userRepository.GetByIdAsync(model.Id);
        if (user == null) throw Oops.Bah("该用户不存在!").StatusCode();
        return user.Adapt<UserOutput>();
    }

    /// <summary>
    /// 添加或修改
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddOrUpdateUser(UserInput user)
    {
        if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && !user.PhoneNumber.TryValidate(ValidationTypes.PhoneNumber).IsValid) throw Oops.Bah("不是有效的手机号码格式").StatusCode();
        var userRes = user.Adapt<User>();
        bool isSuccess;
        int Id = user.Id ?? 0;
        if (Id == 0)
        {
            userRes.UserName = string.IsNullOrWhiteSpace(user.PhoneNumber) ? user.Email : user.PhoneNumber;
            var password = user.Password;
            if (string.IsNullOrWhiteSpace(password)) password = "Aa123456";
            userRes.PasswordHash = MD5Encryption.Encrypt(password);
            Id = await userRepository.AsInsertable(userRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            var password = user.Password;
            if (!string.IsNullOrWhiteSpace(password))
                userRes.PasswordHash = MD5Encryption.Encrypt(password);
            isSuccess = await userRepository.AsUpdateable(userRes).IgnoreColumns(true).IgnoreColumns(u => new { u.CreatedTime }).ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 锁定用户
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> LockUser(UserLockInput lockInput)
    {
        var lockUser = await userRepository.GetByIdAsync(lockInput.Id);
        if (lockUser == null || lockUser.IsDeleted) throw Oops.Bah("该用户不存在或已删除").StatusCode();
        if (lockUser.IsLocked == lockInput.IsLocked) throw Oops.Bah("状态错误").StatusCode();
        var status = new { lockUser.Id, lockInput.IsLocked };
        var isSuccess = await userRepository.AsUpdateable(status.Adapt<User>()).IgnoreColumns(true).UpdateColumns(
            x => new { x.IsLocked, x.UpdatedTime, x.UpdatedUserId, x.UpdatedUserName }).ExecuteCommandHasChangeAsync();
        if (!isSuccess) throw Oops.Bah("操作失败,请检查后重新尝试!").StatusCode();
        return lockInput.IsLocked ? "锁定成功!" : "解锁成功!";
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeleteUser(OnlyId model)
    {
        var deleteUser = await userRepository.GetByIdAsync(model.Id);
        if (deleteUser == null || deleteUser.IsDeleted) throw Oops.Bah("该用户不存在或已删除").StatusCode();
        var isSuccess = await userRepository.RecycleByIdAsync<User>(deleteUser.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> ResetPwd(UserResetPwdInput model)
    {
        var user = await userRepository.GetByIdAsync(User.Id);
        if (string.IsNullOrWhiteSpace(model.VerifyCode) && user.UserType < Core.CodeFirst.Enums.UserType.Manager)
            throw Oops.Bah("请输入验证码!").StatusCode();

        if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("VERIFYCODE")) || HttpContext.Session.GetString("VERIFYCODE") != model.VerifyCode)
            throw Oops.Bah("请检查验证码是否正确").StatusCode();
        HttpContext.Session.Remove("VERIFYCODE");

        var resetUser = await userRepository.GetByIdAsync(model.Id);
        if (resetUser == null || resetUser.IsDeleted) throw Oops.Bah("该用户不存在或已删除").StatusCode();
        var PasswordHash = MD5Encryption.Encrypt(string.IsNullOrWhiteSpace(model.NewPassword) ? "Aa123456" : model.NewPassword);
        var status = new { resetUser.Id, PasswordHash };
        var isSuccess = await userRepository.AsUpdateable(status.Adapt<User>()).IgnoreColumns(true).UpdateColumns(
            x => new { x.PasswordHash, x.UpdatedTime, x.UpdatedUserId, x.UpdatedUserName }).ExecuteCommandHasChangeAsync();
        if (!isSuccess) throw Oops.Bah("重置密码失败,请检查后重新尝试!").StatusCode();
        return "重置密码成功!";
    }

    /// <summary>
    /// 获取今日心情和笔记
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<TodayInput> TodayInfo()
    {
        var result = await userMoonNoteRepository.AsQueryable().FirstAsync(wa => wa.UserId == User.Id && wa.Today == DateTime.Today);
        return result.Adapt<TodayInput>() ?? new TodayInput();
    }

    /// <summary>
    /// 修改今日心情和笔记
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> SetTodayInfo(TodayInput model)
    {
        if (model.Note.ContainsKeyWords()) throw Oops.Bah("您发表的内容包含敏感词").StatusCode();
        var result = await userMoonNoteRepository.AsQueryable().FirstAsync(wa => wa.UserId == User.Id && wa.Today == DateTime.Today);
        bool isSuccess;
        if (result == null)
        {
            var umnRes = model.Adapt<UserMoonNote>();
            umnRes.UserId = User?.Id ?? 0;
            var Id = await userMoonNoteRepository.AsInsertable(umnRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            result.Moon = string.IsNullOrWhiteSpace(model.Moon) ? result.Moon : model.Moon;
            result.Note = string.IsNullOrWhiteSpace(model.Note) ? result.Note : model.Note;
            isSuccess = await userMoonNoteRepository.AsUpdateable(result).IgnoreColumns(true).ExecuteCommandHasChangeAsync();
        }
        if (isSuccess) return "修改成功!";
        else throw Oops.Bah("修改失败,请联系管理员").StatusCode();
    }

    /// <summary>
    /// 获取屏蔽词
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<BlackKey>> BlackWord()
    {
        var result = await blackkeyRepository.AsQueryable().ToListAsync();
        return result;
    }

    /// <summary>
    /// 修改屏蔽词
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> SetBlackWord(BlackWordInput model)
    {
        var result = model.Id > 0 ? await blackkeyRepository.AsQueryable().FirstAsync(wa => wa.Id == model.Id) : null;
        bool isSuccess;
        if (result == null)
        {
            var keywords = model.Keyword.Split(new[] { "|", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<BlackKey> list;
            if (keywords.Length > 1)
            {
                list = keywords.Select(wa => new BlackKey { Keyword = wa }).ToList();
            }
            else
            {
                var keys = model.Adapt<BlackKey>();
                list = new List<BlackKey> { keys };
            }
            var Id = await blackkeyRepository.AsInsertable(list).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            var keywords = model.Keyword.Split(new[] { "|", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (keywords.Length > 1)
            {
                throw Oops.Bah("修改模式下不能写入多个关键词").StatusCode();
            }
            result.Keyword = model.Keyword;
            isSuccess = await blackkeyRepository.AsUpdateable(result).IgnoreColumns(true).ExecuteCommandHasChangeAsync();
        }
        if (isSuccess) return "修改成功!";
        else throw Oops.Bah("修改失败,请联系管理员").StatusCode();
    }


    /// <summary>
    /// 删除屏蔽词
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> BlackWordDelete(OnlyId model)
    {
        var deleteKey = await blackkeyRepository.GetByIdAsync(model.Id);
        if (deleteKey == null) throw Oops.Bah("该帖子不存在或已删除").StatusCode();
        var isSuccess = await blackkeyRepository.DeleteByIdAsync(deleteKey.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }
}
