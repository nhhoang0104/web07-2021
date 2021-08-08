<template lang="html">
  <span class="tooltip" :class="tooltip.active ? 'tooltip--active' : ''">{{
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
