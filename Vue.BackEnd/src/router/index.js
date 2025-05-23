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
import island from '@/views/system/island';
import article from '@/views/system/article';
import post from '@/views/system/post';
import reply from '@/views/system/reply';
import blackword from '@/views/system/blackword';
import teamMember from '@/views/system/teamMember';

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
      component: island,
      meta: {
        title: "岛群管理",
        requireAuth: true
      }
    },
    {
      path: '/system/article',
      name: '文章管理',
      component: article,
      meta: {
        title: "文章管理",
        requireAuth: true
      }
    },
    {
      path: '/system/post',
      name: '帖子管理',
      component: post,
      meta: {
        title: "帖子管理",
        requireAuth: true
      }
    },
    {
      path: '/system/reply',
      name: '评论管理',
      component: reply,
      meta: {
        title: "评论管理",
        requireAuth: true
      }
    },
    {
      path: '/system/blackword',
      name: '屏蔽关键词管理',
      component: blackword,
      meta: {
        title: "屏蔽关键词管理",
        requireAuth: true
      }
    },
    {
      path: '/system/teamMember',
      name: '团队成员管理',
      component: teamMember,
      meta: {
        title: "团队成员管理",
        requireAuth: true
      }
    }]
  }]
})
