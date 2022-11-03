namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 用户登录
/// </summary>
public class UserLoginInput
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = default!;
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = default!;
}