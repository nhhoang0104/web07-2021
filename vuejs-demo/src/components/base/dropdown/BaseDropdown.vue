<template>
  <div>
    <div class="dropdown__label" v-show="label">
      {{ label }}
      <div class="dropdown__label__required" v-show="required">
        &nbsp;(<span>*</span>)
      </div>
    </div>
    <div
      :class="classNameDropdown"
      @click="clickDropdown"
      @blur="outFocus"
      tabindex="0"
    >
      <div class="dropdown__content">{{ content }}</div>
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
  </div>
</template>

<script>
import _ from "lodash";
export default {
  name: "dropdown",
  props: {
    id: {
      type: String,
      required: true,
    },
    label: {
      type: String,
      required: false,
    },
    required: {
      type: Boolean,
      required: false,
    },
    value: {
      type: [String, Number],
      required: true,
      default: "0",
    },
    data: {
      type: Array,
      required: true,
    },
  },
  provide() {
    return {
      pkey: this.$props.id,
    };
  },
  data() {
    return {
      content: "",
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
    value(newVal) {
      let index = _.findIndex(this.data, (item) => item.id === newVal);
      this.content = this.data[index].label;
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
