import { post, other } from './axiosFun';

/**
 * 支付配置信息
 **/
// 支付配置信息-获取支付配置信息列表
export const MachineConfigList = (data) => { return post("/Api/MachineConfig/list", data) };
// 支付配置信息-保存支付配置信息
export const MachineConfigSave = (data) => { return post("/Api/MachineConfig/save", data) };
// 支付配置信息-删除支付配置信息
export const MachineConfigDelete = (params) => { return other("delete", "/Api/MachineConfig/delete", params) };

/**
 * 支付配置
 **/
// 支付配置-获取支付配置列表
export const ConfigList = (data) => { return post("/Api/Config/list", data) };
// 支付配置-保存支付配置
export const ConfigSave = (data) => { return post("/Api/Config/save", data) };
// 支付配置-删除支付配置
export const ConfigDelete = (params) => { return other("delete", "/Api/Config/delete", params) };

/**
 * 订单管理-交易订单
 **/
// 交易订单-获取交易订单列表
export const OrderList = (data) => { return post("/Api/Order/list", data) };
// 交易订单-s删除交易订单
export const OrderDelete = (params) => { return other("delete", "/Api/Order/delete", params) };
// 交易订单-交易订单退款
export const OrderRefund = (data) => { return post("/Api/Order/refund", data) };
