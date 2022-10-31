using Furion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var option = App.GetConfig<MailKitOptions>("MailKitSettings:Outlook");
            optionBuilder.UseMailKit(option);
        });

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
    {
        // 设置依赖注入中间件
        // services.AddAutoDependency();
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
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //Console.WriteLine(sql);//输出sql
                };
            });
        services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
    }
}