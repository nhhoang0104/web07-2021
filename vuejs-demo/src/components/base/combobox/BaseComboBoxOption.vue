<template>
  <div
    class="combo-box__option"
    :value="value"
    :checked="checked"
    @click="selectItem($event, value)"
  >
    <div class="combo-box__option__check">
      <i class="fas fa-check icon icon--13"></i>
    </div>
    <div class="combo-box__option__label">{{ label }}</div>
  </div>
</template>

<script>
export default {
  name: "combo-box-option",
  emits: ["select-item"],
  inject: ["pkey"],
  props: {
    label: {
      type: String,
      requied: true,
    },
    value: {
      type: String,
      requied: true,
    },
    checked: {
      type: Boolean,
      requied: false,
      default: true,
    },
  },
  methods: {
    /*
      Xử lý chọn option trong dropdown
    */
    selectItem(event, value) {
      this.$emit("select-item", { key: this.pkey, id: value });
    },
  },
};
</script>
