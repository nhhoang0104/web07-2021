<template>
  <button
    type="button"
    class="btn"
    :class="[className, circle ? 'btn--circle' : '']"
    @click="$emit('onclick')"
    :disabled="disable"
  >
    <slot></slot>
  </button>
</template>

<script>
export default {
  name: "base-button",
  emits: ["onclick"],
  props: {
    className: {
      default: "",
    },
    circle: {
      type: Boolean,
      default: false,
    },
    disable: {
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
