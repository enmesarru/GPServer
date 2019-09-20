import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    status: {
      token: localStorage.getItem('token') || '',
      user: {}
    }
  },
  mutations: {

  },
  actions: {
    login({ commit }, user) {
      return new Promise((resolve, reject) => {
        // commit('auth_request');
        axios({ url: 'http://localhost:5000/api/token', data: user, method: 'POST'})
          .then(response => {
            const token = response.data.token;
          })
      });
    }
  },
  getters: {

  }
})
