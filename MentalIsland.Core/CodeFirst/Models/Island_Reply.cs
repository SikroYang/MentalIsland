using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// ��Ⱥ���ӻظ�
/// </summary>
[SugarTable("Island_Replies")]
[Description("�ظ���")]
public class Island_Reply : Entity
{
    /// <summary>
    /// �ظ�����
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// ����Id
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// ���ӵ�������
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(PostId))]
    public virtual Island_Post Post { get; set; }

}
