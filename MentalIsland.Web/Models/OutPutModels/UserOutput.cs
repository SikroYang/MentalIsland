using MentalIsland.Core.CodeFirst.Enums;

namespace MentalIsland.Web.Models.OutPubModels;

/// <summary>
/// 用户信息
/// </summary>
public class UserOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; } = default!;
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
    /// 国家/地区
    /// </summary>
    public string Country { get; set; } = default!;
    /// <summary>
    /// 用户头像
    /// </summary>
    public string HeadImage { get; set; } = default!;
    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool IsLocked { get; set; } = default!;

    /// <summary>
    /// 用户个性化注册类型
    /// </summary>
    public Personal Personal { get; set; }

    /// <summary>
    /// 用户个性化备注
    /// </summary>
    public List<UserComment> UserComment { get; set; } = default!;
}