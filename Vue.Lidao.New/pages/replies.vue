<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-21 13:28:00
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="body bac">
    <Top />
    <div class="content">
      <div class="mar">
        <div>
          <p class="font">{{ title }}</p>
          <div class="author">
            <p>
              {{ name }} <span>{{ time }}</span>
            </p>
          </div>
          <div class="flex">
            <div style="text-indent: 2em">
              <p style="white-space:pre-wrap;" v-html="content"></p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="content" style="margin-top: 15px">
      <p class="font2">全部回帖</p>
      <div v-for="(item, i) in reList" :key="i">
        <div class="ht_flex">
          <div style="margin-top: 20px">
            <img src="~assets/new/tx.png" alt="" srcset="" class="head" />
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
      <div class="flex" style="margin-top:30px">
        <div style="width: 100%">
          <!-- <el-input
            type="textarea"
            placeholder="请留下你的评论"
            v-model="desc"
          ></el-input> -->
          <el-input :disabled="discuss" placeholder="请输入内容" v-model="desc">
            <el-button :disabled="discuss"  slot="append" type="primary" @click="reply">评论</el-button>
          </el-input>
        </div>
      </div>
      <!-- <el-button
        type="primary"
        style="margin-top: 20px; margin-left: 50px"
        @click="reply"
        >回复</el-button
      > -->
    </div>
    <div style="height: 200px"></div>
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
      totals: 10,
      discuss:false
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
        .post("/Api/Post/PostById", {
          Id: this.$route.query.id,
          Page: a,
          Size: 10,
        })
        .then((res) => {
          if (res.data.Code === 200) {
            console.log(res.data.Data);
            that.reList = res.data.Data.Replies.List;
            that.name = res.data.Data.CreatedUserName;
            that.time = res.data.Data.CreatedTime;
            that.content = res.data.Data.Content;
            that.title = res.data.Data.Title;
            that.totals = res.data.Data.Replies.Total;
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
.bac {
  background: linear-gradient(277.57deg, #ffd0bc -12.96%, #ffffff 124.07%),
    #ffffff;
  min-height: 98vh;
  overflow: hidden;
  position: relative;
}

.el-button--primary {
  background: #b28750;
  border-color: #b28750;
}
.content {
  width: 50%;
  margin: auto;
  background: rgba(255, 255, 255, 0.6);
  padding: 20px;
  border-radius: 4px;
}
.ht_flex {
  display: flex;
  justify-items: center;
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
  font-weight: 400;
  font-size: 44px;
  line-height: 62px;
}
.font2 {
  font-weight: 500;
  font-size: 18px;
  line-height: 25px;
  color: #000000;
}
.author {
  font-weight: 400;
  font-size: 18px;
  line-height: 25px;
  color: #666666;
}
</style>
  
  