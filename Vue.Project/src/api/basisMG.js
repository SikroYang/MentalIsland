import { post, other } from './axiosFun';

/**
 * 商品管理
 **/
// 商品管理-获取商品管理列表
export const GoodsList = (data) => { return post("/Api/Goods/list", data) };
// 商品管理-保存商品管理
export const GoodsSave = (data) => { return post("/Api/Goods/save", data) };
// 商品管理-删除商品管理
export const GoodsDelete = (data) => { return other("delete", "/Api/Goods/delete", data) };

/**
 * 机器信息管理
 **/
// 机器信息管理-获取机器信息管理列表
export const MachineList = (data) => { return post("/Api/Machine/list", data) };
// 机器信息管理-保存机器信息管理
export const MachineSave = (data) => { return post("/Api/Machine/save", data) };
// 机器信息管理-删除机器信息管理
export const MachineDelete = (data) => { return other("delete", "/Api/Machine/delete", data) };

/**
 * 货道信息管理
 **/
// 货道信息管理-获取获取货道信息管理列表
export const MachineAisleList = (data) => { return post("/Api/MachineAisle/list", data) };
// 货道信息管理-删除货道信息管理
export const MachineAisleDelete = (data) => { return other("delete", "/Api/MachineAisle/delete", data) };
// 货道信息管理-保存货道信息管理
export const MachineAisleRsave = (data) => { return post("/Api/MachineAisle/save", data) };
