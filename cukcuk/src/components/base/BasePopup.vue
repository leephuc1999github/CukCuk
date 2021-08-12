<template>
  <div class="dark-screen" v-if="open">
    <div class="popup">
      <div class="popup-header-site">
        <p class="title">{{ header }}</p>
        <div class="popup-close-site" v-on:click="closePopup">&times;</div>
      </div>
      <div class="popup-content-site">
        <div class="popup-icon-site">
          <i class="fa fa-exclamation-triangle"></i>
        </div>
        <p class="popup-body-site">
          {{ content }}
        </p>
      </div>
      <div class="popup-action-site">
        <div class="box">
          <button class="misa-btn misa-btn-cancle" v-on:click="closePopup">Hủy</button>
          <button class="misa-btn misa-btn-action" v-on:click="confirmEvent">Xóa</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import EventBus from "../../EventBus";
export default {
    name : "Popup",
    props : [
        "header", "content"
    ],
    data() {
        return {
            open : false
        }
    },
    created() {
      EventBus.$on("openPopup", (value)=>{
        this.open = value;
      })
    },
    methods: {
      /**--------------------------------------
       * Close popup
       * Author : LP(2/8)
       */
      closePopup(){
        this.open = false;
      },

      /**-------------------------------------
       * Delete a record
       * Author : LP(2/8)
       */
      confirmEvent(){
        EventBus.$emit("confirmed", true);
        this.closePopup();
      }
    },
    beforeUnmount() {
    }
};
</script>

<style scoped>
.misa-btn{
  height: 40px;
  outline: none;
  border: none;
  border-radius: 4px;
  font-size: 13px;
  padding: 0 16px;
  min-width: 100px;
  font-family: "GoogleSans-Regular";
  cursor: pointer;
}

.misa-btn-action{
  background: #F65454;
  color : #fff;
}
.dark-screen {
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgb(0, 0, 0);
    background-color: rgba(0, 0, 0, 0.4);
  }
  .popup {
    background-color: #ffffff;
    border-radius: 4px;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    min-width: 400px;
  }
  .title {
    padding: 24px;
    margin: 0;
    font-size: 15px;
    font-weight: bold;
  }
  .popup-close-site {
    position: absolute;
    top: 0;
    right: 0;
    height: 38px;
    width: 38px;
    border-radius: 4px;
    font-size: 24px;
    text-align: center;
    cursor: pointer;
  }
  .popup-close-site:hover {
    background-color: #e5e5e5;
    color: #ffffff;
  }
  .popup-action-site {
    height: 60px;
    background-color: #e5e5e5;
    border-radius: 0 0 4px 4px;
    display: flex;
    padding: 0 24px;
  }
  .popup-action-site .box {
    margin-left: auto;
    align-self: center;
  }
  .popup-action-site .box button {
    margin-left: 16px;
  }
  
  .popup-content-site {
    padding: 24px;
    display: block;
    line-height: 38px;
  }
  .popup-icon-site {
    width: 38px;
    height: 38px;
    border-radius: 50%;
    background-color: #e5e5e5;
    margin-right: 10px;
    float: left;
    display: flex;
    justify-content: center;
  }
  .popup-icon-site i {
    text-align: center;
    display: block;
    font-size: 20px;
    align-self: center;
    color: #f65454;
  }
  .popup-content-site .popup-body-site{
      margin: 0;
      min-height: 38px;
  }
  .btnDelete {
  border: none;
  background: none;
  text-decoration: underline;
  cursor: pointer;
}
</style>