<template>
  <div class="navbar">
    <div class="logo-site">
      <div class="logo-box">
        <div class="toggle-icon"></div>
        <div class="cukcuk-logo"></div>
      </div>
    </div>
    <div class="navbar-content">
      <a href="/" class="nav-item" tabindex="1">
        <div class="nav-item-icon">
          <i class="fa fa-tachometer" aria-hidden="true"></i>
        </div>
        <div class="nav-item-text">Tổng quan</div>
      </a>
      <a href="/" class="nav-item" tabindex="2">
        <div class="nav-item-icon">
          <i class="fa fa-bar-chart" aria-hidden="true"></i>
        </div>
        <div class="nav-item-text">Báo cáo</div>
      </a>
      <a href="/" class="nav-item" tabindex="3">
        <div class="nav-item-icon">
          <i class="fa fa-shopping-cart" aria-hidden="true"></i>
        </div>
        <div class="nav-item-text">Mua hàng</div>
      </a>
      <a href="/" class="nav-item active" tabindex="4">
        <div class="nav-item-icon">
          <i class="fa fa-male" aria-hidden="true"></i>
        </div>
        <div class="nav-item-text">Danh mục nhân viên</div>
      </a>
      <a href="#/customer" class="nav-item" tabindex="5">
        <div class="nav-item-icon">
          <i class="fa fa-users" aria-hidden="true"></i>
        </div>
        <div class="nav-item-text">Danh mục khách hàng</div>
      </a>
      <a href="/" class="nav-item" tabindex="6">
        <div class="nav-item-icon">
          <i class="fa fa-cogs" aria-hidden="true"></i>
        </div>
        <div class="nav-item-text">Thiết lập hệ thống</div>
      </a>
    </div>
  </div>
</template>

<script>
export default {
  name: "Navbar",
};
</script>

<style scoped>
.navbar {
  height: 100vh;
  width: 226px;
  background-color: #ffffff;
  border-right: 1px solid #e5e5e5;
  float: left;
}

.logo-site {
  display: flex;
  width: 100%;
  height: 48px;
}
.logo-box {
  display: flex;
  width: 100%;
  padding: 12px 16px;
}

.toggle-icon {
  width: 29px;
  height: 100%;
  background-repeat: no-repeat;
  background-size: contain;
  background-image: url("../../assets/img/toggle.png");
}
.cukcuk-logo {
    margin-left: 10px;
  width: 100%;
  height: 24px;
  background-size: contain;
  background-repeat: no-repeat;
  background-image: url("../../assets/img/cukcuk-logo.png");
}

.navbar-content {
  width: 100%;
}
.nav-item {
  height: 44px;
  display: flex;
  padding: 0 16px;
  text-decoration: none;
  color: #000000;
}
.nav-item.active {
  background-color: #019160;
  color: #ffffff;
}
.nav-item:hover {
  background-color: #e5e5e5e5;
  color: #000000;
  cursor: pointer;
}

.nav-item-icon {
  align-self: center;
  height: 20px;
  width: 20px;
}
.nav-item-icon > i {
  font-size: 20px;
}
.nav-item-text {
  align-self: center;
  padding-left: 10px;
}
</style>