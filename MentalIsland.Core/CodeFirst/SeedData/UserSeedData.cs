using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using Furion.DataEncryption;

namespace MentalIsland.Core.CodeFirst.SeedData;

/// <summary>
/// 用户数据种子类
/// </summary>
public class UserSeedData : ISeedData, ISqlSugarEntitySeedData<User>
{
    /// <summary>
    /// 用户数据种子
    /// </summary>
    public IEnumerable<User> HasData()
    {
        return new List<User>{
            new User{
                Id = 1,
                UserName = "admin",
                PasswordHash = MD5Encryption.Encrypt("admin123"),
                Country = "China",
                Email = "yxx95lz@163.com",
                FullName = "admin",
                PhoneNumber = "15962191164",
                UserType = Enums.UserType.Admin,
                CreatedTime = DateTime.Now
            }
        };
    }

}