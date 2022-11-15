<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-28 09:24:55
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-15 16:47:08
 * @FilePath: \project\pages\psychology.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="center">
      <h1>搜查结果：{{ text }}</h1>
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
        <el-radio-group
          v-model="radio1"
          @change="sea()"
          fill="#F7BC99"
          v-for="(item, i) in gyRadio"
          :key="i"
        >
          <el-radio-button :label="item.Id">{{ item.Name }}</el-radio-button>
        </el-radio-group>
      </div>
      <div class="j-c psyList">
        <ul>
          <li v-for="(item, i) in artList" :key="i">
            <div style="width: 20%;">
              <img class="pic" src="~assets/p2.jpg" alt="" srcset="" />
            </div>
            <div class="article">
              <h3>
                {{ item.Title }}
              </h3>
              <p>
                {{ item.Content | filterAmount(60) }}
              </p>
              <i
                class="el-icon-right"
                style="color: #2a5caa; float: right; padding-right: 100px"
                @click="details(item.Id)"
              ></i>
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
      radio1: this.$route.query.id,
      text: this.$route.query.text,
      gyRadio: [],
      artList: [],
    };
  },
  filters: {
    filterAmount(value, n) {
      if (!n) n = 20;
      if (value && value.length > n) {
        value = value.substring(0, n) + "...";
      }
      return value;
    },
  },
  created() {
    console.log(this.$route.query);
    if (this.$route.query.text != "") {
      this.psy("", "", "", this.text);
    } else {
      this.psy("", "", this.radio1, "");
    }

    this.psyRadio();
  },
  methods: {
    sea() {
      let that = this;
      console.log(that.radio1);
      that.psy("", "", that.radio1, "");
    },
    research() {
      let that = this;
      console.log(that.reInput);
      this.psy("", "", "", this.reInput);
    },
    details(e) {
      let that = this;
      that.$router.push({ path: "./psydetails" , query: { id: e } });
    },
    psy(a, b, c, d) {
      //搜索结果
      let that = this;
      let data = { Page: a, Size: b, ArticleTypeId: c, Title: d };
      this.$axios.post("/Api/Article/List", data).then((res) => {
        if (res.data.Code === 200) {
          // console.log(res.data.Data);
          that.psyList = res.data.Data;
          // console.log(that.psyList)
          const obj = that.psyList.List;
          // console.log(obj)
          that.artList = obj;
          // let arr = obj.filter((i) => {
          //   return that.radio1 == i.ArticleTypeId;
          // });
          // that.artList=arr
          // console.log(arr);
        }
      });
    },
    psyRadio() {
      //按钮列表
      let that = this;
      this.$axios.post("/Api/Article/TypeList").then((res) => {
        if (res.data.Code === 200) {
          that.gyRadio = res.data.Data;
        }
      });
    },
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
  width: 90%;
}
.article {
  padding: 0 50px;
  color: #9f7861;
  background-color: #f5eed2;
}
</style>
    