namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 查询用户信息
/// </summary>
public class UserSearchInput
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = default!;
    /// <summary>
    /// 用户昵称
    /// </summary>
    public string FullName { get; set; } = default!;
    /// <summary>
    /// 手机号
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    /// <summary>
    /// 是否锁定
    /// Y.是
    /// N.否
    /// </summary>
    public string IsLocked { get; set; } = default!;
}