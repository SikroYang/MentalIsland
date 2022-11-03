import { get, post, other } from './axiosFun';

// 登录接口
export const login = (data) => { return post("/Api/Auth/Login", data,) };
// 获取用户菜单
// export const menu = (params) => { return get("/Api/menu", params) };
// 退出接口
export const loginout = () => { return post("/Api/Auth/Logout") };

/**
 * 用户管理
 **/
// 用户管理-获取用户列表
export const userList = (data) => { return post("/Api/User/List", data) };
// 用户管理-保存（添加编辑）
export const userSave = (data) => { return post("/Api/User/AddOrUpdateUser", data) };
// 用户管理-删除用户
export const userDelete = (data) => { return post("/Api/User/DeleteUser", data) };
// 用户管理-重置密码
export const userPwd = (data) => { return post("/Api/User/ResetPwd", data) };
// 用户管理-修改状态
export const userLock = (data) => { return post("/Api/User/LockUser", data) };
// 用户管理-用户下线
export const userExpireToken = (params) => { return get("/Api/User/expireToken/", params) };

/**
 * 系统环境变量
 **/
// 系统环境变量-获取系统环境变量列表
export const variableList = (data) => { return post("/Api/Variable/list", data) };
// 系统环境变量-保存（添加编辑）
export const variableSave = (data) => { return post("/Api/Variable/save", data) };
// 系统环境变量-删除系统环境变量
export const variableDelete = (params) => { return other("delete", "/Api/Variable/delete", params) };

/**
 * 权限管理
 **/
// 权限管理-获取权限列表
export const permissionList = (data) => { return post("/Api/Permission/list", data) };
// 权限管理-保存权限
export const ermissionSave = (data) => { return post("/Api/Permission/save", data) };
// 权限管理-删除权限
export const ermissionDelete = (params) => { return other("delete", "/Api/Permission/delete", params) };
// 权限管理-获取权限
export const roleDropDown = () => { return get("/Api/Role/dropDown/all") };
// 权限管理-配置权限
export const RolePermission = (data) => { return post("/Api/RolePermission/save", data) };
