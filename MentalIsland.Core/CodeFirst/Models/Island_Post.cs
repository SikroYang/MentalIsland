using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// ��Ⱥ����
/// </summary>
[SugarTable("Island_Posts")]
[Description("���ӱ�")]
public class Island_Post : Entity
{
    /// <summary>
    /// ���ӱ���
    /// </summary>
    public string Title { get; set; } = default!;
    /// <summary>
    /// ����
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(max)")] // Sqlite ��֧��max
    public string Content { get; set; } = default!;
    /// <summary>
    /// �ظ���
    /// </summary>
    public int ReplyNumber { get; set; }
    /// <summary>
    /// ���ظ�ʱ��
    /// </summary>
    public DateTime LastReplyTime { get; set; }
    /// <summary>
    /// ��ȺId
    /// </summary>
    public int IslandId { get; set; }

    /// <summary>
    /// �����б�
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(Island_Reply.PostId))]
    public virtual List<Island_Reply> Replies { get; set; }
}
