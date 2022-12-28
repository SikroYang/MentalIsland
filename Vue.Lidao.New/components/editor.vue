<template>
  <div>
    <div>
      <!-- <input placeholder="请输入标题" class="ace" type="text" v-model="postingTitle"> -->
      <section class="container">
        <quill-editor
          v-if="isClient"
          v-model="content"
          ref="myQuillEditor"
          :myName="content"
          :options="editorOption"
          @blur="onEditorBlur($event)"
          @focus="onEditorFocus($event)"
          @ready="onEditorReady($event)"
          @change="onEditorChange($event)"
        >
        </quill-editor>
      </section>
    </div>
    <!-- <div class="but">
      <button class="tj" @click="tj">发布</button>
    </div> -->
  </div>
</template>
    
    
    <script>
import "quill/dist/quill.core.css";
import "quill/dist/quill.snow.css";
import "quill/dist/quill.bubble.css";

export default {
  name: "Editor",
  data() {
    return {
      isClient: false,
      postingTitle:"",
      content: "",
      editorOption: {
        placeholder: "请在这里输入",
        modules: {
          toolbar: [
            ["bold", "italic", "underline", "strike"], //加粗，斜体，下划线，删除线
            ["blockquote", "code-block"], //引用，代码块
            [{ header: 1 }, { header: 2 }], // 标题，键值对的形式；1、2表示字体大小
            [{ list: "ordered" }, { list: "bullet" }], //列表
            [{ script: "sub" }, { script: "super" }], // 上下标
            [{ indent: "-1" }, { indent: "+1" }], // 缩进
            [{ direction: "rtl" }], // 文本方向
            [{ size: ["small", false, "large", "huge"] }], // 字体大小
            [{ header: [1, 2, 3, 4, 5, 6, false] }], //几级标题
            [{ color: [] }, { background: [] }], // 字体颜色，字体背景颜色
            [{ font: [] }], //字体
            [{ align: [] }], //对齐方式
            ["clean"], //清除字体样式
            ["image"], //上传图片、上传视频
          ],
        },
      },
    };
  },
  props: {
    editorName: {
      type: String,
      default: "",
    },
  },
  computed: {
    // 当前富文本实例
    editor () {
      return this.$refs.myQuillEditor.quill
    }
  },
  mounted() {
    console.log("app init, my quill insrance object is:", this.myQuillEditor);
    // setTimeout(() => {
    //   this.content = 'i am changed'
    // }, 3000)
    if (process.client) {
      const { quillEditor } = require("vue-quill-editor");
      this.$options.components = { quillEditor };
      this.isClient = true;
    }
  },
  methods: {
    onEditorBlur(editor) {
      // console.log('editor blur!', editor)
    },
    onEditorFocus(editor) {
      // console.log('editor focus!', editor)
    },
    onEditorReady(editor) {
      for (var i = 0; i < 20; i++) {
        this.content += "<p><br /></p>";
      }
    },
    onEditorChange ({ editor, html, text }) {
      this.$emit('change', this.escapeStringHTML(this.content))
      console.log(editor, html, text)
    }, 
    escapeStringHTML (str) {
      str = str.replace(/&lt;/g, '<')
      str = str.replace(/&gt;/g, '>')
      return str
    },
    // onEditorChange({ editor, html, text }) {},
    // tj() {
    //   this.$emit("passfunction",this.content)
    //   console.log(this.postingTitle,this.content);
      
    // },
  },
};
</script>
    <style  scoped>
.container {
  width: 100%;
  /* margin: 0 0 0 20px; */
  /* padding: 20px; */
  background-color: #ffffff;
}

.title {
  padding-bottom: 20px;
  background-color: transparent;
}
.title-box {
  height: 4rem;
  padding-left: 10px;
  padding-right: 10px;
  width: 100%;
}
.publish-article {
  margin-top: 20px;
  background-color: #48c774;
  color: #ffffff;
  border-radius: 5px;
  padding-top: 5px;
  padding-bottom: 5px;
  padding-left: 20px;
  padding-right: 20px;
}
.tj {
  width: 116px;
  height: 47px;
  background: #fc8f5e;
  border-radius: 4px;
  line-height: 46px;
  color: #ffffff;
  text-align: center;
  border: none;
}
.ace{
  width: 99.5%;
    height: 31px;
    margin-bottom: 10px;
}
.but{
  margin-top: 10px;
}
</style>
    