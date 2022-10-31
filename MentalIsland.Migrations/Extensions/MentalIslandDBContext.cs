// using Furion.DatabaseAccessor;
// using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Data;
using System.Reflection;
using Furion;
using MentalIsland.Core.CodeFirst.SeedData;
using SqlSugar;

namespace MentalIsland.Migrations.Extensions;

// [AppDbContext("Sqlite", DbProvider.Sqlite)]
// public class MentalIslandDBContext : AppDbContext<MentalIslandDBContext>
// {
//     public MentalIslandDBContext(DbContextOptions<MentalIslandDBContext> options) : base(options)
//     {
//         InsertOrUpdateIgnoreNullValues = true;
//     }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseLazyLoadingProxies();
//         base.OnConfiguring(optionsBuilder);
//     }
// }

public static class MentalIslandDBContext
{
    public static void CreateAndInsertData()
    {

        var client = App.GetService<ISqlSugarClient>();
        client.DbMaintenance.CreateDatabase();
        var types = Assembly.Load("MentalIsland.Core").GetTypes()
        .Where(m => m.GetCustomAttribute<SugarTable>() != null).ToArray();
        foreach (Type type in types)
        {
            client.CodeFirst.InitTables(type);
        }

        var baseType = typeof(ISeedData);
        var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
        var referencedAssemblies = System.IO.Directory.GetFiles(path, "MentalIsland.Core.dll").Select(Assembly.LoadFrom).ToArray();
        var seedDataTypes = referencedAssemblies
            .SelectMany(a => a.DefinedTypes)
            .Select(type => type.AsType())
            .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();

        foreach (var seedType in seedDataTypes)
        {
            var instance = Activator.CreateInstance(seedType);

            var hasDataMethod = seedType.GetMethod("HasData");
            var seedData = ((IEnumerable)hasDataMethod?.Invoke(instance, null))?.Cast<object>();
            if (seedData == null) continue;

            var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();

            var seedDataTable = seedData.ToList().ToDataTable();
            seedDataTable.TableName = client.EntityMaintenance.GetEntityInfo(entityType).DbTableName;

            var storage = client.Storageable(seedDataTable).ToStorage();
            storage.AsInsertable.ExecuteCommand();
        }
    }


    public static DataTable ToDataTable<T>(this List<T> list)
    {
        DataTable result = new();
        if (list.Count > 0)
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

    private static bool IsIgnoreColumn(PropertyInfo pi)
    {
        var sc = pi.GetCustomAttributes<SugarColumn>(false).FirstOrDefault(u => u.IsIgnore == true);
        return sc != null;
    }
}
