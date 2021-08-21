<template lang="html">
  <span class="tooltip tooltip--active" v-show="tooltip.active">{{
    tooltip.message
  }}</span>
</template>

<script>
export default {
  name: "base-tooltip",
  props: {
    tooltip: {
      type: Object,
      required: true,
    },
  },
};
</script>

<style lang="css">
@import url("../../css/common/Tooltip.css");
</style>
