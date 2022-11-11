using Furion;
using MentalIsland.Migrations.Extensions.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

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
}