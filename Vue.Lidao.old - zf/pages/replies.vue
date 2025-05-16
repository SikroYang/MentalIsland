<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-17 16:42:21
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="body">
    <Top />
    <div class="content">
      <div class="mar">
        <div>
          <p class="font">{{ title }}</p>
          <div class="flex">
            <div>
              <img src="~assets/tx2.png" alt="" srcset="" class="head" />
            </div>
            <div style="margin-left: 20px">
              <p>{{ name }}</p>
              <span>{{ time }}</span>
              <p>{{ content }}</p>
            </div>
          </div>
        </div>
        <el-divider></el-divider>
        <p class="font">全部回帖</p>
        <div v-for="(item, i) in reList" :key="i">
          <div class="flex">
            <div>
              <img src="~assets/tx2.png" alt="" srcset="" class="head" />
            </div>
            <div style="margin-left: 20px">
              <p>{{ item.CreatedUserName }}</p>
              <span>{{ item.CreatedTime }}</span>
              <p>{{ item.Content }}</p>
            </div>
          </div>
          <el-divider></el-divider>
        </div>
        <el-pagination
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page="currentPage4"
          :page-size="10"
          layout="total, prev, pager, next"
          :total="totals"
        >
        </el-pagination>
      </div>
      <div class="flex" style="width: 90%; margin: auto">
        <div><img src="~assets/tx2.png" alt="" srcset="" class="head" /></div>
        <div style="margin-left: 20px; width: 100%">
          <el-input
            type="textarea"
            placeholder="请留下你的评论"
            v-model="desc"
          ></el-input>
        </div>
      </div>
      <el-button
        type="primary"
        style="margin-top: 20px; margin-left: 50px"
        @click="reply"
        >回复</el-button
      >
    </div>
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
export default {
  name: "repliesPage",
  components: { Top },
  data: function () {
    return {
      currentPage4: 1,
      desc: "",
      title: "",
      name: "",
      time: "",
      content: "",
      reList: [],
      totals:10,
    };
  },
  created() {
    console.log(this.$route.query.id);
    this.repliesList(1);
  },
  methods: {
    repliesList(a) {
      let that = this;
      this.$axios
        .post("/Api/Post/PostById", { Id: this.$route.query.id , Page: a,Size: 10,})
        .then((res) => {
          if (res.data.Code === 200) {
            console.log(res.data.Data);
            that.reList = res.data.Data.Replies.List;
            that.name = res.data.Data.CreatedUserName;
            that.time = res.data.Data.CreatedTime;
            that.content = res.data.Data.Content;
            that.title = res.data.Data.Title;
            that.totals= res.data.Data.Replies.Total;
            console.log(that.reList);
          }
        });
    },
    reply() {
      let that = this;
      this.$axios
        .post("/Api/Post/AddOrUpdateReply", {
          PostId: this.$route.query.id,
          Content: that.desc,
        })
        .then((res) => {
          if (res.data.Code === 200) {
            console.log(res.data.Data);
            this.repliesList(1);
            this.$message.success("恭喜你，回复成功");
          } else {
            this.$message.error("错了哦，回复失败");
          }
        });
    },
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`);
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
      this.repliesList(val);
    },
  },
};
</script>
  <style scoped>
.body {
  background-color: #f6f6f6;
  font-size: 15px;
}
.el-button--primary {
  background: #B28750;
  border-color: #B28750;
}
.content {
  width: 60%;
  margin: auto;
  background-color: #fff;
  padding: 20px;
}
.flex {
  display: flex;
  justify-items: center;
  align-items: center;
  /* justify-content: end; */
}
.head {
  width: 50px;
  height: 50px;
}
.mar {
  margin: 50px;
}
.el-textarea__inner {
  height: 200px !important;
}
.font {
  font-size: 18px;
  font-weight: 600;
}
</style>
  
  