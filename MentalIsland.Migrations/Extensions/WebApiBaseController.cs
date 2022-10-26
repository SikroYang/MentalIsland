using Furion;
using MentalIsland.AllDependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MentalIsland.Migrations.Extensions;

[ApiController]
public class WebApiBaseController<T> : ControllerBase
{
    public ILogger<T> _logger { get; set; } = default!;
    public new MentalIslandPrincipal? User => new MentalIslandPrincipal(HttpContext.User);

    public WebApiBaseController()
    {
        var props = GetType().GetProperties().Where(wa => typeof(IDependency).IsAssignableFrom(wa.PropertyType) || typeof(ILogger).IsAssignableFrom(wa.PropertyType));
        foreach (var item in props)
        {
            var service = App.GetService(item.PropertyType);
            item.SetValue(this, service);
        }
    }
}