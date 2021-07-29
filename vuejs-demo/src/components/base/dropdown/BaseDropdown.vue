<template>
  <div
    :class="classNameDropdown"
    :valueSelected="valueSelected"
    @click="clickDropdown"
    @blur="outFocus"
    tabindex="0"
  >
    <div class="dropdown__label">{{ label }}</div>
    <div class="dropdown__toggle">
      <i
        class="fas fa-chevron-down icon icon--16"
        :class="isShowed ? 'rotate--180' : ''"
      ></i>
    </div>
    <div :class="classNameDropdownMenu">
      <slot name="dropdown-options"></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "dropdown",
  props: {
    label: {
      type: String,
      required: true,
    },
    valueSelected: {
      type: String,
      required: false,
      value: null,
    },
  },

  data() {
    return {
      isShowed: false,
      classNameDropdown: "dropdown",
      classNameDropdownMenu: "dropdown__select dropdown__select--hide",
    };
  },
  watch: {
    isShowed(value) {
      if (!value) {
        this.classNameDropdown = "dropdown";
        this.classNameDropdownMenu = "dropdown__select dropdown__select--hide";
      } else {
        this.classNameDropdown = "dropdown dropdown--active";
        this.classNameDropdownMenu = "dropdown__select ";
      }
    },
  },
  methods: {
    /*
      xử lý sự kiện đóng hoặc mở dropdown.
    */

    clickDropdown() {
      this.isShowed = !this.isShowed;
    },

    outFocus() {
      this.isShowed = false;
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../../css/common/Dropdown.css");
</style>
