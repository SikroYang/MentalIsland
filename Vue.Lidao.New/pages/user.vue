<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-30 10:36:28
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <Top />
    <div class="content_y">
      <el-tabs
        :tab-position="tabPosition"
        :stretch="true"
        style="height: 700px"
        v-model="activeName"
      >
        <el-tab-pane label="个人信息" name="a">
          <el-form ref="form" :model="form" label-width="80px">
            <el-form-item label="用户名">
              <el-input v-model="form.name" style="width: 220px"></el-input>
            </el-form-item>
            
            <el-form-item label="手机号">
              <el-input v-model="form.phone" style="width: 220px"></el-input>
            </el-form-item>
            <el-form-item label="用户头像">
              <el-upload
                class="avatar-uploader"
                action="/Api/File/UploadImage"
                :show-file-list="false"
                :on-success="handleAvatarSuccess"
                v-model="form.portrait"
              >
                <img v-if="imageUrl" :src="imageUrl" class="avatar" />
                <i v-else class="el-icon-plus avatar-uploader-icon"></i>
              </el-upload>
            </el-form-item>
            <el-form-item label="修改邮箱">
              <el-input v-model="form.email" style="width: 220px"></el-input>
            </el-form-item>
            <el-form-item label="当前密码">
              <el-input v-model="form.psd" style="width: 220px"></el-input>
            </el-form-item>
              <el-form-item label="所属国家">
              <el-select v-model="form.region" placeholder="Select an option">
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
                    <span
                      style="float: right; color: #8492a6; font-size: 13px"
                      >{{ item.value }}</span
                    >
                  </el-option>
                </el-option-group>
              </el-select>
            </el-form-item>
            <el-form-item label="注册类型">
              <el-radio-group v-model="form.resource">
                <el-radio label="1" border>医疗人员</el-radio>
                <el-radio label="2" border>心理疾病患者</el-radio>
                <el-radio label="3" border>自救寻求着</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="备注">
              <el-checkbox-group v-model="form.resource2">
                <el-checkbox label="1" name="type" border>心情低落/极端</el-checkbox>
                <el-checkbox label="2" name="type" border>自我迷茫</el-checkbox>
                <el-checkbox label="3" name="type" border>知识欠缺</el-checkbox>
                <el-checkbox label="4" name="type" border>没有特别问题</el-checkbox>
              </el-checkbox-group>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onSubmit">保存</el-button>
            </el-form-item>
          </el-form>
        </el-tab-pane>
        <!-- <el-tab-pane label="修改密码">
          <el-form ref="form2" :model="form2" label-width="90px">
            <el-form-item label="当前密码">
              <el-input v-model="form2.psd" style="width: 220px"></el-input>
            </el-form-item>
            <el-form-item label="新密码">
              <el-input
                type="password"
                v-model="form2.newPsd"
                style="width: 220px"
              ></el-input>
            </el-form-item>
            <el-form-item label="确认新密码">
              <el-input
                type="password"
                v-model="form2.newPsd2"
                style="width: 220px"
              ></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onSubmit2">保存</el-button>
            </el-form-item>
          </el-form>
        </el-tab-pane> -->
        <el-tab-pane label="我的群岛" name="b">
          <div class="qd">
            <ul>
              <li v-for="(item, i) in qdList" :key="i">
                <img
                  src="https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png"
                  alt=""
                  srcset=""
                />
                <p>
                  <el-button
                    type="primary"
                    style="width: 120px"
                    @click="dialog(item.Id)"
                    >{{ item.Name }}</el-button
                  >
                </p>
              </li>
            </ul>
          </div>
          <el-dialog title="群岛设置" :visible.sync="dialogVisible" width="30%">
            <el-form ref="form3" :model="form3" label-width="90px">
              <el-form-item label="群岛名称">
                <el-input v-model="form3.nickName"></el-input>
              </el-form-item>
              <el-form-item label="群岛内容">
                <el-input type="textarea" v-model="form3.nickDesc"></el-input>
              </el-form-item>
              <!-- <el-form-item label="QQ群">
                <el-input v-model="form3.qq"></el-input>
              </el-form-item> -->
            </el-form>
            <span slot="footer" class="dialog-footer">
              <el-button type="primary" @click="information()"
                >更新信息</el-button
              >
            </span>
          </el-dialog>
        </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>
  
  <script>
import { country } from "./china.js";
import Top from "../components/Header.vue";
export default {
  name: "userPage",
  components: { Top },
  data() {
    return {
      tabPosition: "left",
      activeName:'a',
      country,
      form: {
        name: "",
        phone: "",
        region: "",
        portrait: "",
        resource: "",
        resource2: [],
        email: "",
        psd: "",
      },
      form2: {
        psd: "",
        newPsd: "",
        newPsd2: "",
      },
      form3: {
        nickName: "",
        nickDesc: "",
        qq: "",
        id: "",
      },
      imageUrl: "",
      img:"",
      qdList: [],
      dialogVisible: false,
    };
  },
  created() {
    console.log(this.$cookies.get("user").Id);
    this.userList();
    this.qdLists();
    this.activeName=this.$route.query.id
    console.log(this.$route.query.id)
  },
  methods: {
    onSubmit() {
      let that = this;
      console.log(that.form);
      this.$axios
        .post("/Api/User/AddOrUpdateUser", {
          Id: this.$cookies.get("user").Id,
          Password: that.form.psd,
          Email: that.form.email,
          PhoneNumber: that.form.phone,
          FullName: that.form.name,
          Country: that.form.region,
          HeadImage:that.img,
          Personal:that.form.resource,
          UserComment:that.form.resource2,
        })
        .then((res) => {
          if (res.data.Code === 200) {
            this.$message.success("恭喜你，用户信息修改成功");
          } else {
            this.$message.error("错了哦，用户信息修改失败");
          }
        });
    },
    // onSubmit2() {
    //   let that = this;
    //   console.log(that.form2);
    // },
    handleAvatarSuccess(res, file) {
      this.imageUrl = URL.createObjectURL(file.raw);
      console.log(file.response.Data);
      this.img=file.response.Data
    },
    userList() {
      let that = this;
      this.$axios
        .post("/Api/User/UserDetail", { Id: this.$cookies.get("user").Id })
        .then((res) => {
          if (res.data.Code === 200) {
            console.log(res.data.Data.Email);
            that.form.name = res.data.Data.FullName;
            that.form.phone = res.data.Data.PhoneNumber;
            that.form.region = res.data.Data.Country;
            // that.form.portrait=res.data.Data.
            that.imageUrl=res.data.Data.HeadImage
            that.form.email = res.data.Data.Email;
            let personal=res.data.Data.Personal
            that.form.resource=personal.toString();
            that.form.resource2= res.data.Data.UserComment.map(String)
          }
        });
    },
    qdLists(a, b) {
      //群岛列表
      let that = this;
      this.$axios.post("/Api/Island/GetUserCreatedIsland").then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          that.qdList = res.data.Data;
        }
      });
    },
    dialog(e) {
      let that = this;
      that.dialogVisible = true;
      let arr = that.qdList.filter((i) => {
        return e == i.Id;
      });
      arr.forEach(function (item, index) {
        that.form3.nickName = item.Name;
        that.form3.nickDesc = item.Description;
        that.form3.qq = item.QQunNumber;
        that.form3.id = item.Id;
      });
    },
    information() {
      let that = this;
      console.log(that.form3);
      this.$axios
        .post("/Api/Island/AddOrUpdateIsland", {
          Id: that.form3.id,
          Name: that.form3.nickName,
          Description: that.form3.nickDesc,
          QQunNumber: that.form3.qq,
        })
        .then((res) => {
          if (res.data.Code === 200) {
            this.$message.success("恭喜你，群岛修改成功");
            that.dialogVisible = false;
            that.qdLists();
          } else {
            this.$message.error("错了哦，群岛修改失败");
          }
        });
    },
  },
};
</script >
  <style  scoped>
 .bac {
  background: linear-gradient(284.44deg, #ffd0bc 1.62%, #ffffff 103.96%),
    #ffffff;
  height: 98vh;
  overflow: hidden;
}
  
  .el-button--primary {
    background: #ff5734;
  border-color: #ff5734;
}
.el-input__inner {
  background-color: #f7eeed;
  border: 1px solid #f7eeed;
}
.content_y {
  width: 50%;
  margin: auto;
  margin-top: 50px;
  padding: 20px;
  height: 700px;
}
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}

.qd ul li {
  list-style: none;
  float: left;
  margin: 20px;
}
.qd ul li img {
  width: 120px;
  height: 120px;
}
::v-deep .el-radio {
  border: 1px solid #cfcccc;
}

::v-deep .el-radio.is-checked {
  border: 1px solid #FF5734 !important;
}
::v-deep .el-radio .el-radio__input {
  margin-bottom: px(5);
}

::v-deep .el-radio .el-radio__input.is-checked .el-radio__inner {
  background-color: #FF5734;
  border-color: #FF5734;
}

::v-deep .el-radio .el-radio__input.is-checked+.el-radio__label {
  color: #FF5734 !important;
}

::v-deep .el-radio .el-radio__input .el-radio__inner:hover {
  border-color: #FF5734;
}
/* 多选框 */

::v-deep .el-checkbox {
  border: 1px solid #cfcccc;
}

::v-deep .el-checkbox.is-checked {
  border: 1px solid #FF5734 !important;
}
::v-deep .el-checkbox .el-checkbox__input {
  margin-bottom: px(5);
}

::v-deep .el-checkbox .el-checkbox__input.is-checked .el-checkbox__inner {
  background-color: #FF5734;
  border-color: #FF5734;
}

::v-deep .el-checkbox .el-checkbox__input.is-checked+.el-checkbox__label {
  color: #FF5734 !important;
}

::v-deep .el-checkbox .el-checkbox__input .el-checkbox__inner:hover {
  border-color: #FF5734;
}

</style>
  
  