// using System.Security.Cryptography;
// using System.Text;
// using Furion.DatabaseAccessor;

// namespace MentalIsland.Core.CodeFirst.Identity;

// using System.Threading.Tasks;
// using Models;
// /// <summary>
// /// �û�����ʵ��
// /// </summary>
// public class IdentityService : IIdentityContract
// {
//     /// <summary>
//     /// �û������ֿ�
//     /// </summary>
//     public IRepository<User> UserRepository { get; set; } = Db.GetRepository<User>();
//     /// <summary>
//     /// �û��ֿ�
//     /// </summary>
//     public IQueryable<User> Users => UserRepository.Entities;

//     /// <summary>
//     /// ����û�
//     /// </summary>
//     /// <param name="user"></param>
//     /// <returns></returns>
//     public async Task<long> Add(User user)
//     {
//         var result = await UserRepository.InsertNowAsync(user);
//         return result.IsKeySet ? result.Entity.Id : 0;
//     }

//     /// <summary>
//     /// �жϵ�ǰ�û���¼�Ƿ�ɹ�
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
//     /// ��ȡ�û��б�
//     /// </summary>
//     /// <returns></returns>
//     public IQueryable<User> GetList()
//     {
//         return Users.OrderBy(wa => wa.Id);
//     }
// }