using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// Id
/// </summary>
public class OnlyId
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "Id不能为空")]
    public int? Id { get; set; }

}

/// <summary>
/// 用户锁定请求
/// </summary>
public class UserLockInput : OnlyId
{
    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool IsLocked { get; set; }
}


/// <summary>
/// 重置密码请求
/// </summary>
public class UserResetPwdInput : OnlyId
{
    /// <summary>
    /// 验证码
    /// </summary>
    public string VerifyCode { get; set; } = default!;
}
