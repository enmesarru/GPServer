import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import vSelect from 'vue-select';
import TokenManager from './utils/token.service';

import VModal from 'vue-js-modal';
Vue.use(VModal, { dialog: true });

import 'vue-select/dist/vue-select.css';
Vue.component('v-select', vSelect);

// Css
import 'milligram';
import './assets/css/style.css';

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
