import { createStore } from "vuex";
import axios from "axios";

const API_BASE = "https://localhost:7084/";

export default createStore({
  state: {
    groups: [],
  },

  mutations: {
    SET_GROUPS(state, payload) {
      state.groups = payload;
    },
  },

  getters: {
    getGroups: (state) => state.groups,
  },

  actions: {
    loadGroups({ commit }) {
      axios
        .get(API_BASE + "groups")
        .then((response) => response.data.data.groups)
        .then((items) => {
          //console.log(items)
          commit("SET_GROUPS", items);
        })
        .catch((error) => {
          console.error(error);
        });
    },
    async postUrl({ commit }, data) {
      console.log("Posting:" + data);
      let response = await axios
        .post(API_BASE + "shorten", data)
        .then((response) => console.log(response.data))
        .catch((error) => {
          console.error(error);
        });
      commit("SET_GROUPS", response);
    },
  },
  modules: {},
});
