<template>
  <div class="content">
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: "Content",
  components: {  },
};
</script>

<style scoped>
.content {
  background-color: #fff;
  /* height: calc(100vh - 49px); */
  width: calc(100vw - 226px);
  display: flex;
  height: 100vh;
  flex-direction: column;
}
</style>