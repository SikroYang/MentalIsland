<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-22 15:50:47
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <Top />
    <div class="login-wrap">
      <el-form class="login-container">
        <h1 class="title">登陆</h1>
        <el-form-item>
          <el-input
            type="text"
            v-model="username"
            prefix-icon="el-icon-user-solid"
            placeholder="手机号/邮箱登录"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            type="password"
            v-model="password"
            prefix-icon="el-icon-user-solid"
            placeholder="密码"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-row
          style="
            margin-top: -10px;
            margin-bottom: 10px;
          "
        >
          <el-link type="primary" @click="doRegister">注册账号</el-link>
          <el-link style="float: right;" type="primary" @click="goPsd">忘记密码</el-link>
        </el-row>
        <el-form-item>
          <el-button type="primary" @click="doLogin" style="width: 100%"
            >登录</el-button
          >
        </el-form-item>
      </el-form>
    </div>
    <div class="group">
      <img class="gr1" src="../assets/new/Group.png" alt="" />
    </div>
    <div>
      <img class="gr2" src="../assets/new/Vector.png" alt="" srcset="" />
    </div>
  </div>
</template>

<script>
import Top from "../components/Header.vue";
export default {
  name: "LoginPage",
  components: { Top },
  data: function () {
    return {
      username: "",
      password: "",
    };
  },
  created() {
    this.code();
  },
  methods: {
    doLogin() {
      let that = this;
      console.log(that.username, that.password);
      let data = { UserName: that.username, Password: that.password };
      that.$axios.post("/Api/Auth/Login", data).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.$cookies.set("user", res.data.Data);
          this.$router.push("/home");
        } else {
          this.$message.error(res.data.Content);
        }
      });
    },
    doRegister() {
      this.$router.push("/signin");
    },
    goPsd() {
      this.$router.push("/goPsd");
    },
    code() {
      let that = this;
      that.$axios.post("/Api/User/UserInfo").then((res) => {
        if (res.data.Code == 200) {
          this.$cookies.set("user", res.data.Data);
          this.$router.push("/home");
        }
        if (res.data.Code === 401) {
          this.$cookies.remove("user");
        }
      });
    },
  },
};
</script>
<style scoped>
.bac {
  background: linear-gradient(284.44deg, #ffd0bc 1.62%, #ffffff 103.96%),
    #ffffff;
  height: 98vh;
  overflow: hidden;
  position: relative;
}
.login-wrap {
  box-sizing: border-box;
  width: 100%;
  height: 100%;
  padding-top: 4%;
  background-repeat: no-repeat;
  background-position: center right;
  background-size: 100%;
}
.el-button--primary {
  background: #ff5734;
  border-color: #ff5734;
}
.el-link.el-link--primary {
  color: #862d0b;
}
.login-container {
  margin: 0px auto;
  width: 350px;
  padding: 30px 35px 15px 35px;
  text-align: left;
}

.title {
  margin: 0px auto 40px auto;
  text-align: center;
  color: #505458;
}
.group {
  position: absolute;
}
.gr1 {
  bottom: 645px;
  width: 245px;
  position: absolute;
  z-index: 999;
}
.gr2 {
  bottom: 0px;
  right: 0;
  width: 160px;
  position: absolute;
  z-index: 999;
}
</style>

