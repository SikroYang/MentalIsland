<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-26 10:01:20
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-11 09:59:02
 * @FilePath: \project\pages\team.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div>
    <Top />
    <div class="content">
      <el-tabs v-model="activeName" type="card" @tab-click="handleClick">
        <el-tab-pane label="过去活动" name="first">
          <div class="cont_title">
            <h1>Excepteur sint</h1>
            <div class="flex" style="justify-content: space-between">
              <div class="leftBox">
                <h3>
                  Ruoli mentioned in her letter that besides being out of tune
                  with her roommates, she always remembered the time she spent
                  with her high school friends.
                </h3>
              </div>
              <div>
                <img
                  class="pic"
                  src="https://t7.baidu.com/it/u=4069854689,43753836&fm=193&f=GIF"
                  alt=""
                  srcset=""
                />
              </div>
            </div>
            <h3 style="margin-top: 50px">
              I can arrange things well, but I can think of things I didn't
              arrange。Soldiers have their own aspirations: to reform forever
              and start from scratch; All shameful recessions can only make
              people regard them as enemies and trample them into sand. The true
              freedom is to find a way everywhere when there is nothing to lean
              on. He who can go to a place where water is poor and sit and watch
              the clouds rise is free. This is not the way, but the way of
              heart.
            </h3>
          </div>
        </el-tab-pane>
        <el-tab-pane label="展览" name="second">
          <div class="introduction">
            <ul>
              <li class="introduction_li">
                <img
                  class="pict"
                  src="https://t7.baidu.com/it/u=4069854689,43753836&fm=193&f=GIF"
                  alt=""
                  srcset=""
                />
                <p style="font-size: 20px; font-weight: bold">
                  Knowledge is power
                </p>
                <p>
                  A year's plan is like a tree valley; Ten years' plan is
                  nothing like trees; One hundred years' plan is to cultivate
                  people
                </p>
              </li>
              <li class="introduction_li">
                <img
                  class="pict"
                  src="https://t7.baidu.com/it/u=4069854689,43753836&fm=193&f=GIF"
                  alt=""
                  srcset=""
                />
                <p style="font-size: 20px; font-weight: bold">
                  Knowledge is power
                </p>
                <p>
                  A year's plan is like a tree valley; Ten years' plan is
                  nothing like trees; One hundred years' plan is to cultivate
                  people
                </p>
              </li>
              <li class="introduction_li">
                <img
                  class="pict"
                  src="https://t7.baidu.com/it/u=4069854689,43753836&fm=193&f=GIF"
                  alt=""
                  srcset=""
                />
                <p style="font-size: 20px; font-weight: bold">
                  Knowledge is power
                </p>
                <p>
                  A year's plan is like a tree valley; Ten years' plan is
                  nothing like trees; One hundred years' plan is to cultivate
                  people
                </p>
              </li>
            </ul>
          </div>
        </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>
  
  <script>
import Top from "../components/Header.vue";
export default {
  name: "ExhibitionPage",
  components: { Top },
  data() {
    return {
      activeName: "first",
    };
  },
  methods: {
    handleClick(tab, event) {
      console.log(tab, event);
    },
  },
};
</script>
  <style scoped>
.flex {
  display: flex;
}
.content {
  text-align: center;
  margin-top: 60px;
  width: 60%;
  margin: auto;
}
.el-tabs__nav {
  left: 40%;
}

.cont_title {
  margin-top: 80px;
  color: #9f7861;
  margin: auto;
  text-align: left;
}
.leftBox {
  width: 520px;
  height: 300px;
  background-color: #f9f1e1;
  margin-right: 50px;
}

.leftBox h3 {
  padding: 60px 50px;
  font-size: 18px;
  font-style: italic;
}
.pic {
  width: 500px;
  height: 300px;
}
.introduction {
  padding-top: 80px;
  color: #9f7861;
  margin: auto;
}
.introduction ul {
  display: flex;
  justify-content: space-between;
}
.introduction ul li {
  list-style: none;
  width: 30%;
  text-align: left;
}
.pict {
  width: 300px;
  height: 220px;
}
</style>
  