using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 团队成员信息
/// </summary>
public class TeamMemberInput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int? Id { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    [Required(ErrorMessage = "姓名不能为空")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = default!;
    /// <summary>
    /// 照片地址
    /// </summary>
    [Required(ErrorMessage = "照片不能为空")]
    public string PhotoUrl { get; set; } = default!;
}
