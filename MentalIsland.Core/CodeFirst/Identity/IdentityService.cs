// using System.Security.Cryptography;
// using System.Text;
// using Furion.DatabaseAccessor;

// namespace MentalIsland.Core.CodeFirst.Identity;

// using System.Threading.Tasks;
// using Models;
// /// <summary>
// /// 用户操作实体
// /// </summary>
// public class IdentityService : IIdentityContract
// {
//     /// <summary>
//     /// 用户操作仓库
//     /// </summary>
//     public IRepository<User> UserRepository { get; set; } = Db.GetRepository<User>();
//     /// <summary>
//     /// 用户仓库
//     /// </summary>
//     public IQueryable<User> Users => UserRepository.Entities;

//     /// <summary>
//     /// 添加用户
//     /// </summary>
//     /// <param name="user"></param>
//     /// <returns></returns>
//     public async Task<long> Add(User user)
//     {
//         var result = await UserRepository.InsertNowAsync(user);
//         return result.IsKeySet ? result.Entity.Id : 0;
//     }

//     /// <summary>
//     /// 判断当前用户登录是否成功
//     /// </summary>
//     /// <param name="userName"></param>
//     /// <param name="password"></param>
//     /// <returns></returns>
//     public async Task<User> Login(string userName, string password)
//     {
//         var passwordHash = Encoding.UTF8.GetString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
//         var result =
//             await UserRepository.SingleOrDefaultAsync(wa => wa.UserName == userName && wa.PasswordHash == passwordHash);

//         return result;
//     }

//     /// <summary>
//     /// 获取用户列表
//     /// </summary>
//     /// <returns></returns>
//     public IQueryable<User> GetList()
//     {
//         return Users.OrderBy(wa => wa.Id);
//     }
// }