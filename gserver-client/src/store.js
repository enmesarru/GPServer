import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import jwt_decode from 'jwt-decode';
import TokenManager from './utils/token.service';

Vue.use(Vuex);

const http = axios.create({
  baseURL: 'http://localhost:5000/api',
});

const options = {
  headers: {
    'Authorization': `Bearer ${TokenManager.getToken()}`,
  }
};

http.interceptors.request.use(
  config => {
    //  here is request handler before it sends
    // config.headers["Authorization"] = `Bearer ${TokenManager.getToken()}`;
    return config;
  },
  error => {
    console.log(error);
    Promise.reject(error);

  }
);

export default new Vuex.Store({
  state: {
    token: TokenManager.getToken() || '',
    status: '',
    canUserCreateGame: false,
    allGames: [],
    gameTypes: [],
    categories: [],
  },
  mutations: {
    auth_success(state, token) {
      state.status = 'success';
      state.token = token;
    },
    auth_error(state) {
      state.status = 'error';
    },
    logout(state) {
      state.status = '';
      state.token = '';
    },
    set_all_posts(state, games) {
      state.allGames = games;
    },
    set_all_categories(state, categories) {
      state.categories = categories;
    },
    set_all_gametypes(state, gametypes) {
      state.gameTypes = gametypes;
    },
    set_available_to_add_game(state, availability) {
      state.canUserCreateGame = availability;
    }
  },
  actions: {
    async login({ commit }, user) {
        try {
          const { data } = await http.post('/token', user);
          TokenManager.saveToken(data.token);
          commit('auth_success', data.token);
        } catch (error) {
          commit('auth_error');
          TokenManager.removeToken();
          throw Error(error);
        }
    },
    logout({ commit }) {
      commit('logout');
      TokenManager.removeToken();
    },
    async fetchAllGames({ commit }) {
      try {
        const result = await http.get("/game");
        commit('set_all_posts', result.data);
      } catch (error) {
        throw Error(error);
      }
    },
    async fetchCategories({ commit }) {
      try {
        const result = await http.get("/category");
        commit('set_all_categories', result.data);
      } catch (error) {
        throw Error(error);
      }
    },
    async fetchGameTypes({ commit }) {
      try {
        const result = await http.get("/gameroot");
        commit('set_all_gametypes', result.data);
      } catch (error) {
        throw Error(error);
      }
    },
    async register({ commit }, user) {
      try {
        const result = await http.post("/account", user);
      } catch(error) {
        throw Error(error);
      }
    },
    async createNewGame({ commit }, game) {
      try {
        await http.post("/game", game, options);
      } catch (error) {
        throw Error(error);
      }
    },
    async canCreateAGame({ commit }) {
      try {
        const {userId} = jwt_decode(TokenManager.getToken());
        const result = await http.get(`/game/user/${userId}`, options);
        commit('set_available_to_add_game', result.data);
      } catch(error) {
        throw Error(error);
      }
    }
  },
  getters: {
    isLoggedIn: state => !!state.token,
    authStatus: state =>  state.status,
    allGames: state => state.allGames,
    canUserCreateGame: state => state.canUserCreateGame,
    categories: state => 
      state.categories.map(value => 
        ({
          id: value.id,
          label: value.title
        })
      ),
    gameTypes: state => 
      state.gameTypes.map(value => 
        ({
          id: value.id,
          label: value.name
        })
      ),
    categoryById: state => id => state.categories.find(x => x.id === id),
    gameTypeById: state => id => state.gameTypes.find(x => x.id === id)
  }
});