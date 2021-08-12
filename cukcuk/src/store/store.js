import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    countTab: 1
  },
  getters : {

  },
  mutations: {
    incrementCountTab (state) {
      state.count++
    }
  },
  actions : {
    incrementCountTab(context){
        context.commit("increment");
    }
  }
})