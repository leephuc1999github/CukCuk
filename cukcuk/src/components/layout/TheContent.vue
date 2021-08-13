<template>
  <div class="content">
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: "Content",
  components: {},
};
</script>

<style scoped>

.content {
  /* width: calc(100vw - 227px); */
  background-color: #fff;
  height: calc(100vh - 49px);
  margin-left: 226px;
  /* background-color: red; */
  display: flex;
  flex-direction: column;
}

</style>