using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// Id
/// </summary>
public class OnlyId
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "Id不能为空")]
    public int? Id { get; set; }

}
