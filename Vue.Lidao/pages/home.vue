<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 14:57:41
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-16 17:00:58
 * @FilePath: \project\pages\index.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <div>
      <div>
        <Top />
        <div class="banner"></div>
      </div>
      <div class="home_flex">
        <div class="welcome">
          <h1 class="written">欢迎回家</h1>
          <img
            src="~assets/sq.png"
            class="welcome_left"
            alt=""
            srcset=""
            width="220"
          />
          <img
            src="~assets/s2.png"
            class="welcome_center"
            alt=""
            srcset=""
            width="240"
          />
          <img
            src="~assets/s3.png"
            class="welcome_right"
            alt=""
            srcset=""
            width="220"
          />
        </div>
        <div class="angry">
          <div>
            <h3>今日名言</h3>
            <div class="saying">
              <p>
                世界很单纯，人生也一样。不是世界复杂，而是你把世界变复杂了。
              </p>
            </div>
          </div>
          <div class="happy_flex">
            <div class="happy_left">
            <h3>今日心情</h3>
            <div class="happy">
              <div v-for="(item, i) in list" :key="i">
                <img :src="i==ins?item.image2:item.image" width="64" @click="btn(i)" />
              </div>
            </div>
          </div>
          <div class="happy_right">
            <h3>今日笔记</h3>
            <el-input type="textarea"  placeholder="请输入内容" v-model="desc" :rows="6"></el-input>
          </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
export default {
  name: "homePage",
  components: { Top },
  data() {
    return {
        ins:0,
      list: [
        {
          id: 0,
          image: require("../assets/happy2.png"),
          image2: require("../assets/happy.png"),
        },
        {
          id: 1,
          image: require("../assets/upset2.png"),
          image2: require("../assets/upset.png"),
        },
        {
          id: 2,
          image: require("../assets/angry2.png"),
          image2: require("../assets/angry.png"),
        },
        {
          id: 3,
          image: require("../assets/embar2.png"),
          image2: require("../assets/embar.png"),
        },
      ],
      desc: "",
    };
  },
  methods: {
    btn(e){
        this.ins=e
        console.log(e)
    }
  },
};
</script>
  <style scoped>
h1,
h3 {
  color: #9f7861;
}
.home_flex {
  display: flex;
  justify-content: space-around;
  align-items: end;
}
.banner {
  width: 1725px;
  height: 281px;
  /* background-color: #F5E9E9; */
  background: url(../assets/banner.jpg) no-repeat;
  background-size: 100% 100%;
  margin: auto;
}
.written {
  position: absolute;
  left: 245px;
}
.welcome {
  margin-top: 100px;
  width: 50%;
  position: relative;
  margin-left: 100px;
}
.welcome_left {
  position: absolute;
}
.welcome_center {
  position: absolute;
  top: 85px;
  left: 210px;
}
.welcome_right {
  position: absolute;
  left: 450px;
}
.angry {
  width: 50%;
  margin-top: 50px;
}
.saying {
  width: 90%;
  height: 120px;
  background-color: #fadfcf;
}
.saying p {
  padding: 30px 20px;
}
.happy_flex{
    display: flex;
}
.happy_left{
    width: 40%;
}
.happy_right{
    width: 40%;
    margin-left: 88px;
}
.happy {
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}
</style>
  