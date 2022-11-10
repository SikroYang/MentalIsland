using System.Text;
using System.Security.Cryptography;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using Furion.DataEncryption;

namespace MentalIsland.Core.CodeFirst.SeedData;

public class IslandSeedData : ISeedData, ISqlSugarEntitySeedData<Island>
{
    // 测试数据
    public IEnumerable<Island> HasData()
    {
        return new List<Island>{
            new Island{
                Id = 1,
                Name = "岛群一",
                Description = "创建第一个岛群",
                CreatedTime = DateTime.Now
            }
        };
    }

}