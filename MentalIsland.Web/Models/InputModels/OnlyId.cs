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
    [RegularExpression(@"^\d+$", ErrorMessage = "Id只能为数字")]
    public int? Id { get; set; }

}

/// <summary>
/// Id和Name
/// </summary>
public class IdAndName : OnlyId
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空")]
    public string Name { get; set; } = default!;

}



/// <summary>
/// Id或Name
/// </summary>
public class IdOrName
{

    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = default!;
}
