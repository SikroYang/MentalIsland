<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-15 16:56:00
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="content_y">
      <el-tabs
        :tab-position="tabPosition"
        :stretch="true"
        style="height: 600px"
      >
        <el-tab-pane label="个人信息">
          <el-form ref="form" :model="form" label-width="80px">
            <el-form-item label="用户名">
              <el-input v-model="form.name" style="width: 220px"></el-input>
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
            <el-form-item label="手机号">
              <el-input v-model="form.phone" style="width: 220px"></el-input>
            </el-form-item>
            <!-- <el-form-item label="用户头像">
              <el-upload
                class="avatar-uploader"
                action="https://jsonplaceholder.typicode.com/posts/"
                :show-file-list="false"
                :on-success="handleAvatarSuccess"
                v-model="form.portrait"
              >
                <img v-if="imageUrl" :src="imageUrl" class="avatar" />
                <i v-else class="el-icon-plus avatar-uploader-icon"></i>
              </el-upload>
            </el-form-item> -->
            <el-form-item label="修改邮箱">
              <el-input v-model="form.email" style="width: 220px"></el-input>
            </el-form-item>
            <el-form-item label="当前密码">
              <el-input v-model="form.psd" style="width: 220px"></el-input>
            </el-form-item>
            <!-- <el-form-item label="注册类型">
              <el-radio-group v-model="form.resource">
                <el-radio label="医疗人员"></el-radio>
                <el-radio label="心理疾病患者"></el-radio>
                <el-radio label="自救寻求着"></el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="备注">
              <el-checkbox-group v-model="form.resource2">
                <el-checkbox label="心情低落/极端" name="type"></el-checkbox>
                <el-checkbox label="自我迷茫" name="type"></el-checkbox>
                <el-checkbox label="知识欠缺" name="type"></el-checkbox>
                <el-checkbox label="没有特别问题" name="type"></el-checkbox>
              </el-checkbox-group>
            </el-form-item> -->
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
        <el-tab-pane label="我的群岛">
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
      country,
      form: {
        name: "",
        phone:"",
        region: "",
        portrait: "",
        resource: "",
        resource2: [],
        email: "",
        psd:""
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
        id:''
      },
      imageUrl: "",
      qdList: [],
      dialogVisible: false,
    };
  },
  created() {
    console.log(this.$cookies.get("user").Id);
    this.userList();
    this.qdLists();
  },
  methods: {
    onSubmit() {
      let that = this;
      console.log(that.form);
      this.$axios
        .post("/Api/User/AddOrUpdateUser", { Id: this.$cookies.get("user").Id,Password:that.form.psd,Email:that.form.email,PhoneNumber:that.form.phone,FullName:that.form.name,Country:that.form.region})
        .then((res) => {
          if (res.data.Code === 200) {
            this.$message.success("恭喜你，用户信息修改成功");
          }else {
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
      console.log(file);
    },
    userList() {
      let that = this;
      this.$axios
        .post("/Api/User/UserDetail", { Id: this.$cookies.get("user").Id })
        .then((res) => {
          if (res.data.Code === 200) {
            console.log(res);
            that.form.name = res.data.Data.FullName;
            that.form.phone= res.data.Data.PhoneNumber;
            that.form.region = res.data.Data.Country;
            // that.form.portrait=res.data.Data.
            // that.form.resource=res.data.Data.
            // that.form.resource2=res.data.Data.
            that.form.email = res.data.Data.Email;
          }
        });
    },
    qdLists(a, b) {
      //群岛列表
      let that = this;
      this.$axios
        .post("/Api/Island/GetUserCreatedIsland")
        .then((res) => {
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
    information(){
        let that = this;
        console.log(that.form3)
        this.$axios
        .post("/Api/Island/AddOrUpdateIsland", { Id: that.form3.id, Name: that.form3.nickName,Description:that.form3.nickDesc,QQunNumber:that.form3.qq })
        .then((res) => {
          if (res.data.Code === 200) {
            this.$message.success("恭喜你，群岛修改成功");
            that.dialogVisible = false;
            that.qdLists()
          }else {
          this.$message.error("错了哦，群岛修改失败");
        }
        });
        
    }
  },
};
</script>
  <style>
.content_y {
  width: 50%;
  margin: auto;
  margin-top: 50px;
  border: 1px solid #ccc;
  padding: 20px;
  border-radius: 5px;
  height: 600px;
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
</style>
  
  