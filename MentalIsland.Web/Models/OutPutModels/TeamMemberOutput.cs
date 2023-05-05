namespace MentalIsland.Web.Models.OutPubModels;

/// <summary>
/// 团队成员信息
/// </summary>
public class TeamMemberOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = default!;
    /// <summary>
    /// 照片地址
    /// </summary>
    public string PhotoUrl { get; set; } = default!;
}