using System.ComponentModel;
using MentalIsland.Core.CodeFirst.Enums;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// 用户
/// </summary>
[SugarTable("Users")]
[Description("用户表")]
public class User : Entity
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
    public string FullName { get; set; } = default!;
    /// <summary>
    /// 密码Hash
    /// </summary>
    public string PasswordHash { get; set; } = default!;
    /// <summary>
    /// 国家/地区
    /// </summary>
    public string Country { get; set; } = default!;
    /// <summary>
    /// 是否锁定用户
    /// </summary>
    public bool IsLocked { get; set; } = false;

    /// <summary>
    /// 用户类型
    /// </summary>
    public UserType UserType { get; set; } = UserType.Normal;

    /// <summary>
    /// 用户个性化注册类型
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public Personal? Personal { get; set; }

    /// <summary>
    /// 用户个性化备注
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public UserComment? UserComment { get; set; }

    /// <summary>
    /// 用户已关注岛群
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(typeof(User_Island), nameof(User_Island.UserId), nameof(User_Island.IslandId))]
    public virtual List<Island> Islands { get; set; }

    /// <summary>
    /// 用户已关注岛群
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(UserMoonNote.UserId))]
    public virtual List<UserMoonNote> MoodNotes { get; set; }

}
