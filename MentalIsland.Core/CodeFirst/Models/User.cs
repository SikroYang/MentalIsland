using System.ComponentModel;
using MentalIsland.Core.CodeFirst.Enums;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// �û�
/// </summary>
[SugarTable("Users")]
[Description("�û���")]
public class User : Entity
{
    /// <summary>
    /// �û���
    /// </summary>
    public string UserName { get; set; } = default!;
    /// <summary>
    /// ��������
    /// </summary>
    public string Email { get; set; } = default!;
    /// <summary>
    /// �ֻ���
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    /// <summary>
    /// �ǳ�
    /// </summary>
    public string FullName { get; set; } = default!;
    /// <summary>
    /// ����Hash
    /// </summary>
    public string PasswordHash { get; set; } = default!;
    /// <summary>
    /// ����/����
    /// </summary>
    public string Country { get; set; } = default!;
    /// <summary>
    /// �Ƿ������û�
    /// </summary>
    public bool IsLocked { get; set; } = false;

    /// <summary>
    /// �û�����
    /// </summary>
    public UserType UserType { get; set; } = UserType.Normal;

    /// <summary>
    /// �û��ѹ�ע��Ⱥ
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(typeof(User_Island), nameof(User_Island.UserId), nameof(User_Island.IslandId))]
    public virtual List<Island> Islands { get; set; }

}
