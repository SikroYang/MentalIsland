using MentalIsland.Migrations.Extensions.ControllerEx;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentalIsland.Web.Controllers;

/// <summary>
/// 首页(测试,请忽略)
/// </summary>
[Route("[controller]")]
[Authorize]
[SwaggerHideApiFilter]
public class HomeController : WebApiBaseController<HomeController>
{
    /// <summary>
    /// 首页(需认证)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public List<string> Index()
    {
        var list = new List<string> { "心理", "咨询", "心理咨询" };
        return list;
    }

    /// <summary>
    /// 首页
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public List<string> List()
    {
        var list = new List<string> { "心理", "咨询", "心理咨询" };
        return list;
    }

}

