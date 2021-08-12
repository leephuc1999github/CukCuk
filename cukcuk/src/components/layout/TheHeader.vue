<template>
  <div class="header">
    <div class="box">
      <div class="header-left">
        <div class="store-dropdown" tabindex="7">
            <div class="store-name">Cửa hàng biển đông</div>
            <i class="fa fa-angle-down" aria-hidden="true"></i>
        </div>
      </div>
      <div class="header-right">
        <div class="information-site">
          <div class="avatar-logo"></div>
          <div class="username">Lê Khắc Phúc</div>
        </div>
        <div class="more-icon" tabindex="8"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Header",
};
</script>

<style scoped>
.store-dropdown{
    display: flex;
    height: 40px;
    line-height: 40px;
    min-width: 100px;
    cursor: pointer;
}
.store-name{
    padding-left: 10px;
}
.store-dropdown i{
    display: block;
    line-height: 40px;
    padding: 0 12px;
}
.store-dropdown:focus{
    border: 1px solid #019160;
    border-radius: 4px;
    outline: none;
}
.header {
  height: 48px;
  background-color: #ffffff;
  border-bottom: 1px solid #e5e5e5;
  width: calc(100% - 227px);
  margin-left: 227px;
}
.header>.box {
  display: flex;
  padding: 0 16px;
  height: 100%;
}
.header-left {
  align-self: center;
}

.header-right {
  display: flex;
  right: 16px;
  position: absolute;
  height: 48px;
}

.information-site {
  display: flex;
  line-height: 48px;
}

.avatar-logo {
  align-self: center;
  height: 24px;
  width: 24px;
  background: url("../../assets/icon/avatar-default.png");
  background-size: contain;
  background-repeat: no-repeat;
  margin: 0 8px;
}

.more-icon {
  align-self: center;
  height: 24px;
  width: 24px;
  background: url("../../assets/icon/option.png");
  background-size: contain;
  background-repeat: no-repeat;
  margin-left: 8px;
}
.username {
  padding: 0 8px;
}

/* css select branch */
#branch > .drop-text-site {
  border: none;
}
/* end css select branch */
</style>