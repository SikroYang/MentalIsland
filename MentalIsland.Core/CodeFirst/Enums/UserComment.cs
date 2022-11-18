using System.ComponentModel;

namespace MentalIsland.Core.CodeFirst.Enums;

/// <summary>
/// 用户个性化备注
/// </summary>
public enum UserComment
{
    /// <summary>
    /// 心情低落/极端
    /// </summary>
    [Description("心情低落/极端")]
    Downcast = 1,
    /// <summary>
    /// 自我迷茫
    /// </summary>
    [Description("自我迷茫")]
    Confused,
    /// <summary>
    /// 知识欠缺
    /// </summary>
    [Description("知识欠缺")]
    Knowledge,
    /// <summary>
    /// 没有特别的问题
    /// </summary>
    [Description("没有特别的问题")]
    Nothing
}