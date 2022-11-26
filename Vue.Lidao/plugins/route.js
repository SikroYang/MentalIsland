/*
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-11-22 10:11:49
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2022-11-26 11:04:32
 * @FilePath: \qundao\qundao\plugins\route.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */
import { Message } from 'element-ui';
export default ({ app, redirect }) => {
  console.log('全局插件执行了')
  app.router.afterEach((to, from) => {
    if (!$cookies.get('user')) {
      // return redirect('/login')
      console.log(1, to.path)
      if (to.path != '/' && to.path != '/home' && to.path != '/exhibition' && to.path != '/team' && to.path != '/signin' && to.path != '/login') {
        Message({
          type: 'error',
          message: '请先登录！'
        })
        app.router.push('/login')

      }

    }
  })
}


