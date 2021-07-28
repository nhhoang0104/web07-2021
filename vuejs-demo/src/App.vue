<template>
  <div>
    <the-menu>
      <div></div>
    </the-menu>
    <the-header></the-header>
    <the-content></the-content>
  </div>
</template>

<script>
import TheHeader from "./components/layout/TheHeader.vue";
import TheMenu from "./components/layout/menu/TheMenu.vue";
import TheContent from "./components/layout/TheContent.vue";

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
