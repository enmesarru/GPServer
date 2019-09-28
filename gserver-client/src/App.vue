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
  grid-gap: 5px;
  grid-template-columns: 70px 1fr;
  height: 100vh;
  background-color: #49d0f7;

}
.content{
  height: 100vh;
  padding: 10px;
  background-color: #e6e6e6;
  overflow-y: scroll;
}
</style>
