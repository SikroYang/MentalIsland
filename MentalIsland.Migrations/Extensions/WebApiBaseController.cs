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

#pragma warning disable CS8618 // ���˳����캯��ʱ������Ϊ null ���ֶα�������� null ֵ���뿼������Ϊ����Ϊ null��
    public WebApiBaseController()
#pragma warning restore CS8618 // ���˳����캯��ʱ������Ϊ null ���ֶα�������� null ֵ���뿼������Ϊ����Ϊ null��
    {
        var props = GetType().GetFields();
        foreach (var item in props)
        {
            var service = App.GetService(item.FieldType);
            item.SetValue(this, service);
        }
    }
}