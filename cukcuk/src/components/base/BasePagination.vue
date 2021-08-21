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
          v-for="page in textPages"
          :key="page.Value"
          @click="clickBtn(page.Value)"
          v-bind:class="current == page.Value ? 'active' : ''"
        >
          <div class="number-text">
            {{ page.Text }}
          </div>
        </a>
      </div>
      <div
        class="next-page"
        @click="clickBtn(current + 1 > allPages ? allPages : current + 1)"
      >
        <img src="../../assets/icon/btn-next-page.svg" alt="" />
      </div>
      <div class="go-to-end" @click="clickBtn(allPages)">
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
import EventBus from "../../EventBus";
export default {
  props: ["page", "size", "active"],
  data() {
    return {
      total: 0,
      numPages: 5,
      start: 0,
      end: 0,
      current: 1,
      totalPages: [],
      textPages: [],
      allPages: 0,
    };
  },
  created() {
    EventBus.$on("totalRecord", (value) => {
      this.total = value;
      this.paging();
    });
  },
  methods: {
    clickBtn(index) {
      this.current = index;
      this.start = (this.current - 1) * this.size + 1;
      if (this.current < this.numPages) {
        this.end = this.start + this.size - 1;
      } else {
        this.end = this.total;
      }
      EventBus.$emit("pagination", this.current);
    },

    /**
     * pagining
     * Author: LP(13/8)
     */
    paging() {
      this.allPages =
        this.total % this.size == 0
          ? this.total / this.size
          : Math.floor(this.total / this.size) + 1;
      this.textPages = [];
      for (let i = 1; i <= this.allPages; i++) {
        this.totalPages.push({ Text: i + "", Value: i });
      }

      this.current = this.active;

      if (this.current <= Math.round(this.numPages / 2)) {
        this.textPages = this.totalPages.slice(0, this.allPages < this.numPages ? this.allPages : this.numPages);
      } else if (
        this.current > Math.round(this.numPages / 2) &&
        this.current <= this.allPages - Math.floor(this.numPages / 2)
      ) {
        this.textPages = this.totalPages.slice(
          this.current - 3,
          this.current + 2
        );
      } else {
        this.textPages = this.totalPages.slice(
          this.allPages - this.numPages,
          this.allPages
        );
      }

      this.start = (this.current - 1) * this.size + 1;
      this.end =
        this.current < this.numPages ? this.start + this.size - 1 : this.total;
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