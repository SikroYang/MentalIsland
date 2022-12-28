<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 14:57:41
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-28 09:41:58
 * @FilePath: \project\pages\index.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <div>
      <div>
        <Top />
      </div>
      <div class="home_flex">
        <div class="welcome">
          <img src="~assets/new/home.png" class="welcome_hello" alt="" srcset="" />
          <p class="written">欢迎回来</p>
        </div>
        <div class="angry">
          
            <div class="saying">
              <p style="font-size: 24px;">今日名言</p>
              <p style="color: #444444;">
                我们真的会变成我们自己所想的那样。我们认为自己好，自己就会更好；认为自己坏，自己就会更坏。所有我们生命中的痛苦和愉快，都完全是自己造成的。我们所思所想的“ 因”，就是在创造将来的“果”。别忽略我们的一思一想，一言一行，那些我们所思所想，所说所做的，都在创造我们的将来。——露易丝·海《生命的重建》
              </p>
            </div>
            <div class="saying">
              <p style="font-size: 24px;">今日心情</p>
              <div class="happy">
                <div v-for="(item, i) in list" :key="i">
                  <img
                    :src="i == ins ? item.image2 : item.image"
                    width="64"
                    style="cursor: pointer;"
                    @click="btn(i)"
                  />
                </div>
              </div>
            </div>
            <div class="saying">
              <p style="font-size: 24px;">今日笔记</p>
              <el-input
                type="textarea"
                placeholder="请输入内容"
                v-model="desc"
                :rows="5"
              ></el-input>
              <el-button style="float: right;margin-top: 5px;" @click="bj">保存</el-button>
            </div>
        </div>
      </div>
    </div>
  </div>
</template>
  
  <script>
// import { info } from "console";
import Top from "../components/Header.vue";
export default {
  name: "homePage",
  components: { Top },
  data() {
    return {
      ins: 0,
      list: [
        {
          id: 0,
          image: require("../assets/expression/Shape.png"),
          image2: require("../assets/expression/Shape1.png"),
        },
        {
          id: 1,
          image: require("../assets/expression/Shape_1.png"),
          image2: require("../assets/expression/Shape1_1.png"),
        },
        {
          id: 2,
          image: require("../assets/expression/Union.png"),
          image2: require("../assets/expression/Union1.png"),
        },
        {
          id: 3,
          image: require("../assets/expression/Shape_2.png"),
          image2: require("../assets/expression/Shape1_2.png"),
        },
        {
          id: 4,
          image: require("../assets/expression/Shape_3.png"),
          image2: require("../assets/expression/Shape1_3.png"),
        },
        {
          id: 5,
          image: require("../assets/expression/Shape_4.png"),
          image2: require("../assets/expression/Shape1_4.png"),
        },
      ],
      desc: "世界很单纯，人生也一样。不是世界复杂，而是你把世界变复杂了。",
    };
  },
  created() {
    this.note();
  },
  methods: {
    btn(e) {
      this.ins = e;
      console.log(e);
      let that = this;
      let data = { Moon: e};
      that.$axios.post("/Api/User/SetTodayInfo", data).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.$message.success(res.data.Data);
        } else {
          this.$message.error(res.data.Data);
        }
      });
    },
    bj(){
      let that = this;
      let data = { Note: that.desc};
      that.$axios.post("/Api/User/SetTodayInfo", data).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.$message.success(res.data.Data);
        } else {
          this.$message.error(res.data.Data);
        }
      });
    },
    note(){
      let that = this;
      that.$axios.post("/Api/User/TodayInfo").then((res) => {
        if (res.data.Code === 200) {
          console.log(res);
          that.desc=res.data.Data.Note
          this.ins=res.data.Data.Moon
        } 
      });
    }
  },
};
</script>
  <style scoped>
.bac {
  background: linear-gradient(360deg, #ffd0bc 0%, rgba(255, 255, 255, 0) 53.89%),
    #ffffff;
  height: 98vh;
}
.home_flex {
  display: flex;
  justify-content: space-around;
  /* align-items: end; */
}

.written {
  text-align: center;
  color: rgba(0, 0, 0, 0.5);
  font-family: "PingFang HK";
  font-style: normal;
  font-weight: 400;
  font-size: 24px;
  line-height: 34px;
}
.welcome {
  margin-top: 50px;
  width: 50%;
  position: relative;
  margin-left: 100px;
}

.welcome_hello {
  width: 500px;
    height: 700px;
    margin-left: 160px;
}
.angry {
  width: 50%;
  margin-top: 50px;
}
.saying {
  width: 780px;
  height: 240px;
  border-radius: 4px;
  background: rgba(255, 255, 255, 0.63);
  padding: 5px 20px;
  margin-bottom: 15px;
}

.happy {
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}
</style>
  