/**
 * 系统管理 用户管理
 */
<template>
  <div>
    <!-- 面包屑导航 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>用户管理</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 搜索筛选 -->
    <el-form :inline="true" :model="formInline" class="user-search">
      <el-form-item label="搜索：">
        <el-select size="small" v-model="formInline.IsLocked" placeholder="请选择">
          <el-option label="全部" value=""></el-option>
          <el-option label="正常" value="N"></el-option>
          <el-option label="已锁定" value="Y"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="">
        <el-input size="small" v-model="formInline.UserName" placeholder="输入用户名"></el-input>
      </el-form-item>
      <el-form-item label="">
        <el-input size="small" v-model="formInline.NickName" placeholder="输入用户昵称"></el-input>
      </el-form-item>
      <el-form-item label="">
        <el-input size="small" v-model="formInline.PhoneNumber" placeholder="输入手机号"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button size="small" type="primary" icon="el-icon-search" @click="search">搜索</el-button>
        <el-button size="small" type="primary" icon="el-icon-plus" @click="handleEdit()">添加</el-button>
        <!-- <el-button size="small" type="primary" @click="handleunit()">部门设置</el-button> -->
      </el-form-item>
    </el-form>
    <!--列表-->
    <el-table size="small" @selection-change="selectChange" :data="userData" highlight-current-row v-loading="loading"
      border element-loading-text="拼命加载中" style="width: 100%;">
      <el-table-column align="center" type="selection" width="50">
      </el-table-column>
      <el-table-column align="center" sortable prop="UserName" label="用户名" width="120">
      </el-table-column>
      <el-table-column align="center" sortable prop="NickName" label="姓名" width="120">
      </el-table-column>
      <el-table-column align="center" sortable prop="PhoneNumber" label="手机号" width="120">
      </el-table-column>
      <el-table-column align="center" sortable prop="Email" label="邮箱" min-width="120">
      </el-table-column>
      <el-table-column align="center" sortable prop="Country" label="区域/国家" min-width="120">
      </el-table-column>
      <el-table-column align="center" sortable prop="CreatedTime" label="创建时间" min-width="120">
        <template slot-scope="scope">
          <div>{{ scope.row.CreatedTime | timestampToTime }}</div>
        </template>
      </el-table-column>
      <el-table-column align="center" sortable prop="IsLocked" label="状态" min-width="50">
        <template slot-scope="scope">
          <el-switch v-model="!scope.row.IsLocked ? nshow : fshow" active-color="#13ce66" inactive-color="#ff4949"
            @change="editType(scope.$index, scope.row)">
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="操作" min-width="300">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="deleteUser(scope.$index, scope.row)">删除</el-button>
          <el-button size="mini" type="success" @click="resetpwd(scope.$index, scope.row)">重置密码</el-button>
          <!-- <el-button size="mini" type="success" @click="dataAccess(scope.$index, scope.row)">数据权限</el-button> -->
          <!-- <el-button size="mini" type="success" @click="offlineUser(scope.$index, scope.row)">下线</el-button> -->
          <!-- <el-button size="mini" type="success" @click="refreshCache(scope.$index, scope.row)">刷新缓存</el-button> -->
        </template>
      </el-table-column>
    </el-table>
    <!-- 分页组件 -->
    <Pagination v-bind:child-msg="pageparm" @callFather="callFather"></Pagination>
    <!-- 编辑界面 -->
    <el-dialog :title="title" :visible.sync="editFormVisible" width="30%" @click='closeDialog("edit")'>
      <el-form label-width="120px" ref="editForm" :model="editForm" :rules="rules">
        <el-form-item label="用户名" prop="UserName">
          <el-input size="small" v-model="editForm.UserName" auto-complete="off" placeholder="请输入用户名"></el-input>
        </el-form-item>
        <el-form-item label="姓名" prop="NickName">
          <el-input size="small" v-model="editForm.NickName" auto-complete="off" placeholder="请输入姓名"></el-input>
        </el-form-item>
        <!-- <el-form-item label="角色" prop="roleId">
          <el-select size="small" v-model="editForm.roleId" placeholder="请选择" class="userRole">
            <el-option label="公司管理员" value="1"></el-option>
            <el-option label="普通用户" value="2"></el-option>
          </el-select>
        </el-form-item> -->
        <el-form-item label="手机号" prop="PhoneNumber">
          <el-input size="small" v-model="editForm.PhoneNumber" placeholder="请输入手机号"></el-input>
        </el-form-item>
        <el-form-item label="邮件" prop="Email">
          <el-input size="small" v-model="editForm.Email" placeholder="请输入邮箱地址"></el-input>
        </el-form-item>
        <el-form-item label="区域/国家" prop="Country">
          <el-input size="small" v-model="editForm.Country" placeholder="请输入区域或国家"></el-input>
        </el-form-item>
        <!-- <el-form-item label="性别" prop="userSex">
          <el-radio v-model="editForm.userSex" label="男">男</el-radio>
          <el-radio v-model="editForm.userSex" label="女">女</el-radio>
        </el-form-item> -->
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" @click='closeDialog("edit")'>取消</el-button>
        <el-button size="small" type="primary" :loading="loading" class="title" @click="submitForm('editForm')">保存
        </el-button>
      </div>
    </el-dialog>
    <!-- 数据权限 -->
    <el-dialog title="数据权限" :visible.sync="dataAccessshow" width="30%" @click='closeDialog("perm")'>
      <el-tree ref="tree" default-expand-all="" :data="UserDept" :props="defaultProps" :default-checked-keys="checkmenu"
        node-key="id" show-checkbox>
      </el-tree>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" @click='closeDialog("perm")'>取消</el-button>
        <el-button size="small" type="primary" :loading="loading" class="title" @click="menuPermSave">保存</el-button>
      </div>
    </el-dialog>
    <!-- 所属单位 -->
    <el-dialog title="所属单位" :visible.sync="unitAccessshow" width="30%" @click='closeDialog("unit")'>
      <el-tree ref="tree" default-expand-all="" :data="UserDept" :props="defaultProps" @check-change="handleClick"
        :default-checked-keys="checkmenu" node-key="id" show-checkbox check-strictly>
      </el-tree>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" @click='closeDialog("unit")'>取消</el-button>
        <el-button size="small" type="primary" :loading="loading" class="title" @click="unitPermSave">保存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
// 导入请求方法
import {
  userList,
  userSave,
  userDelete,
  userPwd,
  userLock,
} from '../../api/userMG'
import Pagination from '../../components/Pagination'
export default {
  data() {
    return {
      nshow: true, //switch开启
      fshow: false, //switch关闭
      loading: false, //是显示加载
      title: '添加用户',
      editFormVisible: false, //控制编辑页面显示与隐藏
      dataAccessshow: false, //控制数据权限显示与隐藏
      unitAccessshow: false, //控制所属单位隐藏与显示
      // 编辑与添加
      editForm: {
        Id: '',
        UserName: '',
        NickName: '',
        // roleId: '',
        PhoneNumber: '',
        Email: '',
        Country: '',
      },
      // 部门参数
      unitparm: {
        userIds: '',
        deptId: '',
        deptName: ''
      },
      // 选择数据
      selectdata: [],
      // rules表单验证
      rules: {
        UserName: [
          { required: true, message: '请输入用户名', trigger: 'blur' }
        ],
        NickName: [
          { required: true, message: '请输入姓名', trigger: 'blur' }
        ],
        Country: [
          { required: true, message: '请输入国家或地区', trigger: 'blur' }
        ],
        PhoneNumber: [
          { required: true, message: '请输入手机号', trigger: 'blur' },
          {
            pattern: /^1(3\d|47|5((?!4)\d)|7(0|1|[6-8])|8\d)\d{8,8}$/,
            required: true,
            message: '请输入正确的手机号',
            trigger: 'blur'
          }
        ],
        Email: [
          { required: true, message: '请输入邮箱', trigger: 'blur' },
          {
            pattern: /^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/,
            required: true,
            message: '请输入正确的邮箱',
            trigger: 'blur'
          }
        ],
      },
      // 重置密码
      resetpsd: {
        Id: ''
      },
      // 请求数据参数
      formInline: {
        page: 1,
        size: 10,
        // deptId: '',
        UserName: '',
        NickName: '',
        PhoneNumber: '',
        IsLocked: ''
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
      // 获取用户列表
      userList(parameter).then(res => {
        this.loading = false
        if (res.Type != "Success") {
          this.$message({
            type: 'info',
            message: res.Content
          })
        } else {
          this.userData = res.Data
          // 分页赋值
          this.pageparm.currentPage = this.formInline.page
          this.pageparm.pageSize = this.formInline.size
          this.pageparm.total = res.count
        }
      })
    },
    // 分页插件事件
    callFather(parm) {
      this.formInline.page = parm.currentPage
      this.formInline.size = parm.pageSize
      this.getdata(this.formInline)
    },
    //搜索事件
    search() {
      this.getdata(this.formInline)
    },
    // 修改type
    editType: function (index, row) {
      this.loading = true
      let parm = {
        IsLocked: !row.IsLocked,
        Id: row.Id,
      }
      // 修改状态
      userLock(parm).then(res => {
        this.loading = false
        if (res.Type == 'Success') {
          this.$message({
            type: 'success',
            message: '状态修改成功'
          })
          this.getdata(this.formInline)
        } else {
          this.$message({
            type: 'info',
            message: res.Errors
          })
        }
      })
    },
    //显示编辑界面
    handleEdit: function (index, row) {
      this.editFormVisible = true
      if (row != undefined && row != 'undefined') {
        this.title = '修改用户'
        this.editForm.Id = row.Id
        this.editForm.UserName = row.UserName
        this.editForm.NickName = row.NickName
        // this.editForm.roleId = row.roleId
        this.editForm.PhoneNumber = row.PhoneNumber
        this.editForm.Email = row.Email
        this.editForm.Country = row.Country
        // this.editForm.userSex = row.userSex
      } else {
        this.title = '添加用户'
        this.editForm.Id = ''
        this.editForm.UserName = ''
        this.editForm.NickName = ''
        // this.editForm.roleId = ''
        this.editForm.PhoneNumber = ''
        this.editForm.Email = ''
        this.editForm.Country = ''
        // this.editForm.userSex = ''
      }
    },
    // 编辑、添加提交方法
    submitForm(editData) {
      this.$refs[editData].validate(valid => {
        if (valid) {
          // 请求方法
          userSave(this.editForm)
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
          userDelete({ Id: row.Id })
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
    // 重置密码
    resetpwd(index, row) {
      this.resetpsd.Id = row.Id
      this.$confirm('确定要重置密码吗?', '信息', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          userPwd(this.resetpsd)
            .then(res => {
              if (res.Type == 'Success') {
                this.$message({
                  type: 'success',
                  message: '重置密码成功！'
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
              this.$message.error('重置密码失败，请稍后再试！')
            })
        })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '取消重置密码！'
          })
        })
    },
    //数据格式化
    changeArr(data) {
      var pos = {}
      var tree = []
      var i = 0
      while (data.length != 0) {
        if (data[i].pId == 0) {
          tree.push({
            id: data[i].id,
            name: data[i].name,
            pId: data[i].pId,
            open: data[i].open,
            checked: data[i].checked,
            children: []
          })
          pos[data[i].id] = [tree.length - 1]
          data.splice(i, 1)
          i--
        } else {
          var posArr = pos[data[i].pId]
          if (posArr != undefined) {
            var obj = tree[posArr[0]]
            for (var j = 1; j < posArr.length; j++) {
              obj = obj.children[posArr[j]]
            }

            obj.children.push({
              id: data[i].id,
              name: data[i].name,
              pId: data[i].pId,
              open: data[i].open,
              checked: data[i].checked,
              children: []
            })
            pos[data[i].id] = posArr.concat([obj.children.length - 1])
            data.splice(i, 1)
            i--
          }
        }
        i++
        if (i > data.length - 1) {
          i = 0
        }
      }
      return tree
    },
    // 选中菜单
    changemenu(arr) {
      let change = []
      for (let i = 0; i < arr.length; i++) {
        if (arr[i].checked) {
          change.push(arr[i].id)
        }
      }
      return change
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

