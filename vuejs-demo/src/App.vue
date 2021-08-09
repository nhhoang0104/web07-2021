<template>
  <div>
    <the-header className="header" @minimize="minimizeMenu"></the-header>
    <the-menu :isMinimize="isMinimize"></the-menu>
    <the-content
      :className="isMinimize ? 'page page--minimize' : 'page'"
    ></the-content>
  </div>
</template>

//
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
  
  data() {
    return {
      isMinimize: false,
    };
  },

  methods: {
    minimizeMenu() {
      this.isMinimize = !this.isMinimize;
    },
  },
};
</script>

<style lang="css">
@import url("./css/common/Main.css");
</style>
