using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 岛群信息
/// </summary>
public class IslandInput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 岛群名称
    /// </summary>
    [Required(ErrorMessage = "岛群名称不能为空")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 岛群描述
    /// </summary>
    public string Description { get; set; } = default!;
    /// <summary>
    /// 岛群图标
    /// </summary>
    public string? IslandIcon { get; set; }
    /// <summary>
    /// 岛群QQ群号
    /// </summary>
    public string QQunNumber { get; set; } = default!;
    /// <summary>
    /// 岛群QQ群二维码
    /// </summary>
    public string QQunQRCodeFile { get; set; } = default!;

}

/// <summary>
/// 岛群信息
/// </summary>
public class IslandSearchInput : Paged
{

    /// <summary>
    /// 岛群名称
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 岛群描述
    /// </summary>
    public string Description { get; set; } = default!;

}
