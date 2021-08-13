<template>
  <div class="paging">
    <div class="total-record">
      <p>
        Hiển thị
        <span class="total">{{ start }}-{{ end }}/ {{ total }}</span> nhân viên
      </p>
    </div>
    <div class="pagination">
      <a class="back-to-start" @click="clickBtn(1)">
        <img src="../../assets/icon/btn-firstpage.svg" alt="" />
      </a>
      <a class="pre-page" @click="clickBtn(current - 1 < 1 ? 1 : current - 1)">
        <img src="../../assets/icon/btn-prev-page.svg" alt="" />
      </a>
      <div class="number-page">
        <a
          class="number"
          v-for="index in numPages"
          :key="index"
          v-bind:class="index == current ? 'active' : ''"
          @click="clickBtn(index)"
        >
          <div class="number-text">{{ index }}</div>
        </a>
      </div>
      <div
        class="next-page"
        @click="clickBtn(current + 1 > numPages ? numPages : current + 1)"
      >
        <img src="../../assets/icon/btn-next-page.svg" alt="" />
      </div>
      <div class="go-to-end" @click="clickBtn(numPages)">
        <img src="../../assets/icon/btn-lastpage.svg" alt="" />
      </div>
    </div>
    <div class="total-page">
      <p>
        <span class="total">{{ size }}</span> nhân viên/trang
      </p>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Common from "../../Common";
import EventBus from "../../EventBus";
export default {
  props: ["page", "size", "active"],
  data() {
    return {
      total: 0,
      numPages: 0,
      start: 0,
      end: 0,
      current: 0,
    };
  },
  async created() {
    await this.countRecord();
    this.paging();
  },
  methods: {
    clickBtn(index) {
      this.current = index;
      if(this.current < this.numPages){
          this.start = (this.current-1)*12+1;
          this.end = this.start + this.size - 1;
      }else{
          this.start = (this.current-1)*12+1;
          this.end = this.total;
      }
      EventBus.$emit("pagination", this.current);
    },

    /**
     * pagining
     * Author: LP(13/8)
     */
    paging() {
      if (this.total % this.size == 0) {
        this.numPages = this.total / this.size;
      } else {
        this.numPages = Math.floor(this.total / this.size) + 1;
      }
      this.current = this.active;
      this.start = this.current;
      this.end = this.size;
    },

    /**
     * Get all employee api
     * Author : LP(7/8)
     */
    async countRecord() {
      try {
        let result = await axios.get(Common.APIURL+"employees");
        if (Common.isNullOrUndifined(result.data.Data)) {
          this.total = 0;
        } else {
          this.total = result.data.Data.length;
        }
      } catch (error) {
        console.log("getAllEmployees\n" + error);
      }
    },
  },
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
  margin: 0 10px;
  cursor: pointer;
}

.back-to-start {
  margin-right: 10px;
}
.go-to-end {
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
  cursor: pointer;
}
.number.active {
  background: #019160;
  border: 1px solid #019160;
  color: #fff;
}

.number:not(.active):hover {
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
.paging {
  display: flex;
  height: 56px;
  align-items: center;
  justify-content: space-between;
}
</style>