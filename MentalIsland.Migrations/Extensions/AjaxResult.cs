using Microsoft.AspNetCore.Http;

namespace MentalIsland.Migrations.Extensions;
public class AjaxResult<T>
{
    /// <summary>
    /// 状态码
    /// </summary>
    public int Code { get; set; } = StatusCodes.Status200OK;

    /// <summary>
    /// 类型
    /// </summary>
    public AjaxResultType TypeId { get; set; } = AjaxResultType.Success;

    /// <summary>
    /// 类型描述
    /// </summary>
    public virtual string Type => TypeId.ToString();

    /// <summary>
    /// 消息
    /// </summary>
    public string Content { get; set; } = default!;
    /// <summary>
    /// 返回数据
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public object Errors { get; set; } = default!;
    /// <summary>
    /// 附加数据
    /// </summary>
    public object Extras { get; set; } = default!;
    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}
