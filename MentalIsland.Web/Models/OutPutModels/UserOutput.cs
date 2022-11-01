namespace MentalIsland.Web.Models.OutPubModels;

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
    public string NickName { get; set; } = default!;
    /// <summary>
    /// 国家/地区
    /// </summary>
    public string Country { get; set; } = default!;
}