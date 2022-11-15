<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-11 14:55:24
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="body">
    <Top />
    <div class="letter flex">
      <div class="com-wrap">
        <el-row class="tac">
          <el-col :span="12">
            <p style="font-size: 15px;font-weight: 700;">我的关注</p>
            <el-radio-group @change="radio(item.Id)" v-model="radio1" v-for="(item , i) in follList"
              :key="i">
              <el-radio class="radio" :label="item.Id" >{{item.Name}}</el-radio>
            </el-radio-group>
          </el-col>
        </el-row>
      </div>
      <div class="com-right">
        <el-table :data="tableData"  style="width: 100%" @row-click="card">
          <!-- <el-table-column prop="Id" label="ID"> </el-table-column> -->
          <el-table-column prop="Title" label="标题"> </el-table-column>
          <el-table-column prop="ReplyNumber" label="回复" width="120"> </el-table-column>
          <el-table-column prop="CreatedUserName" label="发帖人" width="120">
          </el-table-column>
          <el-table-column prop="CreatedTime" label="创建时间" width="180">
          </el-table-column>
        </el-table>
      </div>
    </div>
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
export default {
  name: "privateLetterPage",
  components: { Top },
  data: function () {
    return {
      radio1: Number(this.$route.query.id),
      tableData: [
        {
          date: "2016-05-02",
          name: "王小虎",
          ace:"12/52",
          address: "上海市普陀区金沙江路 1518 弄",
        },
        {
          date: "2016-05-04",
          name: "王小虎",
          ace:"12/52",
          address: "上海市普陀区金沙江路 1517 弄",
        },
        {
          date: "2016-05-01",
          name: "王小虎",
          ace:"12/52",
          address: "上海市普陀区金沙江路 1519 弄",
        },
        {
          date: "2016-05-03",
          name: "王小虎",
          ace:"12/52",
          address: "上海市普陀区金沙江路 1516 弄",
        },
      ],
      follList: [],
    };
  },
  created() {
    this.followList();
    this.postsTable(this.$route.query.id)
  },
  methods: {
    radio() {
      let that = this;
      console.log(that.radio1);
      this.postsTable(that.radio1)
    },
    card(row, column, event){
        let that = this;
        console.log(row);
        this.$router.push({path:'./replies',query:{id:row.Id}})
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
    postsTable(a){//帖子列表
      let that = this;
      this.$axios.post("/Api/Post/List",{IslandId:a}).then((res) => {
        if (res.data.Code === 200) {
          console.log(res);
          that.tableData = res.data.Data;
        } else {
          this.$message.error("错了哦，群岛没有帖子");
        }
      });
    }
  },
};
</script>
  <style>
    .body{
  background-color: #F6F6F6;
  height: 100vh;
}
.flex {
  display: flex;
}
.com-wrap {
  width: 20%;
  margin-right: 50px;
}
.com-right {
  width: 80%;
  margin-top: 50px;
}
.radio {
  margin: 10px;
}
.letter{
  width: 60%;
  margin: 0 auto;
  background-color: #fff;
  padding: 20px;
}
</style>
  
  