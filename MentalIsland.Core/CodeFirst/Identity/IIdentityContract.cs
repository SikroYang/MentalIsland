using Furion.DatabaseAccessor;
using MentalIsland.AllDependency;

namespace MentalIsland.Core.CodeFirst.Identity;

using Models;
/// <summary>
/// �û������ӿ�
/// </summary>
public interface IIdentityContract : IScopeDependency
{
    /// <summary>
    /// �û������ֿ�
    /// </summary>
    IRepository<User> UserRepository { get; set; }
    /// <summary>
    /// �û��ֿ�
    /// </summary>
    IQueryable<User> Users { get; }
    /// <summary>
    /// ��ȡ�û��б�
    /// </summary>
    /// <returns></returns>
    IQueryable<User> GetList();
    /// <summary>
    /// ����û�
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<long> Add(User user);

    /// <summary>
    /// �жϵ�ǰ�û���¼�Ƿ�ɹ�
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<User> Login(string userName, string password);
}