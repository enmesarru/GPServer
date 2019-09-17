<script>
export default {
  name: 'AppBar'
}
</script>

<template>
    <nav class="navbar">
        <div class="navbar-item navbar-item-home">
            <!-- Home -->
            <router-link
                to="/">
            </router-link>
            <!-- Home -->
        </div>

        <div class="navbar-item navbar-item-login">
            <!-- Log-in -->
            <router-link
                to="/login">
            </router-link>
            <!-- Log-in -->
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
    height: 30px;
    width: 30px;
    background-repeat: no-repeat;
    background-size: cover;
}
.navbar-item-home a {
    background-image: url('./../../assets/home.svg');
}
.navbar-item-login a {
    background-image: url('./../../assets/log-in.svg');
}
</style>