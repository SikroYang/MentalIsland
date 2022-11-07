using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// ��Ⱥ
/// </summary>
[SugarTable("Islands")]
[Description("��Ⱥ��")]
public class Island : Entity
{
    /// <summary>
    /// ��Ⱥ����
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// ��Ⱥ����
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// ��ȺQQȺ��
    /// </summary>
    public string? QQunNumber { get; set; }
    /// <summary>
    /// ��ȺQQȺ��ά��
    /// </summary>
    public string? QQunQRCodeFile { get; set; }
    /// <summary>
    /// ��Ⱥ����
    /// </summary>
    public int PersonNumber { get; set; } = 0;
    /// <summary>
    /// ��Ⱥ������
    /// </summary>
    public int PostNumber { get; set; } = 0;
    /// <summary>
    /// ��Ⱥ�����ʱ��
    /// </summary>
    public DateTime? LastPostTime { get; set; }
    /// <summary>
    /// ��Ⱥ���ظ�ʱ��
    /// </summary>
    public DateTime? LastReplyTime { get; set; }

}
