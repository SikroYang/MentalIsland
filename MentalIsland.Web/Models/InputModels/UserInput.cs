using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 用户信息
/// </summary>
public class UserInput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int? Id { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = default!;
    /// <summary>
    /// 电子邮箱
    /// </summary>
    [Required(ErrorMessage = "电子邮箱不能为空")]
    [EmailAddress(ErrorMessage = "不是有效的电子邮箱")]
    public string Email { get; set; } = default!;
    /// <summary>
    /// 手机号
    /// </summary>
    [Required(ErrorMessage = "手机号码不能为空")]
    [Phone(ErrorMessage = "不是有效的手机号码格式")]
    public string PhoneNumber { get; set; } = default!;
    /// <summary>
    /// 昵称
    /// </summary>
    [Required(ErrorMessage = "昵称不能为空")]
    public string FullName { get; set; } = default!;
    /// <summary>
    /// 国家/地区
    /// </summary>
    [Required(ErrorMessage = "国家或地区不能为空")]
    public string Country { get; set; } = default!;

}

/// <summary>
/// 注册用户信息
/// </summary>
public class UserRegistryInput : UserInput
{
    /// <summary>
    /// 验证码
    /// </summary>
    [Required(ErrorMessage = "验证码不能为空")]
    [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "验证码必须是6位数字")]
    public string VerifyCode { get; set; } = default!;

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    [RegularExpression(@"^[A-Za-z]\w{5,17}$", ErrorMessage = "密码必须以字母开头，长度在6~18之间，只能包含字母、数字和下划线")]
    public new string Password { get; set; } = default!;
}