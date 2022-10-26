using Furion.DatabaseAccessor;

namespace MentalIsland.Core.CodeFirst.Identity.Models;

/// <summary>
/// 用户
/// </summary>
public class User : Entity<long>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = default!;
    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = default!;
    /// <summary>
    /// 手机号
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = default!;
    /// <summary>
    /// 密码Hash
    /// </summary>
    public string PasswordHash { get; set; } = default!;
    /// <summary>
    /// 国家/地区
    /// </summary>
    public string Country { get; set; } = default!;

    /// <summary>
    /// 默认值
    /// </summary>
    public User()
    {
        CreatedTime = DateTimeOffset.Now;
    }
}
