using Furion;
using Furion.DataValidation;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MentalIsland.Migrations.Extensions;

[UnifyModel(typeof(AjaxResult<>))]
public class AjaxResultProvider : IUnifyResultProvider
{

    /// <summary>
    /// 异常返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="metadata"></param>
    /// <returns></returns>
    public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
    {
        return new JsonResult(SetAjaxResult(metadata.StatusCode, data: metadata.Data, errors: metadata.Errors),
            UnifyContext.GetSerializerSettings(context));
    }
    /// <summary>
    /// 成功返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        return new JsonResult(SetAjaxResult(StatusCodes.Status200OK, data),
            UnifyContext.GetSerializerSettings(context));
    }
    /// <summary>
    /// 验证失败返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="metadata"></param>
    /// <returns></returns>
    public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
    {
        return new JsonResult(SetAjaxResult(metadata.StatusCode ?? StatusCodes.Status400BadRequest, data: metadata.Data, errors: metadata.ValidationResult), UnifyContext.GetSerializerSettings(context));
    }
    /// <summary>
    /// 特定状态码返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    /// <param name="unifyResultSettings"></param>
    /// <returns></returns>
    public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings)
    {
        // 设置响应状态码
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);
        switch (statusCode)
        {
            // 处理 401 状态码
            case StatusCodes.Status401Unauthorized:
                await context.Response.WriteAsJsonAsync(SetAjaxResult(statusCode, errors: "401 未登录")
                    , App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                break;
            // 处理 403 状态码
            case StatusCodes.Status403Forbidden:
                await context.Response.WriteAsJsonAsync(SetAjaxResult(statusCode, errors: "403 访问无权限")
                    , App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                break;
            default: break;
        }
    }
    /// <summary>
    /// 返回 RESTful 风格结果集
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="data"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private static AjaxResult<object> SetAjaxResult(int statusCode, object data = default!, object errors = default!)
    {
        var Type = AjaxResultType.Success;
        if (statusCode >= 1000)
            Type = AjaxResultType.Info;
        if (statusCode >= 400)
            Type = statusCode == 401 ? AjaxResultType.Warning : AjaxResultType.Error;

        return new AjaxResult<object>
        {
            Code = statusCode,
            Content = statusCode < 400 ? "请求成功!" : errors is string str ? str : JSON.Serialize(errors),
            Type = Type,
            Data = data,
            Errors = errors,
            Extras = UnifyContext.Take(),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
    }
}