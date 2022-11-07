using Furion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MentalIsland.Migrations.Extensions;

[ApiController]
public class WebApiBaseController<T> : ControllerBase
{
    public readonly ILogger<T> _logger;
    public new MentalIslandPrincipal User => new MentalIslandPrincipal(HttpContext.User);

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
    public WebApiBaseController()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
    {
        var props = GetType().GetFields();
        foreach (var item in props)
        {
            var service = App.GetService(item.FieldType);
            item.SetValue(this, service);
        }
    }
}