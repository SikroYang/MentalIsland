<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 14:57:41
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-16 16:44:10
 * @FilePath: \project\pages\index.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div >
    <Top />
    <div class="flex">
      <div class="left">
          <div>
            <h1>离岛Island</h1>
            <h3>
              期待你们的积极参与，也愿离岛这个独一无二的平台能够达到它的用处，真正的帮助到你们！
            </h3>
          </div>
          <div style="margin-top: 100px">
            <button class="but" style="margin-right: 90px;" @click="syPage">开始旅程</button>
            <button class="but" @click="login">登入</button>
          </div>
      </div>
      <div class="right right_top">
        <el-carousel indicator-position="outside" height="560">
          <el-carousel-item v-for="(item, i) in list" :key="i">
            <img :src="item.image" class="pic" alt="" srcset="" />
          </el-carousel-item>
        </el-carousel>
      </div>
    </div>
  </div>
</template>

<script>
import Top from "../components/Header.vue";
export default {
  name: "IndexPage",
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
    };
  },
  methods: {
    login() {
      this.$router.push("/login");
    },
    syPage(){
      this.$router.push("/home");
    },
  },
};
</script>
<style>
h1,
h3 {
  color: #9f7861;
}
.flex {
  display: flex;
}
.but {
  border: 1px solid #DFA9A7;
  background-color: #fff;
  width: 320px;
  height: 100px;
  line-height: 100px;
  cursor: pointer;
  border-radius: 5px;
  color: #B24D11;
  font-weight: bold;
}
.left {
  width: 60%;
  height: 100%;
  margin: 250px 120px;
}
.right {
  width: 40%;
  height: 100%;
 
}

.right_top {
  padding: 160px 120px;
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
/* .el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
} */
</style>
