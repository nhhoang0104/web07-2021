<template>
  <div class="input">
    <div class="input__label" v-show="label">{{ label }}</div>
    <input
      type="text"
      :class="icon ? `icon input--icon ${icon} ` : ''"
      placeholder="Test 12313"
    />
    <div class="input__clear input__clear--hidden">
      <i class="fas fa-times icon icon--16"></i>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-input",
  props: {
    label: {
      type: String,
      required: false,
    },
    icon: {
      type: String,
      required: false,
    },
  },
};
</script>

<style lang="css" scpoed>
@import url("../../../css/common/Input.css");
@import url("../../../css/common/Icon.css");
</style>
