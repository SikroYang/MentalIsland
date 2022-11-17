<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-17 16:29:55
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
            <div style="text-align: center;color: #7C5A45;width: 200%;">
              <img src="../assets/tx.png" alt="" style="width: 100px;height: 100px;">
              <p style="font-size: 15px; font-weight: 700">我的关注</p>
            </div>
            <el-radio-group
              @change="radio(item)"
              v-model="radio1"
              v-for="(item, i) in follList"
              :key="i"
            >
              <el-radio class="radio" :label="item.Id">{{
                item.Name| filterAmount(8)
              }}</el-radio>
            </el-radio-group>
          </el-col>
        </el-row>
      </div>
      <div class="com-right">
        <p style="color: #6E462F;font-size: 18px;font-weight: bold;">{{ text.Name }}</p>
        <p>简介：{{ text.Description }}</p>
        <el-table
          :data="tableData"
          style="width: 100%"
          @row-click="card"
          :row-style="rowStyle"
          :header-cell-style="{
    'background-color': '#fafafa',
    'color': '#6E4630',
    'border-bottom': '1px #FBDDCB solid'
}"
        >
          <!-- <el-table-column prop="Id" label="ID"> </el-table-column> -->
          <el-table-column prop="Title" label="标题"> </el-table-column>
          <el-table-column prop="ReplyNumber" label="回复" width="120">
          </el-table-column>
          <el-table-column prop="CreatedUserName" label="发帖人" width="120">
          </el-table-column>
          <el-table-column prop="CreatedTime" label="创建时间" width="180">
          </el-table-column>
        </el-table>
        <el-pagination
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage1"
          :page-size="8"
          layout="total, prev, pager, next"
          :total="totals"
        >
        </el-pagination>
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
      tableData: [],
      follList: [],
      text: this.$route.query.text,
      currentPage1: 1,
      totals:8,
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
    this.followList();
    this.postsTable(this.$route.query.id,1);
  },
  methods: {
    radio(e) {
      let that = this;
      that.text = e;
      console.log(that.radio1, e);
      this.postsTable(that.radio1,1);
    },
    card(row, column, event) {
      let that = this;
      console.log(row);
      this.$router.push({ path: "./replies", query: { id: row.Id } });
    },
    followList() {
      //当前用户所有的关注群岛
      let that = this;
      this.$axios.post("/Api/Island/GetUserFollowIsland").then((res) => {
        if (res.data.Code === 200) {
          // console.log(res);
          that.follList = res.data.Data;
        } else {
          this.$message.error("错了哦，群岛关注失败");
        }
      });
    },
    postsTable(a,b) {
      //帖子列表
      let that = this;
      this.$axios.post("/Api/Post/List", { IslandId: a,Page: b,Size: 10, }).then((res) => {
        if (res.data.Code === 200) {
          // console.log(res);
          that.tableData = res.data.Data.List;
          that.totals=res.data.Data.Total
        } else {
          this.$message.error("错了哦，群岛没有帖子");
        }
      });
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
      this.postsTable(this.$route.query.id,val)
    },
    rowStyle({ row, rowIndex }) {
      console.log(row,rowIndex)
      if(row.ReplyNumber!==0){
            return {
              cursor: 'pointer'
            }
          }
    },
  },
};
</script>
  <style>
.body {
  background-color: #f6f6f6;
  height: 96vh;
  
}
.flex {
  display: flex;
}
.com-wrap {
  width: 20%;
  margin-right: 50px;
  height: 100%;
  background-color: #FCF6F6;
  padding: 20px;
}
.com-right {
  width: 80%;
  margin-top: 50px;
  height: 100%;
}
.radio {
  margin: 10px;
}
.letter {
  width: 60%;
  margin: 0 auto;
  background-color: #fff;
  padding: 20px 0;
  height: 80vh;
}
.el-radio-group {
  width: 200%;
  background-color: #fbf7ec;
  margin-bottom: 10px;
  border: 1px solid #FBDDCB;
}
</style>
  
  