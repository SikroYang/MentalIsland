using System.Security.Claims;
using Furion;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using SqlSugar;

namespace MentalIsland.Migrations.Extensions;

public static class MyServicesExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
        // 设置依赖注入中间件
        // 注入Sqlsugar
        services.AddSqlsugarSetup(config);
        // Add MailKit
        services.AddMailKit(optionBuilder =>
        {
            var option = App.GetConfig<MailKitOptions>("MailKitSettings:163");
            optionBuilder.UseMailKit(option);
        });

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
    {
        // 设置依赖注入中间件
        // services.AddAutoDependency();

        // 日志记录
        if (App.GetConfig<bool>("Logging:File:Enable")) // 日志写入文件
        {
            Array.ForEach(new[] { LogLevel.Information, LogLevel.Warning, LogLevel.Error }, logLevel =>
            {
                services.AddFileLogging(options =>
                {
                    options.FileNameRule = fileName => string.Format(fileName, DateTime.Now, logLevel.ToString()); // 每天创建一个文件
                    options.WriteFilter = logMsg => logMsg.LogLevel == logLevel; // 日志级别
                    options.HandleWriteError = (writeError) => // 写入失败时启用备用文件
                    {
                        writeError.UseRollbackFileName(Path.GetFileNameWithoutExtension(writeError.CurrentFileName) + "-oops" + Path.GetExtension(writeError.CurrentFileName));
                    };
                });
            });
        }

        // 配置数据库上下文，支持N个数据库
        // services.AddDatabaseAccessor(options =>
        // {
        //     // 配置默认数据库
        //     options.AddDbPool<MentalIslandDBContext>();
        //     // 配置多个数据库，多个数据库必须指定数据库上下文定位器
        //     //  options.AddDbPool<SqliteDbContext, SqliteDbContextLocaotr>();
        // }, "MentalIsland.Migrations");
        return services;
    }

    public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration, string dbName = "Sqlite")
    {
        //如果多个数数据库传 List<ConnectionConfig>
        var configConnection = new ConnectionConfig()
        {
            DbType = SqlSugar.DbType.Sqlite,
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
}