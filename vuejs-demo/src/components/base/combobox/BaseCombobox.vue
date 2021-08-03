<template>
  <div :class="classNameComboBox" @blur="outFocus" tabindex="0">
    <div class="combo-box__label">
      <input
        type="text"
        :value="content"
        @focus="onFocusInput"
        @input="$emit('onchangeinput', { value: $event.target.value, key: id })"
      />
    </div>
    <div
      class="combo-box__toggle"
      :class="isShowed ? 'combo-box__toggle--active' : ''"
      @click="show"
    >
      <i
        class="fas fa-chevron-down icon icon--16"
        :class="isShowed ? 'rotate--180' : ''"
      ></i>
    </div>
    <div :class="classNameComboBoxMenu" @click="show">
      <slot name="combo-box-options"></slot>
    </div>
  </div>
</template>

<script>
import _ from "lodash";
export default {
  name: "combo-box",
  props: {
    value: {
      type: String,
      required: true,
    },
    id: {
      type: String,
      required: true,
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
  emits: ["onchangeinput"],
  data() {
    return {
      isShowed: false,
      classNameComboBox: "combo-box",
      classNameComboBoxMenu: "combo-box__select combo-box__select--hide",
      content: "",
    };
  },
  watch: {
    isShowed(value) {
      if (!value) {
        this.classNameComboBox = "combo-box";
        this.classNameComboBoxMenu =
          "combo-box__select combo-box__select--hide";
      } else {
        this.classNameComboBox = "combo-box combo-box--active";
        this.classNameComboBoxMenu = "combo-box__select";
      }
    },
    value: {
      immediate: true,
      handler(newVal) {
        let index = _.findIndex(this.data, (item) => item.id === newVal);
        if (index !== -1) this.content = this.data[index].label;
        else this.content = newVal;
      },
    },
  },
  methods: {
    /*
      xử lý sự kiện đóng hoặc mở combo-box.
    */

    show() {
      this.isShowed = !this.isShowed;
    },

    outFocus() {
      this.isShowed = false;
    },

    onFocusInput() {
      this.isShowed = true;
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../../css/common/ComboBox.css");
</style>
