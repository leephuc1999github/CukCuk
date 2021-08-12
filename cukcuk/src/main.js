import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import { routes } from './router/router'
import Toast from 'vue-toastification'
import "vue-toastification/dist/index.css";
Vue.config.productionTip = false

const options = {
  // You can set your default options here
}
Vue.use(Toast, options)
Vue.use(VueRouter)
const router = new VueRouter({
  routes
})
new Vue({
  render: h => h(App),
  router
}).$mount('#app')
