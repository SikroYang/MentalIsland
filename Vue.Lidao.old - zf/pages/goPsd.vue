<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-25 14:00:48
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-18 14:10:17
 * @FilePath: \project\pages\signin.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="login-wrap">
      <el-form class="login-container">
        <h1 class="title">忘记密码</h1>
        
        <el-form-item>
          <el-input
            type="text"
            v-model="form.email"
            prefix-icon="el-icon-user-solid"
            placeholder="邮箱"
            autocomplete="off"
          ></el-input>
        </el-form-item>
      
        
        <el-form-item>
          <el-input
            type="password"
            v-model="form.password"
            prefix-icon="el-icon-user-solid"
            placeholder="新密码(必须以字母开头，只能包含字母、数字和下划线)"
            autocomplete="off"
          ></el-input>
        </el-form-item>
       

        <el-form-item>
          <div class="flex">
            <div style="width: 65%; margin-right: 5%">
              <el-input
                type="text"
                v-model="form.verification"
                prefix-icon="el-icon-user-solid"
                placeholder="验证码"
                autocomplete="off"
              ></el-input>
            </div>
            <el-button plain @click="code">获取验证码</el-button>
          </div>
        </el-form-item>
        <el-form-item>
          <el-button
            type="primary"
            icon="el-icon-right"
            @click="doLogin"
            style="width: 100%"
          ></el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
export default {
  name: "signinPage",
  components: { Top },
  data: function () {
    return {
      form: {
        password: "",
        email: "",
        verification: "",
      },
      eCode: "",
    };
  },

  created() {},
  methods: {
    doLogin() {
      let that = this;
      console.log(that.form, that.eCode);
      if (that.form.verification !== "") {
        if (that.form.verification == that.eCode) {
            let data = {
              Email: that.form.email,
              VerifyCode: that.form.verification,
              NewPassword: that.form.password,
            };
            that.$axios.post("/Api/Auth/ForgetPwd", data).then((res) => {
              console.log(res);
              if (res.data.Code === 200) {
                this.$message.success(res.data.Data);
                this.$router.push("/login");
              }
            });
        } else {
          this.$message.error("错了哦，验证码输入错误");
        }
      } else {
        this.$message.error("错了哦，请正确输入");
      }
    },
    code() {
      let that = this;
      console.log(that.form.verification);
      that.$axios
        .get("Api/Auth/VerifyCode", {
          params: {
            MailAddr: that.form.email,
          },
        })
        .then((res) => {
          if (res.data.Code === 200) {
            console.log(res.data.Data);
            that.eCode = res.data.Data;
            this.$message.success("验证码已发送至邮箱");
          } else {
            this.$message.error("错了哦，请输入正确邮箱");
          }
        });
    },
  },
};
</script>
  <style>
.login-wrap {
  box-sizing: border-box;
  width: 100%;
  height: 100%;
  padding-top: 60px;
  background-repeat: no-repeat;
  background-position: center right;
  background-size: 100%;
}

.login-container {
  border-radius: 10px;
  margin: 0px auto;
  width: 400px;
  padding: 30px 35px 15px 35px;
  background: #fff;
  border: 1px solid #eaeaea;
  text-align: left;
  box-shadow: 0 0 20px 2px rgba(0, 0, 0, 0.1);
}

.title {
  margin: 0px auto 40px auto;
  text-align: center;
  color: #505458;
}
.el-select {
  display: block;
}
.flex {
  display: flex;
}
</style>
  
  