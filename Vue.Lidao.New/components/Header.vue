<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-11-04 14:47:39
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-30 10:07:27
 * @FilePath: \qundao\qundao\components\Header.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="header">
    <div>
      <img style="height: 60px" src="../assets/new/LOGO.png" alt="" srcset="" />
    </div>
    <el-menu
      background-color="#fff"
      text-color="#000000"
      active-text-color="#000000"
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
        <el-button @click="login">登入</el-button>
        <!-- <el-button type="warning" @click="sign">注册</el-button> -->
      </el-row>
    </div>
    <div class="flex" v-if="show">
      <div class="block mar-r">
        <img
          v-if="imgs"
          :src="imgs"
          alt=""
          style="width: 40px; height: 40px; border-radius: 50%"
        />
        <img
          v-else
          src="../assets/new/tx.png"
          alt=""
          style="width: 40px; height: 40px; border-radius: 50%"
        />
      </div>
      <el-dropdown trigger="click">
        <span class="el-dropdown-link">
          {{ fullName }}
          <i class="el-icon-arrow-down el-icon--right"></i>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item @click.native="user">用户设置</el-dropdown-item>
          <el-dropdown-item @click.native="user_b">群岛设置</el-dropdown-item>
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
        { name: "", navItem: "活动展览" },
        { name: "", navItem: "我们的团队" },
        // { name: "/exhibition", navItem: "活动展览" },
        // { name: "/team", navItem: "我们的团队" },
      ],
      isShow: true,
      show: false,
      userList: [],
      fullName: "",
      imgs: "",
    };
  },

  created() {
    this.code();
  },
  mounted() {
    this.userList = this.$cookies.get("user");
    if (this.userList && this.userList != undefined) {
      console.log(this.imgs);
      if (this.userList.HeadImage && this.userList.HeadImage != undefined)
        this.imgs = this.userList.HeadImage;

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
      this.$router.push({
        path: "./user",
        query: { id: "a" },
      });
    },
    user_b() {
      this.$router.push({
        path: "./user",
        query: { id: "b" },
      });
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
          this.$cookies.remove("user");
          this.isShow = true;
          this.show = false;
          this.$router.push("/login");
        } else if (res.data.Code === 401) {
          this.$message.success(res.data.Content);
          this.$cookies.remove("user");
          this.$router.push("/login");
        }
      });
    },
    code() {
      let that = this;
      that.$axios.post("/Api/User/UserInfo").then((res) => {
        // console.log(res.data.Data)
        if (res.data.Code == 200) this.$cookies.set("user", res.data.Data);
        if (res.data.Code === 401) {
          this.$cookies.remove("user");
        }
      });
    },
  },
};
</script>
<style>
.el-button--warning {
  background: #f7bc99;
  border-color: #f7bc99;
}

.el-button--warning:hover {
  background: #f5d7b9;
  border-color: #f5d7b9;
  color: #fff;
}

.el-menu--horizontal > .el-menu-item.is-active {
  border-bottom: 2px solid black;
}

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
  /* background: linear-gradient(284.44deg, #ffd0bc 1.62%, #ffffff 103.96%),
    #ffffff; */
  background: #fff;
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
  /* justify-content: center; */
}

.mar-r {
  margin-right: 10px;
}
</style>
