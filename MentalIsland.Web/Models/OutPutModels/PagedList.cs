using System.Collections.Generic;
namespace MentalIsland.Web.Models.OutPubModels;

/// <summary>
/// 分页
/// </summary>
public class PagedList<T>
{
    /// <summary>
    /// 当前页
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// 返回条数
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// 总条数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 数据列表
    /// </summary>
    public List<T> List { get; set; } = default!;
}