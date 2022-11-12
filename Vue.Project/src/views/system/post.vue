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
        <el-input size="small" v-model="formInline.Name" placeholder="输入帖子名"></el-input>
      </el-form-item>
      <el-form-item label="">
        <el-input size="small" v-model="formInline.Description" placeholder="输入帖子简介"></el-input>
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
      <el-table-column align="center" prop="Name" label="帖子名" width="120">
      </el-table-column>
      <el-table-column align="center" prop="Description" label="姓名" width="120">
      </el-table-column>
      <el-table-column align="center" prop="QQunNumber" label="QQ群号" width="120">
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
    <el-dialog :title="title" :visible.sync="editFormVisible" width="30%" @click='closeDialog("edit")'>
      <el-form label-width="120px" ref="editForm" :model="editForm" :rules="rules">
        <el-form-item label="帖子名" prop="Name">
          <el-input size="small" v-model="editForm.Name" auto-complete="off" placeholder="请输入帖子名"></el-input>
        </el-form-item>
        <el-form-item label="群岛简介" prop="Description">
          <el-input size="small" v-model="editForm.Description" auto-complete="off" placeholder="请输入群岛简介"></el-input>
        </el-form-item>
        <el-form-item label="QQ群号" prop="QQunNumber">
          <el-input size="small" v-model="editForm.QQunNumber" placeholder="请输入QQ群号"></el-input>
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
  IslandsList,
  IslandSave,
  IslandDelete
} from '../../api/basisMG'
import Pagination from '../../components/Pagination'
export default {
  data() {
    return {
      nshow: true, //switch开启
      fshow: false, //switch关闭
      loading: false, //是显示加载
      title: '添加帖子',
      editFormVisible: false, //控制编辑页面显示与隐藏
      dataAccessshow: false, //控制数据权限显示与隐藏
      unitAccessshow: false, //控制所属单位隐藏与显示
      // 编辑与添加
      editForm: {
        Id: '',
        Name: '',
        Description: '',
        QQunNumber: '',
        Email: '',
        Country: '',
      },
      // 选择数据
      selectdata: [],
      // rules表单验证
      rules: {
        Name: [
          { required: true, message: '请输入帖子名', trigger: 'blur' }
        ],
        Description: [
          { required: true, message: '请输入群岛简介', trigger: 'blur' }
        ],
      },
      // 请求数据参数
      formInline: {
        Page: 1,
        Size: 10,
        Name: '',
        Description: '',
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
      IslandsList(parameter).then(res => {
        this.loading = false
        if (res.Type != "Success") {
          this.$message({
            type: 'info',
            message: res.Content
          })
        } else {
          this.userData = res.Data
          // 分页赋值
          this.pageparm.currentPage = this.formInline.Page
          this.pageparm.pageSize = this.formInline.Size
          this.pageparm.total = res.count
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
        this.title = '修改帖子'
        this.editForm.Id = row.Id
        this.editForm.Name = row.Name
        this.editForm.Description = row.Description
        this.editForm.QQunNumber = row.QQunNumber
      } else {
        this.title = '添加帖子'
        this.editForm.Id = ''
        this.editForm.Name = ''
        this.editForm.Description = ''
        this.editForm.QQunNumber = ''
      }
    },
    // 编辑、添加提交方法
    submitForm(editData) {
      this.$refs[editData].validate(valid => {
        if (valid) {
          // 请求方法
          IslandSave(this.editForm)
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
          IslandDelete({ Id: row.Id })
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

