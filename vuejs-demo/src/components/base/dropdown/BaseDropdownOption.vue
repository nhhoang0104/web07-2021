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
    selectItem(id) {
      this.$emit("select-item", id);
    },
  },
};
</script>
