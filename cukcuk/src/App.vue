<template>
  <div id="app">
    <Navbar></Navbar>
    <Content>
      <Header></Header>
      <router-view></router-view>
    </Content>
  </div>
</template>

<script>
import Header from "./components/layout/TheHeader.vue";
import Navbar from './components/layout/TheNavbar.vue'
import Content from './components/layout/TheContent.vue'
export default {
  name: 'App',
  components: {
    Navbar,
    Content,
    Header
  }
}
</script>

<style scoped>
@import "./styles/main.css";
@import "./styles/date-picker.css";
#app{
  display: flex;
  flex-direction: row;
  height: 100vh;
}
</style>
