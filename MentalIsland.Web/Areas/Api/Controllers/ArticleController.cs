using System.ComponentModel.DataAnnotations;
using Furion;
using Furion.FriendlyException;
using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using MentalIsland.Migrations.Extensions;
using MentalIsland.Migrations.Extensions.Auth;
using MentalIsland.Migrations.Extensions.ControllerEx;
using MentalIsland.Web.Models.InputModels;
using MentalIsland.Web.Models.OutPubModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snowflake.Core;
using SqlSugar;

namespace MentalIsland.Web.Areas.Api.Controllers;

/// <summary>
/// 科普文章
/// </summary>
[Area("Api")]
[Route("[area]/[controller]")]
[MentalIslandAuthorize]
public class ArticleController : WebApiBaseController<ArticleController>
{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 科普文章仓库
    /// </summary>
    public readonly SqlSugarRepository<Article> articleRepository;
    /// <summary>
    /// 文章类型仓库
    /// </summary>
    public readonly SqlSugarRepository<ArticleType> articleTypeRepository;

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 文章列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<PagedList<ArticleOutput>> List(ArticleSearchInput searchInfo)
    {
        var exp = new Expressionable<Article, ArticleType>().And((a, t) => !a.IsDeleted && !t.IsDeleted);
        if (searchInfo.ArticleTypeId > 0)
            exp.And((a, t) => a.ArticleTypeId == searchInfo.ArticleTypeId);
        if (!string.IsNullOrWhiteSpace(searchInfo.Title))
            exp.And((a, t) => a.Title.Contains(searchInfo.Title));

        var result = articleRepository.AsQueryable()
                        .LeftJoin<ArticleType>((a, t) => a.ArticleTypeId == t.Id)
                        .Where(exp.ToExpression())
                        .Select((a, t) => new ArticleOutput
                        {
                            Id = a.Id,
                            ArticleTypeId = a.ArticleTypeId,
                            Title = a.Title,
                            Content = a.Content,
                            CreatedTime = a.CreatedTime,
                            ArticleTypeName = t.Name
                        });
        var sqlStr = result.ToSqlString();

        var Total = await result.CountAsync();
        var list = searchInfo.Page > 0 ? await result.ToPageListAsync(searchInfo.Page, searchInfo.Size) : await result.ToListAsync();
        if (searchInfo.Page > 0)
            return new PagedList<ArticleOutput> { Page = searchInfo.Page, Size = searchInfo.Size, Total = Total, List = list };
        return new PagedList<ArticleOutput> { Total = Total, List = list };
    }

    /// <summary>
    /// 添加或修改文章
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddOrUpdate(ArticleInput article)
    {
        if (article.ArticleTypeId <= 0) Oops.Bah("类型Id必须大于0").StatusCode();
        var articleRes = article.Adapt<Article>();
        bool isSuccess;
        int Id = article.Id ?? 0;
        if (Id == 0)
        {
            Id = await articleRepository.AsInsertable(articleRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            isSuccess = await articleRepository.AsUpdateable(articleRes).IgnoreColumns(true).IgnoreColumns(l => new { l.CreatedTime }).ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 删除文章
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeleteArticle(OnlyId model)
    {
        var deleteArticle = await articleRepository.GetByIdAsync(model.Id);
        if (deleteArticle == null || deleteArticle.IsDeleted) throw Oops.Bah("该帖子不存在或已删除").StatusCode();
        var isSuccess = await articleRepository.RecycleByIdAsync<Article>(deleteArticle.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }

    /// <summary>
    /// 文章类型
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<ArticleTypeOutput>> TypeList()
    {
        var result = await articleTypeRepository.AsQueryable().ToListAsync();
        return result.Adapt<List<ArticleTypeOutput>>();
    }

    /// <summary>
    /// 添加或修改文章类型
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> AddOrUpdateType(IdOrName model)
    {
        if (model.Id <= 0 && string.IsNullOrWhiteSpace(model.Name))
            throw Oops.Bah("传递空值,请检查").StatusCode();
        bool isSuccess;
        int Id = model.Id < 0 ? 0 : model.Id;
        var typeRes = model.Adapt<ArticleType>();
        if (Id == 0)
        {
            Id = await articleTypeRepository.AsInsertable(typeRes).ExecuteReturnIdentityAsync();
            isSuccess = Id > 0;
        }
        else
        {
            isSuccess = await articleTypeRepository.AsUpdateable(typeRes).IgnoreColumns(true).ExecuteCommandHasChangeAsync();
        }
        if (!isSuccess) throw Oops.Bah("保存失败,请检查后重新尝试!").StatusCode();
        return Id;
    }

    /// <summary>
    /// 删除文章
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> DeleteArticleType(OnlyId model)
    {
        var deleteArticleType = await articleTypeRepository.GetByIdAsync(model.Id);
        if (deleteArticleType == null || deleteArticleType.IsDeleted) throw Oops.Bah("该类型不存在或已删除").StatusCode();
        var isSuccess = await articleTypeRepository.RecycleByIdAsync<Article>(deleteArticleType.Id);
        if (!isSuccess) throw Oops.Bah("删除失败,请检查后重新尝试!").StatusCode();
        return "删除成功!";
    }
}
