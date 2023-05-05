<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-26 10:01:20
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-05-05 15:31:14
 * @FilePath: \project\pages\team.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
    <div class="bac">
      <Top />
      <div class="content">
        <p class="about" style="font-weight: bold;">关于我们</p>
        <p class="about" style="color: rgba(0, 0, 0, 0.5);">ABOUT US</p>
        <div class="introductio">
            <ul>
                <li class="introduction_li" v-for="(item,i) in list" :key="i">
                    <img class="pic" :src="'https://www.lidaoisland.com/'+item.PhotoUrl" alt="" srcset="">
                    <p style="color: black;font-size: 24px;">{{ item.Name }}</p>
                    <!-- <p style="font-size: 16px;">Knowledge is power</p> -->
                    <p>{{ item.Description }}</p>
                    <!-- <div>
                        <img class="in_img" src="~assets/instagram.png" alt="" srcset="">
                        <img class="in_img" src="~assets/twitter.png" alt="" srcset="">
                        <img class="in_img" src="~assets/facebook.png" alt="" srcset="">
                    </div> -->
                </li>
               
            </ul>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import Top from "../components/Header.vue";
  export default {
    name: "TeamPage",
    components: { Top },
    data: function () {
    return {
      list: [],
    };
  },
    methods: {},
    mounted(){
      this.$axios.post("/Api/TeamMember/List", {
        headers: {
          'Content-Type': "application/json;charset=UTF-8",
        },
      }).then((res) => {
        console.log(res.data.Data.List);
        this.list=res.data.Data.List
      });
    }
    
  };
  </script>
  <style scoped>
  .bac {
  background: radial-gradient(66.85% 40.65% at 15.86% 11.82%, #FFE0B0 0%, #CEFFFB 91.92%), #FFFFFF;
  height: 100vh;
}
  h1,
  h3 {
    color: #9f7861;
  }
  .flex {
    display: flex;
  }
 .content{
    width: 70%;
    margin: 0 auto;
    padding-top: 40px;
 }

.introductio{
    padding-top: 80px;
    width: 80%;
    color: #9f7861;
}
.introductio ul{
    display: flex;
    justify-content: space-between;
}
.introductio ul li{
    list-style: none;
    width: 16%;
    text-align: left;
    margin-right: 15%;
}
.pic{
width: 160px;
height: 200px;
}
.in_img{
    width: 30px;
    height: 30px;
    margin-right: 5px;
}
.about{
  font-weight: 400;
font-size: 30px;
line-height: 15px;
}
  </style>
  