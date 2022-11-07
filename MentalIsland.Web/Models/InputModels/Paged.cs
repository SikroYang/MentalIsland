namespace MentalIsland.Web.Models.InputModels;

/// <summary>
/// 分页
/// </summary>
public class Paged
{
    /// <summary>
    /// 当前页
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// 返回条数
    /// </summary>
    public int Size { get; set; } = 10;
}