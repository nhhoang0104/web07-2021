<template>
  <div class="input">
    <div class="input__label" v-show="label">
      {{ label }}
      <div class="input__label__required" v-if="required">
        &nbsp;(<span>*</span>)
      </div>
    </div>
    <input
      :type="type"
      :class="icon ? `icon input--icon ${icon} ` : ''"
      :placeholder="placeholder"
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
    type: {
      type: String,
      required: false,
      default: "text",
    },
    format: {
      type: String,
      required: false,
      default: "text",
    },

    icon: {
      type: String,
      required: false,
    },
    placeholder: {
      type: String,
      default: "",
      required: false,
    },
    required: {
      type: Boolean,
      default: false,
      required: false,
    },
  },
};
</script>

<style lang="css" scpoed>
@import url("../../css/common/Input.css");
@import url("../../css/common/Icon.css");
</style>
