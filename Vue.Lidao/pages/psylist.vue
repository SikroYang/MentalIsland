<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-28 09:24:55
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-08 10:17:30
 * @FilePath: \project\pages\psychology.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="center">
      <h1>搜查结果：----</h1>
      <div class="flex">
        <el-input placeholder="请输入内容" v-model="reInput" clearable>
        </el-input>
        <div class="mar">
          <el-button type="warning" icon="el-icon-search" @click="research()"
            >搜索</el-button
          >
        </div>
      </div>
      <div class="mart">
        <el-radio-group v-model="radio1" @change="sea()" fill="#F7BC99">
          <el-radio-button  label="基础科普"></el-radio-button>
          <el-radio-button  label="双向的著名作品"></el-radio-button>
          <el-radio-button  label="一手调研"></el-radio-button>
          <el-radio-button  label="有意思的案例"></el-radio-button>
        </el-radio-group>
      </div>
      <div class="j-c psyList">
        <ul>
          <li @click="details()">
            <img
              class="pic"
              src="~assets/p2.jpg"
              alt=""
              srcset=""
            />
            <div class="article">
              <h3>
                每个人都有自己喜爱的花，每个人都有许多种理由善待自己，把一生的光阴凝成时光长河中那一瓣恒久的心香。在盛开的一刹那，灿烂夺目的它会吸引所有的视线。
              </h3>
              <p>
                然而，烦躁的都市啊，请不要忘记，这世界本是镜花水月。一切如花、花如一切。所以，佛祖拈花而迦叶微笑；这一笑，便是整个世界。
              </p>
              <i class="el-icon-right" style="color: #2a5caa;float: right;padding-right: 100px;"></i>
            </div>
          </li>
          <li>
            <img
              class="pic"
              src="~assets/p2.jpg"
              alt=""
              srcset=""
            />
            <div class="article">
              <h3>
                每个人都有自己喜爱的花，每个人都有许多种理由善待自己，把一生的光阴凝成时光长河中那一瓣恒久的心香。在盛开的一刹那，灿烂夺目的它会吸引所有的视线。
              </h3>
              <p>
                然而，烦躁的都市啊，请不要忘记，这世界本是镜花水月。一切如花、花如一切。所以，佛祖拈花而迦叶微笑；这一笑，便是整个世界。
              </p>
              <i class="el-icon-right" style="color: #2a5caa;float: right;padding-right: 100px;"></i>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>
    
    <script>
import Top from "../components/Header.vue";
export default {
  name: "PsyListPage",
  components: { Top },
  data() {
    return {
      reInput: "",
      radio1: "基础科普",
      text: this.$route.query.text,
    };
  },
  methods: {
    sea() {
      let that = this;
      console.log(that.radio1);
    },
    research() {
      let that = this;
      console.log(that.reInput);
    },
    details(){
        let that = this;
        this.$router.push({path:'./psydetails',query:{id:that.id}})
    }
  },
};
</script>
  <style scoped>
h1,
h3 {
  color: #9f7861;
}
.col {
  color: #9f7861;
}
.flex {
  display: flex;
  width: 40%;
  margin: auto;
}
.j-c {
  display: flex;
  width: 90%;
  margin: auto;
  margin-top: 50px;
  justify-content: space-between;
}
.mar {
  margin-left: 20px;
}
.mart {
  margin-top: 30px;
  text-align: center;
}
.center {
  width: 70%;
  margin: auto;
  margin-top: 60px;
}
.center h1 {
  text-align: center;
}
.pic {
  width: 240px;
  height: 200px;
}
.border {
  border: 1px solid red;
}
.juc {
  width: 30%;
}

.psyList ul li {
  display: flex;
  list-style: none;
  margin-bottom: 30px;
}
.article {
  padding:0 50px;
  color: #9f7861;
  background-color: #f5eed2;
}
</style>
    