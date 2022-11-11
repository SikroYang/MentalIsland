// using MentalIsland.Core.CodeFirst.Identity.Models;
using MentalIsland.Core.CodeFirst.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MentalIsland.Migrations.Extensions.Auth;

public static class SignInManager
{
    public static async Task SignInAsync(HttpContext httpContext, User user)
    {

        var identity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.FullName),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
        }, CookieAuthenticationDefaults.AuthenticationScheme);


        await httpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
        {
            IsPersistent = true
        });
    }
}