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
        <slot name="dropdown-options" :options="data"></slot>
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
      type: [String, Number, null],
      required: true,
      default: null,
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

    value: {
      immediate: true,
      handler(newVal) {
        if (newVal === null) this.content = "";
        let index = _.findIndex(
          this.data,
          (item) => item.id.toString() === newVal.toString()
        );

        if (index !== -1) this.content = this.data[index].label;
        else this.content = "";
      },
    },

    data: {
      deep: true,
      handler(newVal) {
        let index = _.findIndex(
          newVal,
          (item) => item.id.toString() === this.value.toString()
        );

        if (index !== -1) this.content = this.data[index].label;
      },
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
