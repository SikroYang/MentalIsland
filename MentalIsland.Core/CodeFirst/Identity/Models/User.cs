using Furion.DatabaseAccessor;

namespace MentalIsland.Core.CodeFirst.Identity.Models;

/// <summary>
/// �û�
/// </summary>
public class User : Entity<long>
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

    /// <summary>
    /// Ĭ��ֵ
    /// </summary>
    public User()
    {
        CreatedTime = DateTimeOffset.Now;
    }
}
