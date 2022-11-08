using Furion;
using MentalIsland.Migrations.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MentalIsland.Migrations;
public class Startup : AppStartup
{

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // services.AddFreeSqlSetup();

        // Cookies 身份验证
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
        // Jwt 身份验证
        // services.AddJwt(options =>
        // {
        //     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //     options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        // })
        // .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        services.AddConfig(App.Configuration);
        services.AddMyDependencyGroup();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.Name = ".MentalIsland.Session";
            options.Cookie.IsEssential = true;
        });

        services.AddCorsAccessor();
        services.AddControllers()
            .AddInjectWithUnifyResult<AjaxResultProvider>(o => o.ConfigureSwaggerGen(c => c.DocumentFilter<SwaggerHideApiFilterAttribute>()))
            .AddDataValidation()
            .AddJsonOptions(options =>
            {
                // 该配置应用于 Swagger 文档生成
                // Swagger 文档内部默认使用System.Text.Json
                // 所以加了这段配置
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //var dbContextService = App.GetService<MentalIslandDBContext>();
        //if (dbContextService.Database.GetPendingMigrations().Any())
        //{
        //    dbContextService.Database.Migrate(); //执行迁移
        //}

        // 初始化数据库表结构及种子数据
        MentalIslandSqlSugarInit.InitDataBase();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseUnifyResultStatusCodes();

        //app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();
        // app.UseHttpMetrics();

        app.UseSession();
        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject(options =>
        {
            options.ConfigureSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
            });
        });

        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapMetrics("/wujierp/metrics");
            endpoints.MapControllers();
            // endpoints.MapFallback(async (context) =>
            // {
            //     var phpath = Path.Join(env.WebRootPath, context.Request.Path);
            //     var name = Path.Combine(Path.GetDirectoryName(phpath)!, "index.html");
            //     if (File.Exists(name))
            //     {
            //         context.Response.StatusCode = 200;
            //         await context.Response.SendFileAsync(name);
            //     }
            // });
            endpoints.MapFallback(async (context) => await Task.Run(() => { context.Response.Redirect("/MentalIsland"); }));
        });

    }
}
