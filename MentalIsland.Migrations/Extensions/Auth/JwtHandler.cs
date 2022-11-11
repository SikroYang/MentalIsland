using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MentalIsland.Migrations.Extensions.Auth;
/// <summary>
/// JWT 授权自定义处理程序
/// </summary>
public class JwtHandler : AppAuthorizeHandler
{
    /// <summary>
    /// 请求管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 此处已经自动验证 Jwt token的有效性了，无需手动验证

        // 检查权限，如果方法是异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
        return await CheckAuthorzie(httpContext);
    }

    /// <summary>
    /// 检查权限
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static Task<bool> CheckAuthorzie(DefaultHttpContext httpContext)
    {
        // 获取权限特性
        var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
        if (securityDefineAttribute == null) return Task.FromResult(true);

        return Task.FromResult(false);
    }
}