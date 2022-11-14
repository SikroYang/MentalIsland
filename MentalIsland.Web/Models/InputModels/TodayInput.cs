using System.ComponentModel.DataAnnotations;

namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 今日心情和笔记
/// </summary>
public class TodayInput
{
    /// <summary>
    /// 今日心情
    /// </summary>
    public string Moon { get; set; } = default!;
    /// <summary>
    /// 今日笔记
    /// </summary>
    public string Note { get; set; } = default!;

}