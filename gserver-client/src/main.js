import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Axios from 'axios'

import VModal from 'vue-js-modal';
Vue.use(VModal, { dialog: true });

// Css
import 'milligram';
import './assets/css/style.css'

Vue.config.productionTip = false

Vue.prototype.$http = Axios;
const token = localStorage.getItem('token');

if(token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] = token;
}


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
