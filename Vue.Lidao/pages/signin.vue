<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-25 14:00:48
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-08 16:17:30
 * @FilePath: \project\pages\signin.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="login-wrap">
      <el-form class="login-container">
        <h1 class="title">注册</h1>
        <el-form-item>
          <el-input
            type="text"
            v-model="form.username"
            prefix-icon="el-icon-user-solid"
            placeholder="用户名"
            autocomplete="off"
          ></el-input>
        </el-form-item>
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
            type="text"
            v-model="form.phone"
            prefix-icon="el-icon-user-solid"
            placeholder="手机号"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-select v-model="form.region" placeholder="选择区域">
            <el-option-group
              v-for="group in country"
              :key="group.label"
              :label="group.label"
            >
              <el-option
                v-for="item in group.options"
                :key="item.value"
                :label="item.label"
                :value="item.label"
              >
                <span style="float: left">{{ item.label }}</span>
                <span style="float: right; color: #8492a6; font-size: 13px">{{
                  item.value
                }}</span>
              </el-option>
            </el-option-group>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-input
            type="password"
            v-model="form.password"
            prefix-icon="el-icon-user-solid"
            placeholder="密码(必须以字母开头，只能包含字母、数字和下划线)"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            type="password"
            v-model="form.confirmation"
            prefix-icon="el-icon-user-solid"
            placeholder="确认密码"
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
        <!-- <el-form-item label="你是...">
          <el-radio-group v-model="form.resource">
            <el-radio label="医疗人员"></el-radio>
            <el-radio label="心理疾病患者"></el-radio>
            <el-radio label="自救寻求着"></el-radio>
          </el-radio-group>
        </el-form-item>
        <p style="color: #606266">你正在面临的问题...</p>
        <el-form-item>
          <el-checkbox-group v-model="form.resource2">
            <el-checkbox label="心情低落/极端" name="type"></el-checkbox>
            <el-checkbox label="自我迷茫" name="type"></el-checkbox>
            <el-checkbox label="知识欠缺" name="type"></el-checkbox>
            <el-checkbox label="没有特别问题" name="type"></el-checkbox>
          </el-checkbox-group>
        </el-form-item> -->

        <el-row style="text-align: center; margin-top: -10px">
          <p style="color: #8a8c8e">
            通过创建帐户，您同意我们的服务条款和隐私政策
          </p>
        </el-row>
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
import { country } from "./china.js";
import Top from "../components/Header.vue";
export default {
  name: "signinPage",
  components: { Top },
  data: function () {
    return {
      form: {
        username: "",
        password: "",
        email: "",
        phone: "",
        region: "",
        confirmation: "",
        verification: "",
        resource: "",
        resource2: [],
      },
      country,
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
          if (that.form.confirmation == that.form.password) {
            let data = {
              FullName: that.form.username,
              Email: that.form.email,
              PhoneNumber: that.form.phone,
              Country: that.form.region,
              VerifyCode: that.form.verification,
              Password: that.form.password,
            };
            that.$axios.post("/Api/Auth/Register", data).then((res) => {
              console.log(res);
              if (res.data.Code === 200) {
                this.$message({
                  message: "恭喜你，注册成功",
                  type: "success",
                });
                this.$router.push("/login");
              }
            });
          } else {
            this.$message.error("错了哦，两次密码不一致");
          }
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
  
  