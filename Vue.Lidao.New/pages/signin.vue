<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-25 14:00:48
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-19 16:32:02
 * @FilePath: \project\pages\signin.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <Top />
    <div class="login-wrap ">
      <el-form class="login-container log_cen">
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
      </el-form>
      <el-form class="login-container log">
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
          <el-form-item label="你是...">
            <el-radio-group v-model="form.resource" fill="red">
              <el-radio label="1"  border>医疗人员</el-radio>
              <el-radio label="2"  border>心理疾病患者</el-radio>
              <el-radio label="3"  border>自救寻求着</el-radio>
            </el-radio-group>
          </el-form-item>
          <p style="color: #606266">你正在面临的问题...</p>
          <el-form-item>
            <el-checkbox-group v-model="form.resource2">
              <el-checkbox label="1" name="type" border>心情低落/极端</el-checkbox>
              <el-checkbox label="2" name="type" border>自我迷茫</el-checkbox>
              <el-checkbox label="3" name="type" border>知识欠缺</el-checkbox>
              <el-checkbox label="4" name="type" border>没有特别问题</el-checkbox>
            </el-checkbox-group>
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              @click="doLogin"
              style="width: 100%"
            >提交</el-button>
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
              Personal: that.form.resource,
              UserComment: that.form.resource2,
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
  <style scoped>
.bac {
  background: linear-gradient(284.44deg, #ffd0bc 1.62%, #ffffff 103.96%),
    #ffffff;
  height: 98vh;
  overflow: hidden;
  position: relative;
}
.el-button--primary {
  background: #ff5734;
  border-color: #ff5734;
}
.login-wrap {
  box-sizing: border-box;
  width: 100%;
  height: 100%;
  padding-top: 60px;
  background-repeat: no-repeat;
  background-position: center right;
  background-size: 100%;
  display: flex;
  margin-left: 500px;
}

.login-container {
  /* margin: 0px auto; */
  width: 400px;
  padding: 30px 35px 15px 35px;
  text-align: left;
}
.log{
  margin-top: 82px;
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
.el-radio.is-bordered,.el-checkbox.is-bordered{
  margin: 10px;
}
.log_cen .el-form-item{
  margin-bottom:45px
}
.el-radio.is-bordered.is-checked{
  border-color: #FF5734 !important;
}
.el-radio__input.is-checked+.el-radio__label{
  color: #FF5734 !important;
}
/* 选中后小圆点的颜色 */

.el-radio__input.is-checked .el-radio__inner{
  background: #FF5734 !important;
  border-color: #FF5734 !important;
}


</style>
  
  