using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MentalIsland.Migrations.Extensions.ControllerEx;

public class SwaggerHideApiFilterAttribute : Attribute, IDocumentFilter
{

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var api = apiDescription.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor; //这里强转来获取到控制器的名称
            if (api.ControllerTypeInfo.IsDefined(typeof(SwaggerHideApiFilterAttribute), true) ||
            api.MethodInfo.IsDefined(typeof(SwaggerHideApiFilterAttribute), true))
            {

                string key = "/" + apiDescription.RelativePath;
                if (key.Contains("?"))
                {
                    int idx = key.IndexOf("?", System.StringComparison.Ordinal);
                    key = key.Substring(0, idx);
                }
                swaggerDoc.Paths.Remove(key);
            }
        }
    }
}