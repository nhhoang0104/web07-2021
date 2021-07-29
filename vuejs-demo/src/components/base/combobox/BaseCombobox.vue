<template>
  <div
    :class="classNameComboBox"
    :valueSelected="valueSelected"
    @blur="outFocus"
    tabindex="0"
  >
    <div class="combo-box__label">
      <input
        type="text"
        :label="label"
        @focus="onFocusInput"
        @blur="outFocus"
        @input="$emit('onchangeinput', $event.target.value)"
      />
    </div>
    <div class="combo-box__toggle" @click="show">
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
export default {
  name: "combo-box",
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
  emits: ["onchangeinput"],
  data() {
    return {
      isShowed: false,
      classNameComboBox: "combo-box",
      classNameComboBoxMenu: "combo-box__select combo-box__select--hide",
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
