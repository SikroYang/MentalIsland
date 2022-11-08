using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// ��Ⱥ
/// </summary>
[SugarTable("Islands")]
[Description("��Ⱥ��")]
[SugarIndex("unique_Name_1", nameof(Name), OrderByType.Asc, true)]
public class Island : Entity
{
    /// <summary>
    /// ��Ⱥ����
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// ��Ⱥ����
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? Description { get; set; }
    /// <summary>
    /// ��ȺQQȺ��
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? QQunNumber { get; set; }
    /// <summary>
    /// ��ȺQQȺ��ά��
    /// </summary>
    [SugarColumn(IsNullable = true)]
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
    [SugarColumn(IsNullable = true)]
    public DateTime? LastPostTime { get; set; }
    /// <summary>
    /// ��Ⱥ���ظ�ʱ��
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? LastReplyTime { get; set; }

    /// <summary>
    /// �ѹ�ע��Ⱥ�û�
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(typeof(User_Island), nameof(User_Island.IslandId), nameof(User_Island.UserId))]
    public virtual List<User> Users { get; set; }

    /// <summary>
    /// �����б�
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(Island_Post.IslandId))]
    public virtual List<Island_Post> Posts { get; set; }

}
