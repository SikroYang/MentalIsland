using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MentalIsland.Migrations.Extensions;

public static class MyServicesExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
        // 设置依赖注入中间件

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
    {
        // 设置依赖注入中间件
        services.AddAutoDependency();
        // 配置数据库上下文，支持N个数据库
        services.AddDatabaseAccessor(options =>
        {
            // 配置默认数据库
            options.AddDbPool<MentalIslandDBContext>();
            // 配置多个数据库，多个数据库必须指定数据库上下文定位器
            //  options.AddDbPool<SqliteDbContext, SqliteDbContextLocaotr>();
        }, "MentalIsland.Migrations");
        return services;
    }
}