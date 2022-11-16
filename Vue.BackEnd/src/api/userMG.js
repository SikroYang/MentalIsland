import { post } from './axiosFun';

// 登录接口
export const login = (data) => { return post("/Api/Auth/Login", data,) };
// 登出接口
export const loginout = () => { return post("/Api/Auth/Logout") };

/**
 * 用户管理
 **/
// 用户管理-获取当前登录用户
export const userInfo = (data) => { return post("/Api/User/UserInfo", data) };
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
