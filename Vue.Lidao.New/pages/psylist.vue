<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-28 09:24:55
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-03-20 10:23:53
 * @FilePath: \project\pages\psychology.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <Top />
    <div class="center">
      <h1>你有什么感兴趣的吗？</h1>
      <div class="flex">
        <el-input
            placeholder="请输入内容"
            v-model="reInput"
            suffix-icon="el-icon-search"
            @change="research"
          ></el-input>
        <!-- <el-input placeholder="请输入内容" v-model="reInput" clearable>
        </el-input>
        <div class="mar">
          <el-button type="warning" icon="el-icon-search" @click="research()"
            >搜索</el-button
          >
        </div> -->
      </div>
      <div class="mart_psy">
        <el-radio-group
          v-model="radio1"
          @change="sea()"
          fill="#FFD8D8"
          text-color="#793F28"
          v-for="(item, i) in gyRadio"
          :key="i"
        >
          <el-radio-button :label="item.Id" style="margin-right: 10px;">{{ item.Name }}</el-radio-button>
        </el-radio-group>
      </div>
      <div class="j-c psyList">
        <ul>
          <li v-for="(item, i) in artList" :key="i">
            <div style="width: 45%; position: relative">
              <img class="pic" :src="item.ImageUrl" alt="" srcset="" />
              <!-- <div class="pic_time">
                <div>{{ getPartTime(item.CreatedTime).toString() | filterTime() }}</div>
                <div> {{ getPartTime(item.CreatedTime).toString() | filterMonth() }}</div>
              </div> -->
            </div>
            <div class="article">
              <h3>
                {{ item.Title }}
              </h3>
              <p>
                {{ item.Content | filterAmount(60) }}
              </p>
              <div class="detail">
                <el-button round @click="details(item.Id)">查看详细</el-button>
              </div>
              <!-- <p style="float: right; padding-right: 100px;cursor: pointer;"
                @click="details(item.Id)">
                详细
                <i class="el-icon-right"></i>
              </p> -->
            </div>
          </li>
        </ul>
      </div>
      <div class="pag">
        <el-pagination
          @current-change="handleCurrentChange"
          layout="prev, pager, next"
          :total="Number(ar_total)"
          :current-page="currentPage"
          :page-size="pagesize"
        >
        </el-pagination>
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
      radio1: Number(this.$route.query.id),
      text: this.$route.query.text,
      gyRadio: [],
      artList: [],
      currentPage: 1, //初始页
      pagesize: 5, //    每页的数据
      ar_total: "",
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
    filterTime(value) {
      var date = new Date(value);
      value = date.getDate();
      return value;
    },
    filterMonth(value) {
      var monthsInEng = [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
      ];
      var date = new Date(value);
      value = monthsInEng[date.getMonth()];
      return value;
    },
  },
  created() {
    console.log(this.$route.query);
    if (this.$route.query.text != "") {
      this.psy(1, 6, "", this.text);
    } else {
      this.psy(1, 6, this.radio1, "");
    }

    this.psyRadio();
  },
  methods: {
    getPartTime(val) {
      //let ti = val.split(' ');
      var tim = val.replace(" ", ":").replace(/\:/g, "-").split("-");
      var tim = Number(tim[0]) + "-" + Number(tim[1]) + "-" + Number(tim[2]);
      return tim;
    },
    sea() {
      let that = this;
      console.log(that.radio1);
      that.psy(1, 6, that.radio1, "");
    },
    research() {
      let that = this;
      console.log(that.reInput);
      this.psy(1, 6, "", this.reInput);
    },
    details(e) {
      let that = this;
      that.$router.push({ path: "./psydetails", query: { id: e } });
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
          that.ar_total = that.psyList.Total;
          // let arr = obj.filter((i) => {
          //   return that.radio1 == i.ArticleTypeId;
          // });
          // that.artList=arr
          // console.log(arr);
        }
      });
    },
    handleCurrentChange(currentPage) {
      console.log(currentPage); //点击第几页
      this.psy(currentPage, 6, this.radio1, this.text);
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
  color: #AF6D54;
}
.bac {
  background: linear-gradient(360deg, #ffd0bc 0%, rgba(255, 255, 255, 0) 53.89%),
    #ffffff;
  mix-blend-mode: multiply;
  height: 200vh;
}

.flex {
  display: flex;
  width: 85%;
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
.mart_psy {
  margin-top: 30px;
  text-align: center;
  width: 100%;
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
  width: 325px;
  height: 200px;
  border-radius: 4px 4px 50px 4px;
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
  /* background-color: #f5eed2; */
}
.detail button {
  margin-top: 35px;
  color: #af6d54;
  width: 200px;
}
.pag {
  display: flex;
  width: 90%;
  margin: auto;
  justify-content: center;
}
.pic_time {
  color: #fff;
  width: 64px;
  height: 64px;
  background: #161015;
  border-radius: 4px 4px 25px 4px;
  position: absolute;
  top: -10px;
  right: 5px;
  text-align: center;
  line-height: 26px;
}

</style>
    