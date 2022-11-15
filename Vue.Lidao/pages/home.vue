<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 14:57:41
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-14 16:15:47
 * @FilePath: \project\pages\index.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div>
      <div class="right">
        <!-- <h1>欢迎回家</h1> -->
      </div>
      <div class="flex">
        <!-- <div class="home_left">
          <el-carousel indicator-position="outside" height="460px">
            <el-carousel-item v-for="(item, i) in list" :key="i">
              <img :src="item.image" class="pic" alt="" srcset="" />
            </el-carousel-item>
          </el-carousel>
        </div> -->
        <div class="home_right">
          <div>
            <h3>今日名言</h3>
            <div class="dictum">
              不要停顿,因为别人会超过你;不要返顾,以免摔倒。——阿·雷哈尼
            </div>
          </div>
          <div class="flex">
            <div>
              <h3>今日心情</h3>
              <div>
                <el-radio-group v-model="radio">
                  <el-radio-button label="喜悦"></el-radio-button>
                  <el-radio-button label="愤怒"></el-radio-button>
                  <el-radio-button label="悲哀"></el-radio-button>
                  <el-radio-button label="恐惧"></el-radio-button>
                </el-radio-group>
              </div>
            </div>
            <div>
              <h3>今日笔记</h3>
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
      list: [
        {
          image: require("../assets/pic1.jpg"),
        },
        {
          image: require("../assets/pic2.jpg"),
        },
        {
          image: require("../assets/pic3.jpg"),
        },
        {
          image: require("../assets/pic4.jpg"),
        },
      ],
      radio:"喜悦"
    };
  },
  methods: {},
};
</script>
  <style scoped>
h1,
h3 {
  color: #9f7861;
}
.flex {
  display: flex;
  justify-content: space-around;
  align-items: end;
}
.but {
  border: 1px solid #9f7861;
  background-color: #fff;
  width: 260px;
  padding: 35px;
  cursor: pointer;
  border-radius: 5px;
}
.left {
  width: 60%;
  height: 100%;
}
.right {
  width: 30%;
  height: 300px;
  margin: auto;
  background: url("../assets/hy.png") no-repeat;
  background-size: 100% 100%;
}
.left_top {
  padding: 280px 120px;
}

/* 轮播 */
.el-carousel__item h3 {
  color: #475669;
  font-size: 18px;
  opacity: 0.75;
  line-height: 300px;
  margin: 0;
}
.pic {
  width: 100%;
  height: 100%;
}
.home_left {
  width: 40%;
  /* height: 460px; */
}
.home_right {
  width: 55%;
  margin-left: 30px;
}
.dictum {
  width: 90%;
  height: 60px;
  background-color: #fadfcf;
  padding: 20px 40px;
}
</style>
  