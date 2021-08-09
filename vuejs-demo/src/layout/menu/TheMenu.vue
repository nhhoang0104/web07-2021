<template>
  <div class="menu" :class="isMinimize ? 'menu--minimize' : ''">
    <div class="menu__content">
      <menu-item
        v-for="item in content"
        :key="item.id"
        :isMinimize="isMinimize"
        :item="item"
        @select="selectMenuItem"
      ></menu-item>
    </div>
  </div>
</template>

<script>
import MenuItem from "./MenuItem.vue";
import { MenuModel } from "@/models/MenuModel.js";
import _ from "lodash";

export default {
  name: "the-menu",

  components: {
    "menu-item": MenuItem,
  },

  props: {
    isMinimize: {
      type: Boolean,
      required: true,
    },
  },

  data() {
    // Thêm thuộc tính active cho MenuItem
    let tmp = _.cloneDeep(MenuModel).map((item) => {
      item.active = false;
      return item;
    });

    return { content: tmp };
  },

  methods: {
    // chọn menu item
    selectMenuItem(id) {
      this.content = _.cloneDeep(MenuModel).map((item) => {
        if (item.id === id) item.active = true;
        else item.active = false;
        return item;
      });
    },
  },
};
</script>

<style lang="css" scoped>
@import "../../css/layout/Menu.css";
</style>
