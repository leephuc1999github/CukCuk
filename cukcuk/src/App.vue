<template>
  <div id="app">
    <Navbar></Navbar>
    <Header></Header>
    <Content>
      <router-view></router-view>
    </Content>
  </div>
</template>

<script>
import Header from './components/layout/TheHeader.vue'
import Navbar from './components/layout/TheNavbar.vue'
import Content from './components/layout/TheContent.vue'
export default {
  name: 'App',
  components: {
    Header,
    Navbar,
    Content
  }
}
</script>

<style scoped>
@import "./styles/main.css";
@import "./styles/date-picker.css";
</style>
