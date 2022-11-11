using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using Furion;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace MentalIsland.Migrations.Extensions.DB;

public static class MentalIslandSqlSugarInit
{


    public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
    {
        var dbName = App.GetConfig<string>("DbType") ?? "Sqlite";
        //如果多个数数据库传 List<ConnectionConfig>
        var configConnection = new ConnectionConfig()
        {
            DbType = GetDbType(dbName),
            ConnectionString = configuration.GetConnectionString(dbName),
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute, //从实体特性中读取主键自增列信息
        };

        SqlSugarScope sqlSugar = new SqlSugarScope(configConnection,
            db =>
            {
                //单例参数配置，所有上下文生效

                // 设置超时时间
                db.Ado.CommandTimeOut = 30;
                // 打印SQL语句
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //Console.WriteLine(sql);//输出sql
                };

                // 数据审计
                db.Aop.DataExecuting = (oldValue, entityInfo) =>
                {
                    // 新增操作
                    if (entityInfo.OperationType == DataFilterType.InsertByObject)
                    {
                        // 主键(long类型)且没有值的---赋值雪花Id(已设置int自增,无需生成雪花Id)
                        // if (entityInfo.EntityColumnInfo.IsPrimarykey && entityInfo.EntityColumnInfo.PropertyInfo.PropertyType == typeof(long))
                        // {
                        //     var id = entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue);
                        //     if (id == null || (long)id == 0)
                        //         entityInfo.SetValue(Yitter.IdGenerator.YitIdHelper.NextId());
                        // }
                        if (entityInfo.PropertyName == "CreatedTime")
                            entityInfo.SetValue(DateTime.Now);
                        if (App.User != null)
                        {
                            if (entityInfo.PropertyName == "CreatedUserId")
                                entityInfo.SetValue(App.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                            if (entityInfo.PropertyName == "CreatedUserName")
                                entityInfo.SetValue(App.User.FindFirst(ClaimTypes.GivenName)?.Value);
                        }
                    }
                    // 更新操作
                    if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                    {
                        if (entityInfo.PropertyName == "UpdateTime")
                            entityInfo.SetValue(DateTime.Now);
                        if (App.User != null)
                        {
                            if (entityInfo.PropertyName == "UpdatedUserId")
                                entityInfo.SetValue(App.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                            if (entityInfo.PropertyName == "UpdatedUserName")
                                entityInfo.SetValue(App.User.FindFirst(ClaimTypes.GivenName)?.Value);
                        }

                    }
                };
            });
        services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        services.AddScoped(typeof(SqlSugarRepository<>)); // 注册仓储
        services.AddUnitOfWork<SqlSugarUnitOfWork>(); // 注册事务与工作单元
    }

    private static SqlSugar.DbType GetDbType(string dbName)
    {
        var o = new EnumConverter(typeof(SqlSugar.DbType)).ConvertFromString(dbName);
        var dbtype = (SqlSugar.DbType?)o ?? SqlSugar.DbType.MySql;
        return dbtype;
    }


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
