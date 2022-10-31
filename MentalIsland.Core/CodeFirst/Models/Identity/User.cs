using System.ComponentModel;
using System.Data;
using MentalIsland.Core.CodeFirst.SqlSugarHelper;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models.Identity;

/// <summary>
/// 用户
/// </summary>
[SugarTable("Users")]
[Description("用户表")]
public class User : Entity<int>
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

}
