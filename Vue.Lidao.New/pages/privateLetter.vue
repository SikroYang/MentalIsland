<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2023-02-03 16:08:32
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <Top />
    <div class="letter flex">
      <div class="com-left">
        <div class="com_top">
          <div>
            <img src="../assets/new/2.png" alt="" srcset="" />
          </div>
          <div class="mar_10">
            <p>
              {{ text.Name }}
            </p>
            <p>{{ text.PostNumber }}成员</p>
            <p>{{ text.Description }}</p>
          </div>
          <div style="position: absolute; right: 10px; top: 43px">
            
            <div  v-if="text.IsFollow === false"
              @click="follow(text.Id)" class="btn">
                  关注
                </div>
                <div
                v-if="text.IsFollow === true"
              @click="qxFollow(text.Id)"
                  class="qxBtn"
                >
                  取消关注
                </div>
          </div>
        </div>
        <div class="com_bot">
          <h3>全部帖子</h3>
          <ul v-for="(item, i) in tableData" :key="i">
            <li @click="tzCard(item.Id)">
              <div class="card">
                <div>
                  <img src="../assets/new/tx.png" alt="" />
                </div>
                <div class="mar_10">
                  <p>{{ item.CreatedUserName }}</p>
                  <p class="card_time">{{ item.CreatedTime }}</p>
                </div>
                <div class="card_icon">
                  <i
                    class="el-icon-chat-dot-square"
                    style="font-size: 28px"
                  ></i>
                  {{ item.ReplyNumber }}
                </div>
              </div>
              <div style="margin-left: 57px;">
                <p style="font-weight: 500; font-size: 24px; line-height: 34px">
                  {{ item.Title }}
                </p>
                <!-- {{ item.Content | filterAmount(80) }} -->
                <div style="white-space:pre-wrap;" v-html="$options.filters.filterAmount(item.Content,80)"></div>
              </div>
            </li>
          </ul>
        </div>
        <div class="pagination">
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
      <div class="com-right">
        <div>
          <img src="../assets/new/tx.png" alt="">
        </div>
        <el-button type="primary" @click="posting()">发帖</el-button>
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
      tableData: [],
      follList: [],
      text: [],
      currentPage1: 1,
      totals: 8,
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
    this.postsTable(this.$route.query.id, 1);
    this.followXq();
  },
  methods: {
    tzCard(e){
      console.log(e);
      this.$router.push({ path: "./replies", query: { id: e } });
    },
    card(row, column, event) {
      let that = this;
      console.log(row);
      this.$router.push({ path: "./replies", query: { id: row.Id } });
    },
    posting(){
      let that = this;
      this.$router.push({ path: "./posting", query: { id: that.$route.query.id } });
    },
    postsTable(a, b) {
      //帖子列表
      let that = this;
      this.$axios
        .post("/Api/Post/List", { IslandId: a, Page: b, Size: 10 })
        .then((res) => {
          if (res.data.Code === 200) {
            // console.log(res);
            that.tableData = res.data.Data.List;
            that.totals = res.data.Data.Total;
          } else {
            this.$message.error("错了哦，群岛没有帖子");
          }
        });
    },
    followXq(e) {
      //群岛详情
      let that = this;
      this.$axios
        .post("/Api/Island/IslandDetail", { Id: this.$route.query.id })
        .then((res) => {
          console.log(res);
          if (res.data.Code === 200) {
            that.text = res.data.Data;
          } else {
            this.$message.error(res.data.Data);
          }
        });
    },
    follow(e) {
      //群岛一键关注
      this.$axios.post("/Api/Island/FollowIsland", { Id: e }).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.$message.success(res.data.Data);
          this.followXq();
        } else {
          this.$message.error(res.data.Content);
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
            this.followXq();
          } else {
            this.$message.error(res.data.Data);
          }
        });
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
      this.postsTable(this.$route.query.id, val);
    },
    rowStyle({ row, rowIndex }) {
      console.log(row, rowIndex);
      if (row.ReplyNumber !== 0) {
        return {
          cursor: "pointer",
        };
      }
    },
  },
};
</script>
  <style scoped>
.bac {
  background: linear-gradient(277.57deg, #ffd0bc -12.96%, #ffffff 124.07%),
    #ffffff;
  min-height: 98vh;
  overflow: hidden;
  position: relative;
}
.flex {
  display: flex;
}
.mar_10 {
  margin-left: 10px;
}
.el-button--primary {
  background: #fc8f5e;
  border-color: #fc8f5e;
}
.el-button--warning {
  background: #ccc;
  border-color: #ccc;
}
.letter {
  width: 50%;
  margin: 0 auto;
  padding: 20px 0;
}
.com-left {
  width: 100%;
  position: relative;
}
.com_top {
  width: 100%;
  height: 137px;
  display: flex;
  background: #fff;
  border-radius: 4px;
  align-items: center;
  padding: 20px;
}
.com_top img {
  width: 80px;
  height: 90px;
}
.com_top p:nth-child(1) {
  font-weight: 500;
  font-size: 18px;
  line-height: 25px;
  color: #000000;
}
.com_top p:nth-child(2) {
  font-weight: 400;
  font-size: 12px;
  line-height: 17px;
  color: #999999;
}
.com_top p:nth-child(3) {
  font-weight: 400;
  font-size: 12px;
  line-height: 17px;
  color: #666666;
}
.com_bot {
  background: #fff;
  margin-top: 10px;
  width: 100%;
  padding: 20px;
}
.com_bot ul li {
  list-style: none;
}
.com_bot ul li img {
  width: 48px;
  height: 48px;
}
.card {
  display: flex;
  align-items: center;
  position: relative;
}
.card_time {
  font-weight: 400;
  font-size: 12px;
  line-height: 17px;
  color: #999999;
}
.card_icon {
  position: absolute;
  display: flex;
  right: 10px;
}
.pagination{
  text-align: center;
  margin-top: 20px;
}
.com-right{
  position: absolute;
    top: 105px;
    right: 211px;
    text-align: center;
}
.com-right img{
  width: 120px;
  height: 120px;
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
  
  