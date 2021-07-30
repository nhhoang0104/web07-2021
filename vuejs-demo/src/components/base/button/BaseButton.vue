<template>
  <div class="btn" :class="[className, circle ? 'btn--circle' : '']">
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: "base-button",
  props: {
    className: {
      default: "",
    },
    circle: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style lang="css">
@import url("../../../css/common/Button.css");
@import url("../../../css/common/Text.css");
@import url("../../../css/common/Padding.css");
@import url("../../../css/common/Margin.css");
</style>
