<!--
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-10-24 15:15:45
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-12-23 09:51:40
 * @FilePath: \project\pages\login.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="bac">
    <!-- <Top /> -->
    <div style="padding-top: 10px;display: flex;">
      <div style="width: 85%;">
        <img
          style="height: 60px; margin-left: 10px; "
          src="../assets/new/LOGO.png"
          alt=""
        />
      </div>
      <div style="display: flex;align-items: center;">
        <div class="but" >
          <el-button type="primary" :disabled="input==''?true:false" @click="jc">发布</el-button>
        </div>
        <div><img
          style="height: 60px;"
          src="../assets/new/tx.png"
          alt=""
        /></div>
      </div>
    </div>
    <div class="editor" style="margin-bottom: 10px">
      <el-input placeholder="请输入内容" v-model="input" clearable> </el-input>
    </div>
    <div class="editor">
      <Editor ref="Editor" v-model="content" @change="fb" />
    </div>
  </div>
</template>
    
    <script>
import Top from "../components/Header.vue";
import Editor from "../components/editor";
export default {
  name: "postingPage",
  components: { Top, Editor },
  data: function () {
    return {
      content: "",
      input: "",
    };
  },
  created() {},
  mounted() {},
  methods: {
    fb(value) {
      console.log(value);
      this.content = value;
    },
    jc() {
      console.log(this.input, this.content, this.$route.query.id);
      const reg = /<\/?.+?\/?>/g;
      this.content = this.content.replace(reg, "");
      console.log(this.content);
      let that = this;
      let data = {
        Id: "",
        IslandId: that.$route.query.id,
        Title: that.input,
        Content: that.content,
      };
      this.$axios.post("/Api/Post/AddOrUpdatePost", data).then((res) => {
        console.log(res);
        if (res.data.Code === 200) {
          this.ftVisible = false;
          this.$message.success("恭喜你，发帖成功");
        } else {
          this.$message.error("错了哦，发帖失败");
        }
      });
    },
  },
};
</script>
    <style scoped>
.bac {
  background: radial-gradient(
        149.06% 370.42% at 184.66% 130.64%,
        #ffd2bf 0%,
        rgba(255, 142, 95, 0) 100%
      )
      /* warning: gradient uses a rotation that is not supported by CSS and may not behave as expected */,
    radial-gradient(
      70.83% 80.59% at 5.78% -7.47%,
      #ffd0bc 0%,
      rgba(255, 255, 255, 0) 100%
    ),
    #ffffff;
  height: 98vh;
}
.el-button--primary {
  background: #f7bc99;
  border-color: #f7bc99;
}
.editor {
  width: 40vw;
  margin: auto;
}
.but {
  text-align: center;
  margin-right: 40px;
}
</style>
    
    