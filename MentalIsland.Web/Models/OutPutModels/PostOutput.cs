namespace MentalIsland.Web.Models.OutPubModels;

/// <summary>
/// 岛群信息
/// </summary>
public class PostOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 帖子标题
    /// </summary>
    public string Title { get; set; } = default!;
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = default!;
    /// <summary>
    /// 回复数
    /// </summary>
    public int ReplyNumber { get; set; }
    /// <summary>
    /// 最后回复时间
    /// </summary>
    public DateTime LastReplyTime { get; set; }
    /// <summary>
    /// 岛群Id
    /// </summary>
    public int IslandId { get; set; }

    /// <summary>
    /// 评论列表
    /// </summary>
    public virtual List<ReplyOutput> Replies { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    public virtual long? CreatedUserId { get; set; }

    /// <summary>
    /// 创建者名称
    /// </summary>
    public virtual string CreatedUserName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime CreatedTime { get; set; }
}


/// <summary>
/// 岛群信息
/// </summary>
public class ReplyOutput
{
    /// <summary>
    /// 评论Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 回复内容
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// 帖子Id
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    public virtual long? CreatedUserId { get; set; }

    /// <summary>
    /// 创建者名称
    /// </summary>
    public virtual string CreatedUserName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime CreatedTime { get; set; }
}

