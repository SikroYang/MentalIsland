<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-28 09:24:55
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-15 13:49:27
 * @FilePath: \project\pages\psychology.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="center">
      <h1>你有什么感兴趣的吗？</h1>
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
        <el-radio-group v-model="radio1" @change="sea()" fill="#F7BC99" v-for="(item,i) in gyRadio" :key="i">
          <el-radio-button :label="item.Id">{{item.Name}}</el-radio-button>
        </el-radio-group>
      </div>
      <div class="j-c" v-for="(item,i) in psyList2" :key="i">
        <div class="juc textl">
          <h3>今日话题：{{item.Title}}</h3>
          <p class="col">
            有人安于某种生活，有人不能。因此能安于自已目前处境的不妨就如此生活下去，不能的只好努力另找出路。你无法断言哪里才是成功的，也无法肯定当自已到达了某一点之后，会不会快乐。有些人永远不会感到满足，他的快乐只建立在不断地追求与争取的过程之中，因此，他的目标不断地向远处推移。这种人的快乐可能少，但成就可能大。
          </p>
          <div>
            <img class="pict" src="../assets/pic1.jpg" alt="" srcset="" />
          </div>
        </div>
        <div class="juc">
          <p class="col" style="margin-top: 60px">
            我常以"人就这么一辈子"这句话告诫自己并劝说朋友。这七个字，说来容易，听来简单，想起来却很深沉。它能使我在软弱时变得勇敢，骄傲时变得谦虚，颓废时变得积极，痛苦时变得欢愉，对任何事拿得起也放得下，所以我称它为"当头棒喝"、"七字箴言"。——我常想世间的劳苦愁烦、恩恩怨怨，如有不能化解的，不能消受的，不也就过这短短的几十年就烟消云散了吗？若是如此，又有什么解不开的呢？
          </p>
          <p class="col">
            人就这么一辈子，想到了这句话，如果我是英雄，便要创造更伟大的功业；如果我是学者，便要获取更高的学问；如果我爱什么人，便要大胆地告诉她。因为今日过去便不再来了；这一辈子过去，便什么都消逝了。一本书未读，一句话未讲，便再也没有机会了。这可珍贵的一辈子，我必须好好地把握住它啊！
          </p>
        </div>
        <div class="juc pic">
          <!-- <p>在抉择的哪一刻，成败实已露出端倪。</p> -->
        </div>
      </div>
    </div>
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
export default {
  name: "PsyPage",
  components: { Top },
  data() {
    return {
      reInput: "",
      radio1: "",
      psyList: [],
      psyList2:[],
      gyRadio:[],
    };
  },
  created() {
    this.psy();
    this.psyRadio()
  },
  methods: {
    sea() {
      let that = this;
      console.log(that.radio1);
      // this.psy('','',that.radio1,'');
      this.$router.push({ path: "./psylist", query: { id: that.radio1,text:'' } });
    },
    research() {
      let that = this;
      console.log(that.reInput);
      this.$router.push({ path: "./psylist", query: { text: that.reInput,id:'' } });
    },
    psy(a, b, c, d) {//随机文章
      let that = this;
      let data = { Page: a, Size: b, ArticleTypeId: c, Title: d };
      this.$axios.post("/Api/Article/List", data).then((res) => {
        if (res.data.Code === 200) {
          // console.log(res.data.Data);
          that.psyList = res.data.Data;
          // console.log(that.psyList)
          const obj=that.psyList.List
          console.log(Math.floor(Math.random() * (obj.length - 0) ) + 0)
          const i=Math.floor(Math.random() * (obj.length - 0) ) + 0
          that.radio1=obj[i].ArticleTypeId
          // console.log(obj[i])
          that.psyList2=[]
          that.psyList2.push(obj[i])
        }
      });
    },
    psyRadio(){//按钮列表
      let that = this;
      this.$axios.post("/Api/Article/TypeList").then((res) => {
        if (res.data.Code === 200) {
          console.log(res.data.Data);
          that.gyRadio=res.data.Data
        }
      });
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
.pict {
  width: 100%;
  height: 220px;
}
.border {
  border: 1px solid red;
}
.juc {
  width: 30%;
}
.textl {
  text-align: left;
}
.pic {
  background: url(../assets/pic5.jpg) no-repeat;
  background-size: 100% 100%;
}
</style>
  