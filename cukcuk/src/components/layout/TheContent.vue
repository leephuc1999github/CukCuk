<template>
  <div class="content">
    <slot></slot>
    <div class="content-footer">
      <div class="paging">
        <div class="total-record">
          <p>Hiển thị <span class="total">1-10/1000</span> nhân viên</p>
        </div>
        <div class="pagination">
          <a class="back-to-start" href="#">
            <img src="../../assets/icon/btn-firstpage.svg" alt="">
          </a>
          <a class="pre-page" href="#">
            <img src="../../assets/icon/btn-prev-page.svg" alt="">
          </a>
          <div class="number-page">
            <a class="number active" href="#">
              <div class="number-text">1</div>
            </a>
            <a class="number" href="#">
              <div class="number-text">2</div>
            </a>
            <a class="number" href="#">
              <div class="number-text">3</div>
            </a>
            <a class="number" href="#">
              <div class="number-text">4</div>
            </a>
          </div>
          <div class="next-page">
            <img src="../../assets/icon/btn-next-page.svg" alt="">
          </div>
          <div class="go-to-end">
            <img src="../../assets/icon/btn-lastpage.svg" alt="">
          </div>
        </div>
        <div class="total-page">
          <p><span class="total">10</span> nhân viên/trang</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Content",
  components: {},
};
</script>

<style scoped>
.pagination {
  display: flex;
}
.back-to-start,
.go-to-end,
.next-page,
.pre-page {
  width: 34px;
  height: 34px;
  display: flex;
  margin:0 10px;
}

.back-to-start{
  margin-right: 10px;
}
.go-to-end{
  margin-left: 10px;
}
.back-to-start:hover,
.go-to-end:hover,
.next-page:hover,
.pre-page:hover {
  border-radius: 4px;
  background: #fff;
}

.number-page {
  display: flex;
}
.number-page > .number {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  border: 1px solid #bbbbbb;
  margin: 0 8px;
  text-decoration: none;
  color: #000;
}
.number.active{
  background: #019160;
  border: 1px solid #019160;
  color: #fff;
}

.number:not(.active):hover{
  background: #fff;
  border: 1px solid #fff;
}

.total-page {
  background: #fff;
  border-radius: 4px;
  padding: 8px 10px;
  display: flex;
}
.total-page p {
  padding: 0;
  margin: 0;
}
.total-record > p {
  padding: 0;
  margin: 0;
}
.total {
  font-family: "GoogleSans-Bold";
}
.content {
  /* width: calc(100vw - 227px); */
  background-color: #fff;
  height: calc(100vh - 49px);
  margin-left: 226px;
  /* background-color: red; */
  display: flex;
  flex-direction: column;
}
.content-footer {
  margin-top: auto;
  height: 56px;
  padding: 0 16px;
  /* background: red;  */
}
.paging {
  display: flex;
  height: 56px;
  align-items: center;
  justify-content: space-between;
}
</style>