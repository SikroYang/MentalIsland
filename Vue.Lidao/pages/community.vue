<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-15 14:48:06
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="body">
    <Top />
    <div class="flex" style="width: 70%; margin: auto; align-items: baseline;margin-top: 20px;">
      <div class="com-wrap">
        <el-row class="tac">
          <el-col :span="12">
            <p style="font-size: 15px;font-weight: 700;">我的关注</p>
            <el-radio-group
              v-model="radio1"
              @change="clickRadio()"
              v-for="(item , i) in follList"
              :key="i"
            >
              <el-radio class="radio" :label="item.Id" >{{item.Name}}</el-radio>
            </el-radio-group>
          </el-col>
        </el-row>
      </div>
      <div class="com-cont">
        <div class="flex mar-b" style="width: 50%;margin: auto;">
          <el-input placeholder="请输入内容" v-model="reInput" clearable>
          </el-input>
          <div class="mar">
            <el-button type="warning" icon="el-icon-search" @click="ss"
              >搜索</el-button
            >
          </div>
        </div>
        <!-- <el-radio-group
          class="mar-b"
          v-model="radio"
          @change="sea()"
          fill="#F7BC99"
        >
          <el-radio-button label="关注度"></el-radio-button>
          <el-radio-button label="活跃度"></el-radio-button>
        </el-radio-group> -->
        <el-row class="mar-b">
          <el-col :span="6" v-for="(o, index) in qdList" :key="index">
            <el-card :body-style="{ padding: '0px' }" style="margin: 10px;height: 220px">
              <img src="../assets/sq.png" class="image" />
              <div style="padding: 14px">
                <span style="font-size: 15px;font-weight: 700;">{{ o.Name }}</span>
                <div class="bottom clearfix">
                  <time class="time">{{ o.Description|filterAmount(10) }}</time>
                  <div class="mar-t">
                    <el-button
                      type="primary"
                      class="button"
                      @click="follow(o.Id)"
                      >关注</el-button
                    >

                    <!-- <el-popover
                      placement="top-start"
                      width="200"
                      trigger="hover"
                    >
                      <p>QQ群号：{{ o.QQunNumber }}</p>
                      <img
                        class="picture"
                        src="https://t7.baidu.com/it/u=2272690563,768132477&fm=193&f=GIF"
                        alt=""
                        srcset=""
                      />
                      <el-button slot="reference" type="warning" class="button"
                        >加入</el-button
                      >
                    </el-popover> -->
                  </div>
                </div>
              </div>
            </el-card>
          </el-col>
        </el-row>
        <!-- <el-pagination
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage1"
          :page-size="100"
          layout="total, prev, pager, next"
          :total="1000"
        >
        </el-pagination> -->
      </div>
      <div class="com-right flex mart">
        <el-button
          type="primary"
          @click="dialogFormVisible = true"
          icon="el-icon-circle-plus-outline"
          >新建群岛</el-button
        >
        <el-button type="primary" @click="ftVisible = true" icon="el-icon-edit"
          >发帖</el-button
        >
        <div>
          <el-dialog title="新建群岛" width="700px" :visible.sync="dialogFormVisible">
            <el-form :model="form">
              <el-form-item label="群岛名称" :label-width="formLabelWidth">
                <el-input v-model="form.name" autocomplete="off"></el-input>
              </el-form-item>
              <el-form-item label="群岛简介" :label-width="formLabelWidth">
                <el-input type="textarea" v-model="form.desc"></el-input>
              </el-form-item>
              <!-- <el-form-item label="QQ群号" :label-width="formLabelWidth">
                <el-input v-model="form.qq" autocomplete="off"></el-input>
              </el-form-item> -->
              <!-- <el-form-item
                label="上传群岛二维码"
                :label-width="formLabelWidth"
              >
                <el-upload
                  action="https://jsonplaceholder.typicode.com/posts/"
                  list-type="picture-card"
                  :on-preview="handlePictureCardPreview"
                  :on-remove="handleRemove"
                  :on-success="handleSuccess"
                  :limit="1"
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
                <el-dialog :visible.sync="dialogVisible">
                  <img width="100%" :src="dialogImageUrl" alt="" />
                </el-dialog>
              </el-form-item> -->
            </el-form>
            <div slot="footer" class="dialog-footer">
              <el-button @click="dialogFormVisible = false">取 消</el-button>
              <el-button type="primary" @click="addArchipelago()"
                >确 定</el-button
              >
            </div>
          </el-dialog>
        </div>
        <div>
          <el-dialog title="发帖" width="700px" :visible.sync="ftVisible">
            <el-form :model="formFollow">
              <el-form-item label="标题" :label-width="formLabelWidth">
                <el-input v-model="formFollow.name" autocomplete="off"></el-input>
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
        </div>
      </div>
    </div>
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
// import {post} from "../static/axios"

export default {
  name: "communityPage",
  components: { Top },
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
    };
  },
  filters:{
    filterAmount(value, n) {
		if(!n) n = 20;
		if(value && value.length > n) {
			value = value.substring(0, n) + '...';
		}
		return value;
	}

  },
  created() {
    this.qdLists("", "");
    this.followList();
  },
  mounted() {
    console.log(this.$cookies.get("user").Id);
  },
  methods: {
    ss() {
      //群岛搜索
      let that = this;
      let data = { Name: that.reInput, Description: that.reInput };
      this.qdLists(that.reInput, '');
    },
    qdLists(a, b) {
      //群岛列表
      let that = this;
      this.$axios
        .post("/Api/Island/List", { Name: a, Description: b })
        .then((res) => {
          console.log(res);
          if (res.data.Code === 200) {
            that.qdList = res.data.Data;
          }
        });
    },
    follow(e) {
      //群岛一键关注
      this.$axios.post("/Api/Island/FollowIsland", { Id: e }).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.$message.success("恭喜你，群岛关注成功");
          this.followList()
        } else {
          this.$message.error("错了哦，群岛关注失败");
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
          this.$message.error("错了哦，群岛关注失败");
        }
      });
    },
    ftAdd(){//发帖
      let that = this;
      console.log(that.formFollow)
      let data={Id:'',IslandId:that.formFollow.id,Title:that.formFollow.name,Content:that.formFollow.desc}
      this.$axios
        .post("/Api/Post/AddOrUpdatePost", data)
        .then((res) => {
          console.log(res);
          if (res.data.Code === 200) {
            this.ftVisible=false
          this.$message.success("恭喜你，发帖成功");
        } else {
          this.$message.error("错了哦，发帖失败");
        }
        });
    },
    clickRadio() {
      let that = this;
      console.log(that.radio1);
      this.$router.push({
        path: "./privateLetter",
        query: { id: that.radio1 },
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
      this.$axios.post("/Api/Index/Index2/").then((res) => {
        console.log(res);
      });
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
        Id: '',
        Name: that.form.name,
        Description: that.form.desc,
        QQunNumber: that.form.qq,
      };
      this.$axios.post("/Api/Island/AddOrUpdateIsland", data).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          that.dialogFormVisible = false;
          this.$message.success("恭喜你，群岛创建成功");
          that.qdLists()
          that.followList()
        } else {
          this.$message.error("错了哦，群岛创建失败");
        }
      });
    },
  },
};
</script>
  <style scoped>
  .body{
  background-color: #F6F6F6;
  /* height: 100vh; */
}
.com-wrap {
  width: 15%;
  margin-left: 20px;
}
.flex {
  display: flex;
  background-color: #fff;
  padding: 20px;
}
.com-cont {
  margin: auto;
  width: 60%;
  text-align: center;
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
.com-right {
  width: 15%;
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
  width: 50%;
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
</style>
  
  