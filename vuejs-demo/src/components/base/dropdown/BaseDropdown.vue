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
      @keydown.enter="clickDropdown"
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
    <base-tooltip :tooltip="tooltip"></base-tooltip>
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
      default: "",
    },

    data: {
      type: Array,
      required: true,
    },

    className: {
      type: String,
      required: false,
      default: "",
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
      isValidated: true,
      isShowed: false,
      classNameDropdown: "dropdown",
      classNameDropdownMenu: "dropdown__select dropdown__select--hide",
      tooltip: {
        active: false,
        message: "Không được bỏ trống",
      },
    };
  },

  watch: {
    isShowed(value) {
      if (!value) {
        this.classNameDropdown = "dropdown";
        this.classNameDropdownMenu = "dropdown__select dropdown__select--hide";
      } else {
        this.classNameDropdown = "dropdown dropdown--active";
        this.classNameDropdownMenu = "dropdown__select " + this.className;
      }
    },

    value: {
      immediate: true,
      handler(newVal) {
        this.search(this.data, newVal);
      },
    },

    data: {
      deep: true,
      handler(newVal) {
        this.search(newVal, this.value);
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

    /*
        Validate value đúng theo validation chưa khi Blur
    */
    validate() {},

    /*
      clear tooltip
    */
    clearTooltip() {
      this.tooltip.active = false;
      this.isValidated = true;
    },

    search(data, value) {
      try {
        if (value === null || value === undefined) this.content = "";
        else {
          let tmp = value;
          if (typeof value === "number") tmp = value.toString();

          let index = _.findIndex(data, (item) => item.id === tmp);

          if (index !== -1) this.content = data[index].label;
          else this.content = "";
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../../css/common/Dropdown.css");
</style>
