namespace MentalIsland.Web.Models.OutPubModels;

/// <summary>
/// 文章列表
/// </summary>
public class ArticleOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 文章标题
    /// </summary>
    public string Title { get; set; } = default!;
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// 文章类型外键
    /// </summary>
    public int ArticleTypeId { get; set; }

    /// <summary>
    /// 文章类型名
    /// </summary>
    public string ArticleTypeName { get; set; } = default!;

    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime CreatedTime { get; set; }
}

/// <summary>
/// 文章类型
/// </summary>
public class ArticleTypeOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 类型名
    /// </summary>
    public string Name { get; set; } = default!;
}