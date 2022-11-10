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
