<template lang="html">
  <div class="dialog" v-show="show">
    <div class="dialog__header">
      <div class="dialog__header__close" @click="$emit('close-form')">
        <i class="fas fa-times icon icon--24"></i>
      </div>
      <slot name="header"></slot>
    </div>
    <div class="dialog__body"><slot name="body"></slot></div>
    <div class="dialog__footer"><slot name="footer"></slot></div>
  </div>
</template>

<script>
export default {
  name: "base-dialog",
  emits: ["close-form"],
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
@import url("../../css/common/Dialog.css");
@import url("../../css/common/Icon.css");
</style>
