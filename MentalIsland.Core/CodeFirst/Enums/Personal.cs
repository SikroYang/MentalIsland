using System.ComponentModel;

namespace MentalIsland.Core.CodeFirst.Enums;

/// <summary>
/// 用户个性化注册类型
/// </summary>
public enum Personal
{
    /// <summary>
    /// 医疗人员
    /// </summary>
    [Description("医疗人员")]
    Medical = 1,
    /// <summary>
    /// 心理病患者
    /// </summary>
    [Description("心理病患者")]
    Psychobiology,
    /// <summary>
    /// 自我寻求者
    /// </summary>
    [Description("自我寻求者")]
    PursueSelf,
    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other
}