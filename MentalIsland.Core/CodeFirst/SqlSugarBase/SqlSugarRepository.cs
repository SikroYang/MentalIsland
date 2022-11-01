using Furion;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.SqlSugarBase;
/// <summary>
/// SqlSugar仓储类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SqlSugarRepository<T> : SimpleClient<T> where T : class, new()
{
    public SqlSugarRepository(ISqlSugarClient context = null) : base(context)
    {
        base.Context = App.GetService<ISqlSugarClient>();
    }
}