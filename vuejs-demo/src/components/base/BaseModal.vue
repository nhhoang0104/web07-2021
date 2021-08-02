<template>
  <div class="modal" v-show="show"></div>
</template>

<script>
export default {
  name: "base-modal",
  props: {
    show: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
};
</script>

<style lang="css">
@import url("../../css/common/Modal.css");
</style>
