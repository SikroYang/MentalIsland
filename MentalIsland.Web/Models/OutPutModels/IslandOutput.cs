namespace MentalIsland.Web.Models.OutPubModels;

/// <summary>
/// 岛群信息
/// </summary>
public class IslandOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; } = default!;
    /// <summary>
    /// 创建人
    /// </summary>
    public string CreatedUserName { get; set; } = default!;
    /// <summary>
    /// 岛群名称
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 岛群描述
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 岛群QQ群号
    /// </summary>
    public string QQunNumber { get; set; } = default!;
    /// <summary>
    /// 岛群QQ群二维码
    /// </summary>
    public string QQunQRCodeFile { get; set; } = default!;
    /// <summary>
    /// 岛群人数
    /// </summary>
    public int PersonNumber { get; set; } = 0;
    /// <summary>
    /// 岛群发帖数
    /// </summary>
    public int PostNumber { get; set; } = 0;
    /// <summary>
    /// 岛群最后发帖时间
    /// </summary>
    public DateTime? LastPostTime { get; set; }
    /// <summary>
    /// 岛群最后回复时间
    /// </summary>
    public DateTime? LastReplyTime { get; set; }
    /// <summary>
    /// 是否关注
    /// </summary>
    public bool IsFollow { get; set; }
}