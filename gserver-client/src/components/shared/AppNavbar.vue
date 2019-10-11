<script>
import { mapGetters } from 'vuex';
import TokenManager from '../../utils/token.service';
import jwt_decode from 'jwt-decode';
export default {
  name: 'AppBar',
  methods: {
      logout() {
          this.$store.dispatch('logout')
          .then(() => {
              this.$router.push('/auth')
          });
      }
  },
  computed: {
    ...mapGetters([
        'isLoggedIn'
    ]),
    userIdentifier() {
        const {sub} = jwt_decode(TokenManager.getToken());
        return sub;
    }
  }
}
</script>

<template>
    <nav class="navbar">
        <div class="navbar-item navbar-item-home">
            <!-- Home -->
            <router-link
                class="navbar-icon-item"
                to="/">
            </router-link>
            <!-- Home -->
        </div>

        <div class="navbar-item navbar-item-login" v-if="!isLoggedIn">
            <!-- Log-in -->
            <router-link
                class="navbar-icon-item"
                to="/auth">
            </router-link>
            <!-- Log-in -->
        </div>

        <div class="navbar-item" v-if="isLoggedIn">
            <router-link :to="{ name: 'usergames', params: {id: userIdentifier}}">
                Games
            </router-link>
        </div>

        <div class="navbar-item" v-if="isLoggedIn">
            <router-link :to="{ name: 'profile' }">
                Profile
            </router-link>
        </div>

        <div class="navbar-item" v-if="isLoggedIn">
            <router-link :to="{ name: 'create-game' }">
                Create
            </router-link>
        </div>
        <div class="navbar-item" v-if="isLoggedIn">
            <a @click="logout"> Logout </a>
        </div>
        
    </nav>
</template>

<style>
.navbar {
    background-color: #ff9f1c;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: stretch;
    
}
.navbar .navbar-item {
    flex-grow: 1;
    display: flex;
    justify-content: center;
    align-items: center;
}

.navbar-item a {
    color: #000;
    font-weight: bolder;
    transition: color 0.5s;
}

.navbar-item a:hover {
    color: #fff;
}

.navbar-icon-item {
    width: 30px;
    background-repeat: no-repeat;
    background-size: cover;
    height: 30px;
}
.navbar-item-home a {
    background-image: url('./../../assets/home.svg');
}
.navbar-item-login a {
    background-image: url('./../../assets/log-in.svg');
}
</style>