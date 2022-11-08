using System.Collections;
using System.Data;
using System.Reflection;
using Furion;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Migrations.Extensions;

public static class MentalIslandSqlSugarInit
{
    /// <summary>
    /// 实体类转换为DataTable
    /// </summary>
    public static DataTable ToDataTable<T>(this List<T> list)
    {
        DataTable result = new();
        if (list != null && list.Count > 0)
        {
            // result.TableName = list[0].GetType().Name; // 表名赋值
            PropertyInfo[] propertys = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                Type colType = pi.PropertyType;
                if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                if (IsIgnoreColumn(pi))
                    continue;
                result.Columns.Add(pi.Name, colType);
            }
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new();
                foreach (PropertyInfo pi in propertys)
                {
                    if (IsIgnoreColumn(pi))
                        continue;
                    object obj = pi.GetValue(list[i], null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }

    /// <summary>
    /// 不创建忽略的列
    /// </summary>
    private static bool IsIgnoreColumn(PropertyInfo pi)
    {
        var sc = pi.GetCustomAttributes<SugarColumn>(false).FirstOrDefault(u => u.IsIgnore == true);
        return sc != null;
    }


    /// <summary>
    /// 初始化数据库结构
    /// </summary>
    public static void InitDataBase()
    {
        var client = App.GetService<ISqlSugarClient>();
        client.DbMaintenance.CreateDatabase();

        // 获取所有实体表-初始化表结构
        var entityTypes = App.EffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass
            && u.IsDefined(typeof(SugarTable), false));
        if (!entityTypes.Any()) return;
        foreach (var entityType in entityTypes)
        {
            client.CodeFirst.InitTables(entityType);
        }

        // 获取所有种子配置-初始化数据
        var seedDataTypes = App.EffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass
            && u.GetInterfaces().Any(i => typeof(ISeedData) == i));
        if (!seedDataTypes.Any()) return;
        foreach (var seedType in seedDataTypes)
        {
            var instance = Activator.CreateInstance(seedType);

            var hasDataMethod = seedType.GetMethod("HasData");
            var seedData = ((IEnumerable?)hasDataMethod?.Invoke(instance, null))?.Cast<object>();
            if (seedData == null) continue;

            var seedDataTable = seedData.ToList().ToDataTable();
            var entityType = seedType.GetInterfaces().First(t => t.IsGenericType).GetGenericArguments().First();
            seedDataTable.TableName = client.EntityMaintenance.GetEntityInfo(entityType).DbTableName;
            if (seedDataTable.Columns.Contains("Id"))
            {
                var storage = client.Storageable(seedDataTable).WhereColumns("Id").ToStorage();
                storage.AsInsertable.ExecuteCommand();
                storage.AsUpdateable.ExecuteCommand();
            }
            else // 没有主键或者不是预定义的主键(没主键有重复的可能)
            {
                var storage = client.Storageable(seedDataTable).ToStorage();
                storage.AsInsertable.ExecuteCommand();
            }
        }
    }
}
