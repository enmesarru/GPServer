<script>
import AppNavbar from "./components/shared/AppNavbar";
export default {
  name: 'App',
  components: {
    AppNavbar
  }
}
</script>
<template>
  <div id="container">
    <app-navbar />
    <div class="content">
      <router-view/>
    </div>
  </div>
</template>

<style>
html {
  box-sizing: border-box;
}
*, *:before, *:after {
  padding: 0;
  margin: 0;
}

#container {
  display: grid;
  grid-template-areas: "navbar content";
  grid-gap: 10px;
  grid-template-columns: 70px 1fr;
  height: 100vh;
  background-color:#011627;

}
.content{
  height: 100vh;
}
</style>
