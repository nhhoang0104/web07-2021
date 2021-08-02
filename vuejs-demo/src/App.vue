<template>
  <div>
    <the-menu></the-menu>
    <the-header></the-header>
    <the-content></the-content>
  </div>
</template>

<script>
import TheHeader from "./layout/TheHeader.vue";
import TheMenu from "./layout/menu/TheMenu.vue";
import TheContent from "./layout/TheContent.vue";

export default {
  components: {
    "the-header": TheHeader,
    "the-menu": TheMenu,
    "the-content": TheContent,
  },
};
</script>

<style lang="css">
@import url("./css/common/Main.css");
</style>
