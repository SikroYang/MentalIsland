using Furion.FriendlyException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MentalIsland.Migrations.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class Authorize4MentalIslandAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        if (descriptor == null || descriptor.MethodInfo.CustomAttributes
            .Any(f => f.AttributeType.Name == "AllowAnonymousAttribute")) return;

        var principal = context.HttpContext.User;

        if (principal?.Identity != null && principal.Identity.IsAuthenticated)
            return;
        else
            throw Oops.Oh("您还未登录!").StatusCode(401);
    }
}