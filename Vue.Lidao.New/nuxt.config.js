/*
 * @Author: error: git config user.name && git config user.email & please set dead value or install git
 * @Date: 2022-11-04 14:31:55
 * @LastEditors: error: git config user.name && git config user.email & please set dead value or install git
 * @LastEditTime: 2023-02-02 10:33:38
 * @FilePath: \qundao\qundao\nuxt.config.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */
export default {
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    title: '离岛',
    htmlAttrs: {
      lang: 'en'
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
    ]
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
    'element-ui/lib/theme-chalk/index.css',
    'quill/dist/quill.snow.css',
    'quill/dist/quill.bubble.css',
    'quill/dist/quill.core.css'
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    '@/plugins/element-ui',
    {
      src: '@/plugins/cookies',
      ssr: false
    },
    // { src: '@/plugins/route.js', ssr: false },
    { src: "@/plugins/vue-quill-editor.js", ssr: false },
    { src: '@plugins/lib-flexible.js', ssr: false },
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    // https://go.nuxtjs.dev/axios
    '@nuxtjs/axios',
    //  //有参数配置的
    'cookie-universal-nuxt', ['cookie-universal-nuxt', {
      parseJSON: true
    }],
    // 无参数配置
    'cookie-universal-nuxt'
  ],

  // Axios module configuration: https://go.nuxtjs.dev/config-axios
  axios: {
    // Workaround to avoid enforcing hard-coded localhost:3000: https://github.com/nuxt-community/axios-module/issues/308
    baseURL: 'https://www.lidaoisland.com',
    // baseURL: 'http://localhost',
    // headers: { 'Content-Type': 'application/json', 'crossDomain': true },
    // withCredentials: true
  },
  // proxy: {
  //   '/Api': {
  //     'target':'http://192.168.123.103:5773/',
  //     'changeOrigin': true,
  //     'pathRewrite': {
  //       '^/Api': '/Api'
  //     }
  //   },
  // },
  server: {
    port: 8100, // default: 3000
    host: '0.0.0.0' // default: localhost,
  },
  router: {
    base: '/'
  },

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
    transpile: [/^element-ui/],
    postcss: [
      require('postcss-px2rem')({
        remUnit: 192  // 之所以写192是因为设了pc最大宽度1920px
      })
    ],
  }
}
