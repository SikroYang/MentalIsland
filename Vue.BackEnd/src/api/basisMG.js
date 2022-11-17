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
 * 帖子管理
 **/
// 帖子管理-获取帖子列表
export const PostsList = (data) => { return post("/Api/Post/List", data) };
// 帖子管理-保存帖子
export const PostSave = (data) => { return post("/Api/Post/AddOrUpdatePost", data) };
// 帖子管理-根据Id获取帖子
export const PostById = (data) => { return post("/Api/Post/PostById", data) };
// 帖子管理-删除帖子
export const PostDelete = (data) => { return post("/Api/Post/DeletePost", data) };

// 评论管理-保存评论
export const ReplySave = (data) => { return post("/Api/Post/AddOrUpdateReply", data) };
// 评论管理-删除评论
export const ReplyDelete = (data) => { return post("/Api/Post/DeleteReply", data) };

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

/**
 * 关键词管理
 **/
// 关键词管理-获取关键词列表
export const BlackWordList = (data) => { return post("/Api/User/BlackWord", data) };
// 关键词管理-保存关键词
export const BlackWordSave = (data) => { return post("/Api/User/SetBlackWord", data) };
// 关键词管理-保存关键词
export const BlackWordDelete = (data) => { return post("/Api/User/BlackWordDelete", data) };


// 关键词管理-保存关键词
export const UploadImage = (data) => { return post("/Api/File/UploadImage", data) };
