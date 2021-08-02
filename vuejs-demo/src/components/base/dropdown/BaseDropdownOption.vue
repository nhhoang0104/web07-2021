<template>
  <div
    class="dropdown__option"
    :value="value"
    :checked="checked"
    @click="selectItem(value)"
  >
    <div class="dropdown__option__check">
      <i class="fas fa-check icon icon--13"></i>
    </div>
    <div class="dropdown__option__label">{{ label }}</div>
  </div>
</template>

<script>
export default {
  name: "dropdown-option",
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
      requied: true,
    },
  },

  methods: {
    /*
      Xử lý chọn option trong dropdown
    */

    selectItem(value) {
      this.$emit("select-item", { key: this.pkey, id: value });
    },
  },
};
</script>
