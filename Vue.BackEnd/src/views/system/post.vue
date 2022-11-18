/**
 * 系统管理 帖子管理
 */
<template>
  <div>
    <!-- 面包屑导航 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>帖子管理</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 搜索筛选 -->
    <el-form :inline="true" :model="formInline" class="user-search">
      <el-form-item label="">
        <el-input size="small" v-model="formInline.Title" placeholder="输入帖子标题"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button size="small" type="primary" icon="el-icon-search" @click="search">搜索</el-button>
        <el-button size="small" type="primary" icon="el-icon-plus" @click="handleEdit()">添加</el-button>
      </el-form-item>
    </el-form>
    <!--列表-->
    <el-table size="small" @selection-change="selectChange" :data="userData" highlight-current-row v-loading="loading"
      border element-loading-text="拼命加载中" style="width: 100%;">
      <el-table-column align="center" type="selection" width="50">
      </el-table-column>
      <el-table-column align="center" prop="Title" label="帖子标题" width="120">
      </el-table-column>
      <el-table-column align="center" label="帖子内容" width="500">
        <template slot-scope="scope">
          <el-popover trigger="hover" placement="top">
            <p>{{ scope.row.Content }}</p>
            <div slot="reference" class="name-wrapper">
              <el-tag size="medium">{{ scope.row.Content.substring(0, 50) }}</el-tag>
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
          <el-button size="mini" @click="searchReply(scope.row.Id)">查看评论</el-button>
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
        <el-form-item label="岛群Id" prop="IslandId">
          <el-input size="small" v-model="editForm.IslandId" auto-complete="off" placeholder="请输入岛群Id"></el-input>
        </el-form-item>
        <el-form-item label="帖子标题" prop="Title">
          <el-input size="small" v-model="editForm.Title" auto-complete="off" placeholder="请输入帖子标题"></el-input>
        </el-form-item>
        <el-form-item label="帖子内容" prop="Content">
          <el-input type="textarea" :autosize="{ minRows: 3, maxRows: 12 }" size="small" v-model="editForm.Content"
            auto-complete="off" placeholder="请输入帖子内容"></el-input>
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
  PostsList,
  PostSave,
  PostDelete
} from '../../api/basisMG'
import Pagination from '../../components/Pagination'
export default {
  data() {
    return {
      loading: false, //是显示加载
      title: '添加帖子',
      editFormVisible: false, //控制编辑页面显示与隐藏
      // 编辑与添加
      editForm: {
        Id: '',
        IslandId: '',
        Title: '',
        Content: '',
      },
      // 选择数据
      selectdata: [],
      // rules表单验证
      rules: {
        IslandId: [
          { required: true, message: '请输入岛群Id', trigger: 'blur' },
          {
            pattern: /^\d+$/,
            required: true,
            message: '岛群Id必须是数字',
            trigger: 'blur'
          }
        ],
        Title: [
          { required: true, message: '请输入帖子标题', trigger: 'blur' }
        ],
        Content: [
          { required: true, message: '请输入帖子内容', trigger: 'blur' }
        ],
      },
      // 请求数据参数
      formInline: {
        Page: 1,
        Size: 10,
        IslandId: 0,
        Title: '',
      },
      //帖子数据
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
      }
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
    this.formInline.IslandId = this.$route.query.id;
    this.getdata(this.formInline)
  },

  /**
   * 里面的方法只有被调用才会执行
   */
  methods: {
    // 获取数据方法
    getdata(parameter) {
      this.loading = true

      /***
       * 调用接口，注释上面模拟数据 取消下面注释
       */
      // 获取帖子列表
      PostsList(parameter).then(res => {
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
    searchReply: function (id) {
      this.$router.push({
        path: "./reply",
        query: { id },
      });
    },
    //显示编辑界面
    handleEdit: function (index, row) {
      this.editFormVisible = true
      if (row != undefined && row != 'undefined') {
        this.title = '修改帖子'
        this.editForm.Id = row.Id
        this.editForm.IslandId = row.IslandId
        this.editForm.Title = row.Title
        this.editForm.Content = row.Content
      } else {
        this.title = '添加帖子'
        var islandId = this.formInline.IslandId ? this.formInline.IslandId : ''
        this.editForm.Id = ''
        this.editForm.IslandId = islandId
        this.editForm.Title = ''
        this.editForm.Content = ''
      }
    },
    // 编辑、添加提交方法
    submitForm(editData) {
      this.$refs[editData].validate(valid => {
        if (valid) {
          // 请求方法
          PostSave(this.editForm)
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
    // 删除帖子
    deleteUser(index, row) {
      this.$confirm('确定要删除吗?', '信息', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          // 删除
          PostDelete({ Id: row.Id })
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
</style>

