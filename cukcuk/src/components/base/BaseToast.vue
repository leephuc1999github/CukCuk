<template>
  <div class="toast" v-bind:class="type" v-if="open">
    <div class="toast-icon-site">
      <i v-bind:class="icon" aria-hidden="true"></i>
    </div>
    <div class="toast-content-site">{{ content }}</div>
    <div class="toast-close-site" v-on:click="closeToast">&times;</div>
  </div>
</template>

<script>
import EventBus from "../../EventBus";
export default {
  name: "Toast",
  data() {
    return {
      icon: null,
      open: false,
    };
  },
  props: ["name", "content", "type"],
  created() {
    EventBus.$on("openToast", (value) => {
      this.open = value;
    });

    switch (this.type) {
      case "fail":
        this.icon = "fa fa-exclamation-triangle";
        break;
      case "success":
        this.icon = "fa fa-check";
        break;
      case "warning":
        this.icon = "fa fa-exclamation";
        break;
      case "information":
        this.icon = "fa fa-info";
        break;
      default:
        this.icon = "fa fa-check";
        break;
    }
  },
  mounted() {
    setTimeout(() => {
      this.closeToast();
    }, 8000);
  },
  methods: {
    closeToast() {
      this.open = false;
    },
  },
  beforeUnmount() {
  },
};
</script>

<style scoped>
.toast{
    position: absolute;
    top: 20px;
    right: 20px;
    height: 48px;
    font-size: 13px;
    background: #ffffff;
    border-radius: 4px;
    color : #000000;
    box-shadow: 0 3px 6px rgb(0 0 0 / 0.16);
    min-width: 200px;
    display: flex;
    align-items: center;
    padding: 0 8px 0 10px;
}

.toast .toast-icon-site{
    height: 24px;
    width: 24px;
    border-radius: 50%;
    color: #ffffff;
    margin-right: 8px;
    display: flex;
}

.toast .toast-icon-site i{
    display: block;
    width: 100%;
    text-align: center;
    align-self: center;
}

.toast .toast-close-site{
    margin-left: auto;
    font-size: 24px;
    padding-left: 10px;
    cursor: pointer;
}

.toast.fail .toast-icon-site{
    background-color: #F65454;
}

.toast.fail .toast-close-site{
    color: #F65454;
}

.toast.success .toast-icon-site{
    background-color: #01B075;
}

.toast.success .toast-close-site{
    color: #01B075;
}

.toast.information .toast-icon-site{
    background-color: #0075FF;
}

.toast.information .toast-close-site{
    color : #0075FF;
}

.toast.warning .toast-icon-site{
    background-color: #F0E68C;
}

.toast.warning .toast-close-site{
    color : #F0E68C;
}
</style>