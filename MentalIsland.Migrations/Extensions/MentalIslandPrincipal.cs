using System.Globalization;
using System.Security.Claims;

namespace MentalIsland.Migrations.Extensions;

public class MentalIslandPrincipal : ClaimsPrincipal
{

    public MentalIslandPrincipal(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
    {
    }

    public long? Id => FindFirstValue<long>(ClaimTypes.NameIdentifier);

    public string? UserName => FindFirstValue<string>(ClaimTypes.Name);

    public string? PhoneNumber => FindFirstValue<string>(ClaimTypes.MobilePhone);

    public string? FullName => FindFirstValue<string>(ClaimTypes.GivenName);

    private T? FindFirstValue<T>(string type)
    {
        return (T?)Convert.ChangeType(FindFirst(type)?.Value, typeof(T));
    }
}