using System.Text;
using System.Security.Cryptography;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;

namespace MentalIsland.Core.CodeFirst.SeedData;

public class UserSeedData : ISeedData, ISqlSugarEntitySeedData<User>
{
    public IEnumerable<User> HasData()
    {
        return new List<User>{
            new User{
                Id = 1,
                UserName = "admin",
                PasswordHash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("admin123"))).Replace("-","").ToLower(),
                Country = "China",
                Email = "yxx95lz@163.com",
                NickName = "Sikro",
                PhoneNumber = "15962191164",
            }
        };
    }

}