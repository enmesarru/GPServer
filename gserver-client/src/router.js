import Vue from 'vue';
import Router from 'vue-router';
import Home from '@/components/pages/Home';
import Login from '@/components/pages/Login';
import AddGame from '@/components/pages/AddGame';
import store from './store';
Vue.use(Router)

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/auth',
      component: Login,
      meta: {
        onlyWhenLoggedOut: true
      }
    },
    {
      path: '/game/create',
      requiresAuth: true,
      component: AddGame
    }
  ]
});

router.beforeEach( async (to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
  const onlyWhenLoggedOut = to.matched.some(record => record.meta.onlyWhenLoggedOut);
  const isLoggedIn = store.getters.isLoggedIn;

  if(requiresAuth && !isLoggedIn) {
    return next('/auth');
  }
  if(isLoggedIn && onlyWhenLoggedOut) {
    return next('/');
  }
  next();
});

export default router;