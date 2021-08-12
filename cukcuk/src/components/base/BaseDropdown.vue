<template>
  <div class="dropdown" v-on:click="openBlock">
    <div class="drop-text-site">
      <div class="drop-text">{{ current.Text }}</div>
      <div class="drop-icon">
        <i class="fa fa-angle-down" aria-hidden="true"></i>
      </div>
    </div>
    <transition name="slide">
      <div class="drop-data-site" v-if="open">
        <div
          class="drop-item"
          v-for="(option, index) in options"
          :key="option.Value"
          v-on:click="selectOption(index)"
          v-bind:class="{ selected: activeOption(index) }"
        >
          <div
            class="checked-icon"
            v-html="activeOption(index) ? iconSelected : ''"
          ></div>
          <div class="item-text">{{ option.Text }}</div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import axios from "axios";
import EventBus from "../../EventBus";
// import Common from "../../Common";
export default {
  name: "Dropdown",
  props: ["url", "fields", "data", "name", "value", "type"],
  created() {
    // debugger; // eslint-disable-line
    this.getOptions();
  },
  computed: {},
  data() {
    return {
      current: {},
      options: [],
      open: false,
      currentIndex: 0,
      iconSelected: `<i class="fa fa-check"></i>`,
      counter: 0,
    };
  },
  mounted() {
    document.addEventListener("click", this.closeDataSite);
  },
  watch: {
    value() {
      this.getOptions();
    },
  },
  methods: {
    /**-------------------------------------------------
     * Get options from api
     * Author : LP(31/7)
     */
    getOptions() {
      try {
        let response = this.getDataApi();
        let self = this;
        response.then((array) => {
          self.options = [];

          self.data.forEach((element) => {
            self.options.push(element);
          });
          if (array instanceof Array) {
            for (let i = 0; i < array.length; i++) {
              self.options.push({
                Text: array[i][self.fields[1]],
                Value: array[i][self.fields[0]],
              });
            }
          }
          let i = self.getIndex();
          if (self.options.length > 0) {
            self.selectOption(i);
          }
        });
      } catch (error) {
        console.log(this.name + "-getOptions\n" + error);
      }
    },

    /**
     * Get options from api
     * Author : LP(9/8)
     */
    async getDataApi() {
      let response = await axios.get(this.url);
      return response.data;
    },

    /**--------------------------------------------------
     * Open data site
     * Author : LP(29/7)
     */
    openBlock() {
      this.open = !this.open;
    },

    /**----------------------------------------------------
     * Select option
     * Author : LP(29/7)
     */
    selectOption(index) {
      this.current = this.options[index];
      this.currentIndex = index;
      EventBus.$emit("changed", {
        Value: this.current.Value,
        Name: this.name,
        Type: this.type,
      });
    },

    /**----------------------------------------------------
     * Add active class when option selected
     * Author : LP(30/7)
     */
    activeOption(index) {
      return this.currentIndex == index ? true : false;
    },

    /**-----------------------------------------------------
     * Get index option by value
     * Author : LP(30/7)
     */
    getIndex() {
      let result = this.options.findIndex((element, index) => {
        if (element.Value === this.value) {
          return index;
        }
      });
      return result === -1 ? 0 : result;
    },

    /**-------------------------------------------------------
     * Close data site
     * Author : LP(4/8)
     */
    closeDataSite(event) {
      if (!this.$el.contains(event.target)) {
        this.open = false;
      }
    },
  },
  beforeUnmount() {
    document.removeEventListener("click", this.closeDataSite);
  },
};
</script>

<style scoped>
/* core dropdown */

.dropdown {
  position: relative;
  cursor: pointer;
  border-radius: 4px;
  background: #fff;
  min-width: 200px;
  border: 1px solid #bbbbbb;
}
.dropdown:focus {
  border: 1px solid #01b075;
}

.drop-icon.rotate-180 {
  -moz-transform: rotate(180deg);
  -webkit-transform: rotate(180deg);
  -o-transform: rotate(180deg);
  -ms-transform: rotate(180deg);
  transform: rotate(180deg);
}

.drop-text-site {
  height: 38px;
  border-radius: 4px;
  display: flex;
}

.drop-text {
  line-height: 40px;
  padding: 0 16px;
  white-space: nowrap;
}

.drop-icon {
  cursor: pointer;
  margin-left: auto;
  padding: 0 12px;
}

.drop-icon > i {
  font-size: 16px;
  line-height: 40px;
}

.drop-data-site {
  position: absolute;
  top: 100%;
  width: 100%;
  border-radius: 4px;
  padding: 4px 0;
  box-shadow: 1px 2px 1px 2px #bbbbbb;
  z-index: 1;
  background-color: #ffffff;
}

.drop-item {
  display: flex;
  height: 40px;
  line-height: 40px;
  cursor: pointer;
}

.drop-item:hover {
  background: #e9ebee;
}

.checked-icon {
  width: 16px;
  align-self: center;
  margin-left: 10px;
  margin-right: 10px;
  text-align: center;
}

.checked-icon > i {
  font-size: 16px;
  margin: auto 0;
}

.item-text {
  font-size: 13px;
  color: #000000;
}

.drop-item.selected {
  background-color: #01b075;
}

.drop-item.selected > .item-text {
  color: #ffffff;
}

.drop-item.selected > .checked-icon {
  color: #ffffff;
}

/* end core dropdown */
</style>