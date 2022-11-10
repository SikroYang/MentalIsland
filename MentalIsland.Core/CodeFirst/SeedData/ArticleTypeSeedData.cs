using System.Text;
using System.Security.Cryptography;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using Furion.DataEncryption;

namespace MentalIsland.Core.CodeFirst.SeedData;

public class ArticleTypeSeedData : ISeedData, ISqlSugarEntitySeedData<ArticleType>
{
    // 测试数据
    public IEnumerable<ArticleType> HasData()
    {
        return new List<ArticleType>{
            new ArticleType{
                Id = 1,
                Name = "基础科普"
            },
            new ArticleType{
                Id = 2,
                Name = "双向的著名作品"
            },
            new ArticleType{
                Id = 3,
                Name = "一手调研"
            },
            new ArticleType{
                Id = 4,
                Name = "有意思的案例"
            }
        };
    }

}