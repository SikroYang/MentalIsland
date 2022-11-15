<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-11-04 14:47:39
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-10 16:02:52
 * @FilePath: \qundao\qundao\components\Header.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="header">
    <el-menu
      :default-active="this.$route.path"
      class="el-menu-demo"
      mode="horizontal"
      router
      
      style="margin-left: auto"
    >
      <el-menu-item v-for="(item, i) in navList" :key="i" :index="item.name">
        <template slot="title">
          <span> {{ item.navItem }}</span>
        </template>
      </el-menu-item>
    </el-menu>
    <div v-if="isShow">
      <el-row>
        <el-button type="warning" @click="login">登入</el-button>
        <el-button type="warning" @click="sign">注册</el-button>
      </el-row>
    </div>
    <div class="flex" v-if="show">
      <div class="block mar-r">
        <el-avatar :size="40" :src="circleUrl"></el-avatar>
      </div>
      <el-dropdown trigger="click">
        <span class="el-dropdown-link">
          {{ fullName }}
          <i class="el-icon-arrow-down el-icon--right"></i>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item @click.native="user"
            >用户/群岛设置</el-dropdown-item
          >
          <el-dropdown-item @click.native="archipelago"
            >退出登录</el-dropdown-item
          >
        </el-dropdown-menu>
      </el-dropdown>
    </div>
  </div>
</template>
<script>
export default {
  name: "Header",
  data: function () {
    return {
      navList: [
        { name: "/home", navItem: "首页" },
        { name: "/community", navItem: "群岛社区" },
        { name: "/psychology", navItem: "心理科普" },
        { name: "/exhibition", navItem: "过去活动+展览" },
        { name: "/team", navItem: "我们的团队" },
      ],
      circleUrl:
        "https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png",
      isShow: true,
      show: false,
      userList: [],
      fullName: "",
    };
  },

  created() {
   
  },
  mounted() {
    this.userList = this.$cookies.get("user");
    // console.log(this.userList);
    // console.log(this.$cookies.get("user"));
    if (this.userList != null) {
      this.fullName = this.userList.FullName;
      this.isShow = false;
      this.show = true;
    } else {
      this.isShow = true;
      this.show = false;
    }
  },
  methods: {
    login() {
      this.$router.push("/login");
    },
    sign() {
      this.$router.push("/signin");
    },
    user() {
      this.$router.push("/user");
    },
    archipelago() {
      let that = this;
      that.$axios.post("/Api/Auth/Logout").then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.$message({
            message: "退出成功",
            type: "success",
          });
          this.$router.push("/");
          this.$cookies.remove("user");
          this.isShow = true;
          this.show = false;
        }
      });
    },
  },
};
</script>
<style>
.el-menu-item {
  font-weight: bold;
}
/* .el-menu-item.is-active {
  color: #ea5b2c !important;
} */
.el-menu.el-menu--horizontal {
  border: 0;
}
.header {
  display: flex;
  width: 100%;
  align-items: center;
  background-color: #fff;
  margin-bottom: 20px;
}
.el-dropdown {
  vertical-align: top;
}
.el-dropdown + .el-dropdown {
  margin-left: 15px;
}
.el-icon-arrow-down {
  font-size: 14px;
}
.el-dropdown-link {
  cursor: pointer;
  color: #9f7861;
}

.flex {
  display: flex;
  align-items: center;
  justify-content: center;
}
.mar-r {
  margin-right: 10px;
}
</style>
