<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-30 18:35:16
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="body">
    <Top />

    <div class="com-cont">
      <h3>公共群岛</h3>
      <div class="mar-b" style="width: 100%; margin: auto">
        <el-input
          placeholder="请输入内容"
          v-model="reInput"
          class="input-with-select enterpriseName"
          suffix-icon="el-icon-search"
          @change="ss"
        >
          <!-- <el-button
              type="primary"
              slot="append"
              @click="ss"
              icon="el-icon-search"
            ></el-button> -->
        </el-input>
      </div>
      <div style="height: 500px">
        <div
          class="marg_dq"
          v-for="(o, index) in qdList"
          :key="index"
          style="float: left; text-align: center"
        >
          <img :src="o.IslandIcon?'https://www.lidaoisland.com/'+o.IslandIcon:'~assets/new/Group 7.png'" class="image" />
          <div style="padding: 14px">
            <span
              @click="clickRadio(o)"
              style="font-size: 15px; font-weight: 700"
              >{{ o.Name | filterAmount(6) }}</span
            >
            <div class="bottom clearfix">
              <div style="font-size: 13px; color: #999999">
                {{ o.PersonNumber }}参与
              </div>
              <!-- <time class="time">{{
                    o.Description | filterAmount(10)
                  }}</time> -->
              <div class="mar-t">
                <div v-if="o.IsFollow === false" @click="follow(o.Id)" class="btn">
                  关注
                </div>
                <div
                  v-if="o.IsFollow === true"
                  @click="qxFollow(o.Id)"
                  class="qxBtn"
                >
                  取消关注
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <el-pagination
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage1"
          :page-size="8"
          layout="total, prev, pager, next"
          :total="totals"
        >
        </el-pagination>
      </div>
    </div>

    <div class="com-cont" style="margin-top: 18px; min-height: 500px">
      <h3>我关注的群岛</h3>

      <div style="display: flow-root">
        <div
          class="marg_dq"
          v-for="(o, index) in follList"
          :key="index"
          style="
            float: left;
            text-align: center;
            background: #e3edeb;
            border-radius: 20px;
          "
        >
          <img :src="'https://www.lidaoisland.com/'+o.IslandIcon" class="image" />
          <div style="padding: 14px">
            <span
              @click="clickRadio(o)"
              style="font-size: 15px; font-weight: 700"
              >{{ o.Name | filterAmount(6) }}</span
            >
            <div class="bottom clearfix">
              <div style="font-size: 13px; color: #999999">
                {{ o.PersonNumber }}参与
              </div>
              <!-- <div class="mar-t">
                  <el-tag
                    type="warning"
                    effect="dark"
                    v-if="o.IsFollow === false"
                    @click="follow(o.Id)"
                    >关注</el-tag
                  >
                  <el-tag
                    type="warning"
                    v-if="o.IsFollow === true"
                    @click="qxFollow(o.Id)"
                    >取消关注</el-tag
                  >
                </div> -->
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="com-right">
      <div>
        <img src="../assets/new/tx.png" alt="" />
      </div>
      <el-button type="primary" @click="dialogFormVisible = true"
        >新建群岛</el-button
      >
    </div>
    <!-- <el-button type="primary" @click="ftVisible = true" icon="el-icon-edit"
          >发帖</el-button -->

    <div>
      <el-dialog
        title="新建群岛"
        width="700px"
        center
        :visible.sync="dialogFormVisible"
      >
        <el-form :model="form" label-position="top">
          <el-form-item label="群岛名称" :label-width="formLabelWidth">
            <el-input v-model="form.name" autocomplete="off"></el-input>
          </el-form-item>
          <el-form-item label="群岛简介" :label-width="formLabelWidth">
            <el-input type="textarea" v-model="form.desc"></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="dialogFormVisible = false">取 消</el-button>
          <el-button type="primary" @click="addArchipelago()">确 定</el-button>
        </div>
      </el-dialog>
    </div>
    <!-- <div>
          <el-dialog title="发帖" width="700px" :visible.sync="ftVisible">
            <el-form :model="formFollow">
              <el-form-item label="标题" :label-width="formLabelWidth">
                <el-input
                  v-model="formFollow.name"
                  autocomplete="off"
                ></el-input>
              </el-form-item>
              <el-form-item label="内容" :label-width="formLabelWidth">
                <el-input type="textarea" v-model="formFollow.desc"></el-input>
              </el-form-item>
              <el-form-item label="群岛" :label-width="formLabelWidth">
                <el-select v-model="formFollow.id" placeholder="请选择群岛">
                  <el-option
                    v-for="item in follList"
                    :key="item.value"
                    :label="item.Name"
                    :value="item.Id"
                  >
                  </el-option>
                </el-select>
              </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
              <el-button @click="ftVisible = false">取 消</el-button>
              <el-button type="primary" @click="ftAdd()">确 定</el-button>
            </div>
          </el-dialog>
        </div> -->
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
import Editor from "../components/editor";
// import {post} from "../static/axios"

export default {
  name: "communityPage",
  components: { Top, Editor },
  data: function () {
    return {
      radio: "关注度",
      radio1: "",
      reInput: "",
      currentDate: new Date(),
      currentPage1: 1,
      dialogFormVisible: false,
      ftVisible: false,
      form: {
        name: "",
        desc: "",
        qq: "",
      },
      formFollow: {
        name: "",
        desc: "",
        id: "",
      },
      formLabelWidth: "120px",
      dialogImageUrl: "",
      dialogVisible: false,
      value: "",
      qdList: [],
      follList: [],
      totals: 8,
      content: "",
    };
  },
  filters: {
    filterAmount(value, n) {
      if (!n) n = 20;
      if (value && value.length > n) {
        value = value.substring(0, n) + "...";
      }
      return value;
    },
  },
  created() {
    this.qdLists("", "", 1);
    this.followList();
  },
  mounted() {
    // console.log(this.$cookies.get("user").Id);
  },
  methods: {
    ss() {
      //群岛搜索
      let that = this;
      let data = { Name: that.reInput, Description: that.reInput };
      this.qdLists(that.reInput, "");
    },
    qdLists(a, b, c) {
      //群岛列表
      let that = this;
      this.$axios
        .post("/Api/Island/List", { Name: a, Description: b, Page: c, Size: 8 })
        .then((res) => {
          console.log(res);
          if (res.data.Code === 200) {
            that.qdList = res.data.Data.List;
            that.totals = res.data.Data.Total;
          }
        });
    },
    follow(e) {
      //群岛一键关注
      this.$axios.post("/Api/Island/FollowIsland", { Id: e }).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.$message.success(res.data.Data);
          this.qdLists("", "", 1);
          this.followList();
        } else {
          this.$message.error(res.data.Data);
        }
      });
    },
    qxFollow(e) {
      //群岛取消关注
      this.$axios
        .post("/Api/Island/CancelFollowIsland", { Id: e })
        .then((res) => {
          if (res.data.Code === 200) {
            this.$message.success(res.data.Data);
            this.followList();
            this.qdLists("", "", 1);
          } else {
            this.$message.error(res.data.Data);
          }
        });
    },
    followList() {
      //当前用户所有的关注群岛
      let that = this;
      this.$axios.post("/Api/Island/GetUserFollowIsland").then((res) => {
        if (res.data.Code === 200) {
          console.log(res);
          that.follList = res.data.Data;
        } else {
          // this.$message.error(res.data.Content);
        }
      });
    },
    ftAdd() {
      //发帖
      let that = this;
      console.log(that.formFollow);
      let data = {
        Id: "",
        IslandId: that.formFollow.id,
        Title: that.formFollow.name,
        Content: that.formFollow.desc,
      };
      this.$axios.post("/Api/Post/AddOrUpdatePost", data).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.ftVisible = false;
          this.$message.success("恭喜你，发帖成功");
        } else {
          this.$message.error("错了哦，发帖失败");
        }
      });
    },
    clickRadio(e) {
      let that = this;
      console.log(that.radio1, e.Id);
      this.$router.push({
        path: "./privateLetter",
        query: { id: e.Id },
      });
    },
    sea() {
      let that = this;
      console.log(that.radio);
    },
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`);
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
      this.qdLists("", "", val);
    },
    handleRemove(file, fileList) {
      console.log(file, fileList);
    },
    handlePictureCardPreview(file) {
      console.log(file.url);
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    handleSuccess(file, fileList) {
      console.log(file, fileList);
      this.dialogImageUrl = fileList.url;
    },
    addArchipelago() {
      let that = this;
      console.log(that.form, that.dialogImageUrl);
      let data = {
        Id: "",
        Name: that.form.name,
        Description: that.form.desc,
        QQunNumber: that.form.qq,
      };
      this.$axios.post("/Api/Island/AddOrUpdateIsland", data).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          that.dialogFormVisible = false;
          this.$message.success("恭喜你，群岛创建成功");
          that.qdLists("", "", 1);
          that.followList();
        } else {
          this.$message.error("错了哦，群岛创建失败");
        }
      });
    },
  },
};
</script>
  <style scoped>
.body {
  background: linear-gradient(277.57deg, #ffd0bc -12.96%, #ffffff 124.07%),
    #ffffff;
  min-height: 98vh;
  padding-bottom: 100px;
}

.com-wrap {
  width: 15%;
  margin-left: 20px;
}
.flex {
  display: flex;
  background: rgba(255, 255, 255, 0.6);
  padding: 20px;
}
.com-cont {
  margin: auto;
  width: 43%;
  padding: 20px;
  background: rgba(255, 255, 255, 0.6);
}
.mar {
  margin-left: 20px;
}
.mar-b {
  margin-bottom: 20px;
}
.mar-t {
  margin-top: 15px;
}

/* 卡片 */
.time {
  font-size: 13px;
  color: #999;
}

.bottom {
  margin-top: 13px;
  line-height: 12px;
}

.button {
  padding: 5px;
  margin-right: 5px;
  width: 100px;
}

.image {
  width: 100px;
  height: 100px;
  display: block;
  margin: auto;
  padding: 10px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}

.clearfix:after {
  clear: both;
}
.picture {
  width: 200px;
  height: 200px;
}
.radio {
  margin: 10px;
}

.marg_dq {
  margin: 10px;
  height: 220px;
  width: 180px;
}
.com-right {
  position: absolute;
  top: 105px;
  right: 390px;
  text-align: center;
}
.com-right img {
  width: 120px;
  height: 120px;
}
.el-button--primary {
  background: #fc8f5e;
  border-color: #fc8f5e;
}
::v-deep .enterpriseName .el-input__inner {
  border-radius: 50px;
}
.el-tag {
  border-radius: 20px;
}
.btn{
  width: 40px;
  height: 17px;
  padding: 5px;
  border-radius: 50px;
  font-family: "PingFang HK";
  font-style: normal;
  font-weight: 500;
  font-size: 12px;
  line-height: 17px;
  text-align: center;
  margin: auto;
  background: #fc8f5e;
  color: #ffffff;
}
.qxBtn {
  width: 60px;
  height: 17px;
  padding: 5px;
  border-radius: 50px;
  font-family: "PingFang HK";
  font-style: normal;
  font-weight: 500;
  font-size: 12px;
  line-height: 17px;
  text-align: center;
  margin: auto;
  color: #fc8f5e;
  border: 1px solid #fc8f5e;
}
</style>
  
  