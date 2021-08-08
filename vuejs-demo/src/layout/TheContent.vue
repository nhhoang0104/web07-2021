<template>
  <div :class="className">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  props: {
    className: {
      type: String,
      required: true,
    },
  },
};
</script>

<style lang="css">
@import url("../css/layout/Page.css");
</style>
