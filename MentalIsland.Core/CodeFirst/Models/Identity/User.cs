using System.ComponentModel;
using System.Data;
using MentalIsland.Core.CodeFirst.SqlSugarHelper;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models.Identity;

/// <summary>
/// �û�
/// </summary>
[SugarTable("Users")]
[Description("�û���")]
public class User : Entity<int>
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
    public string NickName { get; set; } = default!;
    /// <summary>
    /// ����Hash
    /// </summary>
    public string PasswordHash { get; set; } = default!;
    /// <summary>
    /// ����/����
    /// </summary>
    public string Country { get; set; } = default!;

}
