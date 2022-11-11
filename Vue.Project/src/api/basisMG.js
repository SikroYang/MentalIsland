import { post } from './axiosFun';

/**
 * 岛群管理
 **/
// 岛群管理-获取岛群列表
export const IslandsList = (data) => { return post("/Api/Island/List", data) };
// 岛群管理-保存岛群
export const IslandSave = (data) => { return post("/Api/Island/AddOrUpdateIsland", data) };
// 岛群管理-删除岛群
export const IslandDelete = (data) => { return post("/Api/Island/DeleteIsland", data) };

/**
 * 文章管理
 **/
// 文章管理-获取文章列表
export const ArticlesList = (data) => { return post("/Api/Article/List", data) };
// 文章管理-保存文章
export const ArticleSave = (data) => { return post("/Api/Article/AddOrUpdate", data) };
// 文章管理-删除文章
export const ArticleDelete = (data) => { return post("/Api/Article/DeleteArticle", data) };

// 文章管理-获取文章类型列表
export const ArticleTypesList = (data) => { return post("/Api/Article/TypeList", data) };
// 文章管理-保存文章类型
export const ArticleTypeSave = (data) => { return post("/Api/Article/AddOrUpdateType", data) };
// 文章管理-删除文章类型
export const ArticleTypeDelete = (data) => { return post("/Api/Article/DeleteArticleType", data) };
