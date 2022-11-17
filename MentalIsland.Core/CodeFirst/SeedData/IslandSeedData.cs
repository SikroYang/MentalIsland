// using System.Text;
// using System.Security.Cryptography;
// using MentalIsland.Core.CodeFirst.Models;
// using MentalIsland.Core.CodeFirst.SqlSugarBase;
// using Furion.DataEncryption;

// namespace MentalIsland.Core.CodeFirst.SeedData;

// /// <summary>
// /// 岛群数据种子类
// /// </summary>
// public class IslandSeedData : ISeedData, ISqlSugarEntitySeedData<Island>
// {
//     /// <summary>
//     /// 岛群数据种子
//     /// </summary>
//     public IEnumerable<Island> HasData()
//     {
//         // 测试数据
//         return new List<Island>{
//             new Island{
//                 Id = 1,
//                 Name = "岛群一",
//                 Description = "创建第一个岛群",
//                 CreatedTime = DateTime.Now
//             }
//         };
//     }

// }