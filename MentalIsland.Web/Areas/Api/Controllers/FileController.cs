using System.ComponentModel.DataAnnotations;
using Furion;
using Furion.FriendlyException;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using MentalIsland.Migrations.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snowflake.Core;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 文件
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[MentalIslandAuthorize]
public class FileController : WebApiBaseController<FileController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。


#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 上传图片
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> UploadImage([Required] IFormFile file)
    {
        bool isSuccess = file != null;
        var worker = new IdWorker(1, 1);
        long id = worker.NextId();
        if (isSuccess)
        {
            var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Upload/Images/");
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            var suffix = Path.GetExtension(file.FileName).ToLower();
            var finalName = id.ToString() + suffix;
            var realFile = Path.Combine(filePath, finalName);
            using var stream = System.IO.File.Create(realFile);

            await file.CopyToAsync(stream);

            // 生成外链
            var newFileUrl = $"/Upload/Images/{finalName}";
            return newFileUrl;
        }
        throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
    }
}
