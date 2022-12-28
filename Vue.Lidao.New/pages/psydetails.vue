<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-28 09:24:55
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-19 13:59:45
 * @FilePath: \project\pages\psychology.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <Top />
    <div class="center col" v-for="(item, i) in list" :key="i">
      <h1>{{ item.Title }}</h1>
      <p>斐然 {{ item.CreatedTime }}</p>
      <div class="image">
        <img :src="item.ImageUrl" alt="" srcset="" />
      </div>
      <div>
        <pre class="pre_style">
          {{ item.Content }}
        </pre>
      </div>
    </div>
  </div>
</template>
    
    <script>
import Top from "../components/Header.vue";
export default {
  name: "PsyDetailsPage",
  components: { Top },
  data() {
    return {
      list: [],
    };
  },
  created() {
    this.psy();
    console.log(this.$route.query.id);
    console.log(this.list);
  },
  methods: {
    psy() {
      let that = this;
      let data = { Id: this.$route.query.id };
      this.$axios.post("/Api/Article/ArticleById", data).then((res) => {
        if (res.data.Code === 200) {
          that.list.push(res.data.Data);
          console.log(res.data.Data.Content);
        }
      });
    },
  },
};
</script>
  <style scoped>
.bac {
  background: linear-gradient(360deg, #ffd0bc 0%, rgba(255, 255, 255, 0) 53.89%),
    #ffffff;
  mix-blend-mode: multiply;
  min-height: 98vh;
}
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
  width: 40%;
  margin: auto;
  margin-top: 60px;
}

.pict {
  width: 100%;
  height: 220px;
}
.juc {
  width: 30%;
}
.textl {
  text-align: left;
}

.pre_style {
  all: initial; /*清除继承样式*/
  display: block; /*设置布局流，避免换行导致的错误布局*/
  white-space: pre-line; /*保留换行符，设置溢出换行*/
  font-size: 16px; /*设置字号*/
  line-height: 30px;
  font-family: "宋体";
}
.image {
  text-align: center;
  margin: 50px 0px;
}
.image img {
  width: 458px;
  height: 458px;
}
</style>
    