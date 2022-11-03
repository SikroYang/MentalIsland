using Furion.FriendlyException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MentalIsland.Migrations.Extensions;

public class Authorize4MentalIslandAttribute : Attribute, IAsyncAuthorizationFilter
{
    // public override void OnActionExecuting(ActionExecutingContext context)
    // {
    //     var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
    //     if (descriptor == null || descriptor.MethodInfo.CustomAttributes
    //         .Any(f => f.AttributeType.Name == "AllowAnonymousAttribute")) return;

    //     var principal = context.HttpContext.User;

    //     if (principal?.Identity != null && principal.Identity.IsAuthenticated)
    //         return;
    //     else
    //         throw Oops.Oh("您还未登录!").StatusCode(401);
    // }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // 获取控制器信息
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        // 获取控制器类型
        var controllerType = actionDescriptor!.ControllerTypeInfo;

        // 获取 Action 类型
        var methodType = actionDescriptor.MethodInfo;

        // 是否匿名访问
        var allowAnonymouse = context.Filters.Any(u => u is IAllowAnonymousFilter)
                        || controllerType.IsDefined(typeof(AllowAnonymousAttribute), true)
                        || methodType.IsDefined(typeof(AllowAnonymousAttribute), true);

        if (allowAnonymouse) await Task.CompletedTask;
        else
        {
            // WebAPI
            if (controllerType.IsDefined(typeof(ApiControllerAttribute), true))
            {
                var principal = context.HttpContext.User;

                if (principal?.Identity != null && principal.Identity.IsAuthenticated)
                    await Task.CompletedTask;
                else
                    throw Oops.Oh("您还未登录!").StatusCode(401);
            }
            // MVC
            // if (typeof(Controller).IsAssignableFrom(controllerType.AsType()))
            // {
            //     context.Result = new RedirectToActionResult("Login", "User", new { area = "" });
            // }
        }
    }
}