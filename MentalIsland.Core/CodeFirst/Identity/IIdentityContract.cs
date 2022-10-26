using Furion.DatabaseAccessor;
using MentalIsland.AllDependency;

namespace MentalIsland.Core.CodeFirst.Identity;

using Models;
/// <summary>
/// 用户操作接口
/// </summary>
public interface IIdentityContract : IScopeDependency
{
    /// <summary>
    /// 用户操作仓库
    /// </summary>
    IRepository<User> UserRepository { get; set; }
    /// <summary>
    /// 用户仓库
    /// </summary>
    IQueryable<User> Users { get; }
    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <returns></returns>
    IQueryable<User> GetList();
    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<long> Add(User user);

    /// <summary>
    /// 判断当前用户登录是否成功
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<User> Login(string userName, string password);
}