using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 屏蔽词
/// </summary>
public class BlackWordInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int? Id { get; set; }
    /// <summary>
    /// 关键字
    /// </summary>
    [Required(ErrorMessage = "关键字不能为空")]
    public string Keyword { get; set; } = default!;

}