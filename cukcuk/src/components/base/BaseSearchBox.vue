<template>
  <div class="search-box">
    <div class="box">
      <div class="search-icon-site">
        <i class="fa fa-search"></i>
      </div>
      <input
        v-model="key"
        type="text"
        class="input-search"
        placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại"
      />
      <div class="reset-txt-icon" v-if="show">
        <i
          class="fa fa-times-circle reset-icon"
          aria-hidden="true"
          @click="resetSearch"
        ></i>
      </div>
    </div>
  </div>
</template>

<script>
import EventBus from "../../EventBus";
import Common from "../../Common";
export default {
  data() {
    return {
      show: false,
      key: "",
    };
  },
  watch: {
    key() {
      if(Common.isNullOrUndifined(this.key)){
        this.show = false;
      }else{
        this.show = true;
      }
      EventBus.$emit("keyChange", this.key);
    },
  },
  methods: {
    focusSearch() {
      this.show = true;
    },

    resetSearch() {
      this.key = "";
      this.show = false;
    },
  },
};
</script>

<style scoped>
.search-box {
  border: 1px solid #bbbbbb;
  border-radius: 4px;
  width: 250px;
  background: #fff;
}
.search-box:focus-within {
  border: 1px solid #01b075;
}
.box {
  display: flex;
}
.search-icon-site {
  width: 16px;
  height: 16px;
  align-self: center;
  margin: 0 8px 0 16px;
}
.search-icon-site > i {
  font-size: 16px;
  color: #bbbbbb;
}
.input-search {
  border: none;
  outline: none;
  width: 100%;
  height: 38px;
  font-family: "GoogleSans-Regular";
  font-size: 13px;
  padding-right: 16px;
}
.reset-txt-icon {
  font-size: 16px;
  padding: 0 10px;
  display: flex;
  color: #bbbbbb;
}
.reset-txt-icon > i {
  align-self: center;
  cursor: pointer;
}
</style>