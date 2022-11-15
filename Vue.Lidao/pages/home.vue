<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 14:57:41
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-15 13:33:53
 * @FilePath: \project\pages\index.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <div>
      <div class="right">
        <Top />
      </div>
      <div class="flex">
        <div class="home_left">
          <img
            src="../assets/welcome.jpg"
            style="width: 640px; height: 430px"
            alt=""
            srcset=""
          />
        </div>
        <div class="home_right">
          <div>
            <h1>关于我们</h1>
            <div class="dictum">
              我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台我们是致力于解决心理障碍的公益平台
            </div>
          </div>
          <!-- <div class="flex">
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
          </div> -->
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
      radio: "喜悦",
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
  width: 100%;
  height: 480px;
  background: url("../assets/banner.jpg") no-repeat;
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
  margin-top: 50px;
}
.dictum {
  width: 90%;
  height: 160px;
  line-height: 35px;
  text-indent: 2em;
}
</style>
  