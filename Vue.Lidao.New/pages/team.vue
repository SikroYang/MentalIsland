<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-26 10:01:20
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-05-11 14:20:49
 * @FilePath: \project\pages\team.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
    <div class="bac">
      <Top />
      <div class="content">
        <div style="padding-left: 40px;">
          <p style="font-weight: 500;font-size: 48px;line-height: 0px;">关于我们</p>
        <p class="about" style="color: rgba(0, 0, 0, 0.5);">ABOUT US</p>
        </div>
        <div class="introductio">
            <ul>
                <li class="introduction_li" v-for="(item,i) in list" :key="i">
                    <img class="pic" :src="'https://www.lidaoisland.com/'+item.PhotoUrl" alt="" srcset="">
                    <p style="color: black;font-size: 24px;">{{ item.Name }}</p>
                    <!-- <p style="font-size: 16px;">Knowledge is power</p> -->
                    <div style="white-space: pre;font-size: 12px;">{{ item.Description }}</div>
                    <!-- <div>
                        <img class="in_img" src="~assets/instagram.png" alt="" srcset="">
                        <img class="in_img" src="~assets/twitter.png" alt="" srcset="">
                        <img class="in_img" src="~assets/facebook.png" alt="" srcset="">
                    </div> -->
                </li>
               
            </ul>
        </div>
        
      </div>
      <div class="map">
        <div class="map_sz">
          <p>中国·苏州</p>
          <img src="../assets/map/Paradam studios 1.png" alt="">
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
  height: 2477px;
  position: relative;
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
    padding-top: 40px;
    width: 100%;
    color: #9f7861;
}
.introductio ul{
  width: 100%;
  display: flex;
  flex-flow: wrap;
}
.introductio ul li{
    list-style: none;
    width: 20%;
    height: 450px;
    margin-right: 66px;
    text-align: left;
}
.pic{
width: 200px;
height: 260px;
}
.in_img{
    width: 30px;
    height: 30px;
    margin-right: 5px;
}
.about{
  font-weight: 400;
font-size: 36px;
line-height: 0px;
}
.map{
  background: url(../assets/map/mapbase.png) no-repeat;
  background-size: 100%;
  width: 100%;
  height: 1100px;
  position: absolute;
  bottom: 0;
}
.map_sz{
  position: absolute;
  top: 206px;
    right: 476px;
    text-align: center;
}
.map_sz img{
width: 66px;
height: 64px;
}
.map_sz p{
  background-color: black;
  color: white;
  padding: 10px 15px;
  border-radius: 5px;
}
  </style>
  