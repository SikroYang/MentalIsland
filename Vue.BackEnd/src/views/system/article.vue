/**
 * 系统管理 文章管理
 */
<template>
  <div>
    <!-- 面包屑导航 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>文章管理</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 搜索筛选 -->
    <el-form :inline="true" :model="formInline" class="user-search">
      <el-form-item label="搜索：">
        <el-select size="small" v-model="formInline.ArticleTypeId" placeholder="请选择">
          <el-option label="全部" value=""></el-option>
          <el-option v-for="v in articleTypes" :key="v.Id" :label="v.Name" :value="v.Id">{{ v.Name }}</el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="">
        <el-input size="small" v-model="formInline.Title" placeholder="输入标题"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button size="small" type="primary" icon="el-icon-search" @click="search">搜索</el-button>
        <el-button size="small" type="primary" icon="el-icon-plus" @click="handleEdit">添加</el-button>
        <!-- <el-button size="small" type="primary" @click="handleunit()">部门设置</el-button> -->
      </el-form-item>
    </el-form>
    <!--列表-->
    <el-table size="small" @selection-change="selectChange" :data="userData" highlight-current-row v-loading="loading"
      border element-loading-text="拼命加载中" style="width: 100%;">
      <el-table-column align="center" type="selection" width="50">
      </el-table-column>
      <el-table-column align="center" label="标题" width="180">
        <template slot-scope="scope">
          <el-popover trigger="hover" placement="top">
            <p>{{ scope.row.Title }}</p>
            <div slot="reference" class="name-wrapper">
              <el-tag size="medium">{{ scope.row.Title.substring(0, 20) }}</el-tag>
            </div>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column align="center" label="内容" width="350">
        <template slot-scope="scope">
          <el-popover trigger="hover" placement="top">
            <p>{{ scope.row.Content }}</p>
            <div slot="reference" class="name-wrapper">
              <el-tag size="medium">{{ scope.row.Content.substring(0, 30) }}</el-tag>
            </div>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column align="center" prop="CreatedTime" label="创建时间" min-width="120">
        <template slot-scope="scope">
          <div>{{ scope.row.CreatedTime | timestampToTime }}</div>
        </template>
      </el-table-column>
      <el-table-column label="操作" min-width="300">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="deleteUser(scope.$index, scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <!-- 分页组件 -->
    <Pagination v-bind:child-msg="pageparm" @callFather="callFather"></Pagination>
    <!-- 编辑界面 -->
    <el-dialog :title="title" :visible.sync="editFormVisible" width="60%" @click='closeDialog("edit")'>
      <el-form label-width="120px" ref="editForm" :model="editForm" :rules="rules">
        <el-form-item label="文章类型" prop="ArticleTypeId">
          <el-select size="small" v-model="editForm.ArticleTypeId" placeholder="请选择文章类型" class="userRole">
            <el-option v-for="v in articleTypes" :key="v.Id" :label="v.Name" :value="v.Id">{{ v.Name }}</el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="标题" prop="Title">
          <el-input size="small" v-model="editForm.Title" auto-complete="off" placeholder="请输入文章标题"></el-input>
        </el-form-item>
        <el-form-item label="内容" prop="Content">
          <el-input type="textarea" :autosize="{ minRows: 3, maxRows: 12 }" size="small" v-model="editForm.Content"
            auto-complete="off" placeholder="请输入内容">
          </el-input>
        </el-form-item>
        <el-form-item label="上传图片" prop="ImageUrl">
          <el-upload class="avatar-uploader" action="/Api/File/UploadImage" :show-file-list="false"
            :before-upload="beforeAvatarUpload" :on-success="handleAvatarSuccess">
            <img v-if="editForm.ImageUrl" :src="editForm.ImageUrl" class="avatar">
            <i v-else class="el-icon-plus avatar-uploader-icon"></i>
          </el-upload>

        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" @click='closeDialog("edit")'>取消</el-button>
        <el-button size="small" type="primary" :loading="loading" class="title" @click="submitForm('editForm')">保存
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
// 导入请求方法
import {
  ArticlesList,
  ArticleSave,
  ArticleDelete,
  ArticleTypesList,
} from '../../api/basisMG'
import Pagination from '../../components/Pagination'
export default {
  data() {
    return {
      loading: false, //是显示加载
      title: '添加文章',
      editFormVisible: false, //控制编辑页面显示与隐藏
      // 编辑与添加
      editForm: {
        Id: '',
        Title: '',
        Content: '',
        ArticleTypeId: '',
        ImageUrl: ''
      },
      // 选择数据
      selectdata: [],
      // rules表单验证
      rules: {
        Title: [
          { required: true, message: '请输入文章标题', trigger: 'blur' }
        ],
        Content: [
          { required: true, message: '请输入文章内容', trigger: 'blur' }
        ],
        ArticleTypeId: [
          { required: true, message: '请选择文章类型', trigger: 'blur' }
        ],
      },
      // 请求数据参数
      formInline: {
        Page: 1,
        Size: 10,
        ArticleTypeId: '',
        Title: '',
      },
      //用户数据
      userData: [],
      defaultProps: {
        children: 'children',
        label: 'name'
      },
      // 选中
      checkmenu: [],
      // 分页参数
      pageparm: {
        currentPage: 1,
        pageSize: 10,
        total: 10
      },
      articleTypes: [],
    }
  },
  // 注册组件
  components: {
    Pagination
  },

  /**
   * 数据发生改变
   */
  watch: {},

  /**
   * 创建完毕
   */
  created() {
    this.getTypes()
    this.getdata(this.formInline)
  },

  /**
   * 里面的方法只有被调用才会执行
   */
  methods: {
    // 获取文章类型列表
    getTypes() {
      ArticleTypesList().then(res => {
        if (res.Type == "Success") {
          this.articleTypes = res.Data;
        }
        else {
          this.$message({
            type: 'info',
            message: res.Content
          })
        }
      });
    },
    // 获取数据方法
    getdata(parameter) {
      this.loading = true

      /***
       * 调用接口，注释上面模拟数据 取消下面注释
       */
      // 获取用户列表
      ArticlesList(parameter).then(res => {
        this.loading = false
        if (res.Type != "Success") {
          this.$message({
            type: 'info',
            message: res.Content
          })
        } else {
          this.userData = res.Data.List
          // 分页赋值
          this.pageparm.currentPage = this.formInline.Page
          this.pageparm.pageSize = this.formInline.Size
          this.pageparm.total = res.Data.Total
        }
      })
    },
    // 分页插件事件
    callFather(parm) {
      this.formInline.Page = parm.currentPage
      this.formInline.Size = parm.pageSize
      this.getdata(this.formInline)
    },
    //搜索事件
    search() {
      this.getdata(this.formInline)
    },
    //显示编辑界面
    handleEdit: function (index, row) {
      this.editFormVisible = true
      if (row != undefined && row != 'undefined') {
        this.title = '修改文章'
        this.editForm.Id = row.Id
        this.editForm.Title = row.Title
        this.editForm.Content = row.Content
        this.editForm.ArticleTypeId = row.ArticleTypeId
        this.editForm.ImageUrl = row.ImageUrl
      } else {
        this.title = '添加文章'
        this.editForm.Id = ''
        this.editForm.Title = ''
        this.editForm.Content = ''
        this.editForm.ArticleTypeId = ''
        this.editForm.ImageUrl = ''
      }
    },
    // 编辑、添加提交方法
    submitForm(editData) {
      this.$refs[editData].validate(valid => {
        if (valid) {
          // 请求方法
          ArticleSave(this.editForm)
            .then(res => {
              this.editFormVisible = false
              this.loading = false
              if (res.Type == "Success") {
                this.getdata(this.formInline)
                this.$message({
                  type: 'success',
                  message: '数据保存成功！'
                })
              } else {
                this.$message({
                  type: 'info',
                  message: res.Errors
                })
              }
            })
            .catch(err => {
              this.editFormVisible = false
              this.loading = false
              this.$message.error('保存失败，请稍后再试！')
            })
        } else {
          return false
        }
      })
    },
    handleClick(data, checked, node) {
      if (checked) {
        this.$refs.tree.setCheckedNodes([])
        this.$refs.tree.setCheckedNodes([data])
        this.unitparm.deptId = data.id
        this.unitparm.deptName = data.name
        //交叉点击节点
      } else {
      }
    },
    // 选择复选框事件
    selectChange(val) {
      this.selectdata = val
    },
    // 关闭编辑、增加弹出框
    closeDialog(dialog) {
      if (dialog == 'edit') {
        this.editFormVisible = false
      } else if (dialog == 'perm') {
        this.dataAccessshow = false
      } else if (dialog == 'unit') {
        this.unitAccessshow = false
      }
    },
    // 删除用户
    deleteUser(index, row) {
      this.$confirm('确定要删除吗?', '信息', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          // 删除
          ArticleDelete({ Id: row.Id })
            .then(res => {
              if (res.Type == 'Success') {
                this.$message({
                  type: 'success',
                  message: '数据已删除!'
                })
                this.getdata(this.formInline)
              } else {
                this.$message({
                  type: 'info',
                  message: res.Errors
                })
              }
            })
            .catch(err => {
              this.loading = false
              this.$message.error('数据删除失败，请稍后再试！')
            })
        })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除！'
          })
        })
    },
    beforeAvatarUpload(file) {
      const isJPG = file.type === 'image/jpeg' || file.type === 'image/png';
      const isLt2M = file.size / 1024 / 1024 < 2;

      if (!isJPG) {
        this.$message.error('上传图片只能是 JPG 或 PNG 格式!');
      }
      if (!isLt2M) {
        this.$message.error('上传图片大小不能超过 2MB!');
      }
      // this.canUploadConsole = isJPG && isLt2M;
      return isJPG && isLt2M;
    },
    handleAvatarSuccess(res, file) {
      console.log(res, file)
      // UploadImage(file).then(res => {
      if (res.Type = 'Success') {
        this.editForm.ImageUrl = res.Data;
        this.$message({
          type: 'success',
          message: '数据保存成功！'
        })
      }
      else {
        this.$message({
          type: 'info',
          message: res.Errors
        })
      }
      // })
    },


  }
}
</script>

<style scoped>
.user-search {
  margin-top: 20px;
}

.userRole {
  width: 100%;
}

.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}

.avatar-uploader .el-upload:hover {
  border-color: #409EFF;
}

.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}

.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>

