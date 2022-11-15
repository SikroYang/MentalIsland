<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-25 10:47:31
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-10-28 11:30:01
 * @FilePath: \project\pages\hero.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <!-- <Tutorial/> -->

  <el-row>
    <el-button>默认按钮</el-button>
    <el-button type="primary" @click="login">登录</el-button>
    <el-button type="success" @click="asyncData">成功按钮</el-button>
    <el-button type="info">信息按钮</el-button>
    <el-button type="warning">警告按钮</el-button>
    <el-button type="danger">危险按钮</el-button>
  </el-row>
</template>
  
  <script>
export default {
  name: "HeroPage",
  methods: {
    login() {
      this.$router.push("/login");
    },
    
  },
};
</script>
  