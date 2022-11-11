using System.Security.Claims;
using Furion;
using MentalIsland.AllDependency;
using MentalIsland.Migrations.Extensions.Auth;
// using MentalIsland.Core.CodeFirst.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MentalIsland.Migrations.Extensions.ControllerEx;


public class MvcBaseController<T> : Controller
{
    public ILogger<T> _logger { get; set; } = default!;
    public new MentalIslandPrincipal? User => new MentalIslandPrincipal(HttpContext.User);
    public MvcBaseController()
    {
        var props = GetType().GetProperties().Where(wa => typeof(IDependency).IsAssignableFrom(wa.PropertyType) || typeof(ILogger).IsAssignableFrom(wa.PropertyType));
        foreach (var item in props)
        {
            var service = App.GetService(item.PropertyType);
            item.SetValue(this, service);
        }
    }
}