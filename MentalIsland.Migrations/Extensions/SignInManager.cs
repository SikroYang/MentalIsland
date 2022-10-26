using MentalIsland.Core.CodeFirst.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MentalIsland.Migrations.Extensions;

public static class SignInManager
{
    public static async Task SignInAsync(HttpContext httpContext, User user)
    {

        var identity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
        }, CookieAuthenticationDefaults.AuthenticationScheme);


        await httpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
        {
            IsPersistent = true
        });
    }
}