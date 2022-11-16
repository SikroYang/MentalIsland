<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-26 10:01:20
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-16 16:45:55
 * @FilePath: \project\pages\team.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
    <div>
      <Top />
      <div class="content">
        <div class="but">我们的团队</div>
        <div class="introductio">
            <ul>
                <li class="introduction_li">
                    <img class="pic" src="../assets/lock.png" alt="" srcset="">
                    <p style="color: black;font-size: 24px;">Name Surname</p>
                    <p style="font-size: 16px;">Knowledge is power</p>
                    <p>A year's plan is like a tree valley; Ten years' plan is nothing like trees; One hundred years' plan is to cultivate people</p>
                    <div>
                        <img class="in_img" src="~assets/instagram.png" alt="" srcset="">
                        <img class="in_img" src="~assets/twitter.png" alt="" srcset="">
                        <img class="in_img" src="~assets/facebook.png" alt="" srcset="">
                    </div>
                </li>
                <li class="introduction_li">
                    <img class="pic" src="../assets/lock.png" alt="" srcset="">
                    <p style="color: black;font-size: 24px;">Name Surname</p>
                    <p style="font-size: 16px;">Knowledge is power</p>
                    <p>A year's plan is like a tree valley; Ten years' plan is nothing like trees; One hundred years' plan is to cultivate people</p>
                    <div>
                        <img class="in_img" src="~assets/instagram.png" alt="" srcset="">
                        <img class="in_img" src="~assets/twitter.png" alt="" srcset="">
                        <img class="in_img" src="~assets/facebook.png" alt="" srcset="">
                    </div>
                </li>
                <li class="introduction_li">
                    <img class="pic" src="../assets/lock.png" alt="" srcset="">
                    <p style="color: black;font-size: 24px;">Name Surname</p>
                    <p style="font-size: 16px;">Knowledge is power</p>
                    <p>A year's plan is like a tree valley; Ten years' plan is nothing like trees; One hundred years' plan is to cultivate people</p>
                    <div>
                        
                        <img class="in_img" src="~assets/instagram.png" alt="" srcset="">
                        <img class="in_img" src="~assets/twitter.png" alt="" srcset="">
                        <img class="in_img" src="~assets/facebook.png" alt="" srcset="">
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
    name: "TeamPage",
    components: { Top },
    methods: {
      login() {
        // this.$router.push("/login");
      },
    },
  };
  </script>
  <style scoped>
  h1,
  h3 {
    color: #9f7861;
  }
  .flex {
    display: flex;
  }
 .content{
   text-align: center;
    width: 70%;
    margin: 0 auto;
    padding-top: 100px;
 }
.but{
    margin-bottom: 100px;
    width: 150px;
    height: 60px;
    line-height: 60px;
    border-radius: 5px;
    color: #fff;
    background-color: #F7BC99;
    margin: auto;
}
.introductio{
    padding-top: 80px;
    width: 80%;
    color: #9f7861;
    margin: auto;
}
.introductio ul{
    display: flex;
    justify-content: space-between;
}
.introductio ul li{
    list-style: none;
    width: 30%;
    text-align: left;
}
.pic{
width: 200px;
height: 200px;
}
.in_img{
    width: 30px;
    height: 30px;
    margin-right: 5px;
}
  </style>
  