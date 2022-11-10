// 导入组件
import Vue from 'vue';
import Router from 'vue-router';
// 登录
import login from '@/views/login';
// 首页
import index from '@/views/index';
/**
 * 基础菜单
 */
/**
 * 系统管理
 */
// 用户管理
import user from '@/views/system/user';

// 启用路由
Vue.use(Router);

// 导出路由
export default new Router({
  routes: [{
    path: '/',
    name: '',
    component: login,
    hidden: true,
    meta: {
      requireAuth: false
    }
  }, {
    path: '/login',
    name: '登录',
    component: login,
    hidden: true,
    meta: {
      requireAuth: false
    }
  }, {
    path: '/index',
    name: '首页',
    component: index,
    iconCls: 'el-icon-tickets',
    children: [{
      path: '/system/user',
      name: '用户管理',
      component: user,
      meta: {
        title: "用户管理",
        requireAuth: true
      }
    },
    {
      path: '/system/island',
      name: '岛群管理',
      component: user,
      meta: {
        title: "岛群管理",
        requireAuth: true
      }
    }]
  }]
})
