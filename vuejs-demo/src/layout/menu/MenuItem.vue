<template lang="html">
  <router-link :to="item.link" class="menu__item">
    <div class="menu__item__icon">
      <div class="icon icon--20" :class="item.iconName"></div>
    </div>
    <div class="menu__item__text" v-show="!isMinimize">{{ item.text }}</div>
  </router-link>
</template>

<script>
export default {
  name: "menu-item",
  emits: ["select"],
  props: {
    item: {
      type: Object,
      required: true,
    },
    isMinimize: {
      type: Boolean,
      required: true,
    },
  },
  data() {
    return {};
  },
};
</script>

<style lang="css"></style>
