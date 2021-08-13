<template>
  <div class="input" :tabindex="tabIndex">
    <div class="input__label" v-show="label">
      {{ label }}
      <div class="input__label__required" v-if="required">
        &nbsp;(<span>*</span>)
      </div>
    </div>
    <input
      ref="focusMe"
      :type="type"
      :class="styleInput"
      :value="valueClone"
      :format="format"
      :placeholder="placeholder"
      :max="`${type === 'date' ? '2100-12-31' : null}`"
      @input="onChangeInput"
      @blur="validate"
      @focus="clearTool"
    />
    <span></span>
    <base-tooltip :tooltip="tooltip"></base-tooltip>
    <div class="input__clear input__clear--hidden">
      <i class="fas fa-times icon icon--16"></i>
    </div>
  </div>
</template>

<script>
import EnumDataType from "@/constants/EnumDataType.js";
import FormatData from "@/utils/FormatData.js";
import Validation from "@/utils/Validation.js";

export default {
  name: "base-input",

  props: {
    id: {
      type: String,
      required: true,
    },

    value: {
      type: [String, Number],
      required: false,
      default: "",
    },

    label: {
      type: String,
      required: false,
    },

    type: {
      type: String,
      required: false,
      default: EnumDataType.TEXT,
    },

    format: {
      type: String,
      required: false,
      default: EnumDataType.TEXT,
    },

    icon: {
      type: String,
      required: false,
    },

    placeholder: {
      type: String,
      default: "",
      required: false,
    },

    required: {
      type: Boolean,
      default: false,
      required: false,
    },

    tabIndex: {
      type: Number,
      required: false,
      default: 1,
    },
  },

  emits: ["handle-input"],

  data() {
    return {
      valueClone: "",
      isValidated: true,
      styleInput: "",
      tooltip: {
        active: false,
        message: "Không được bỏ trống",
      },
    };
  },

  watch: {
    // format giá trị khi thay đổi
    value(newVal) {
      this.valueClone = this.formatData(this.format, newVal);
    },

    // hiển tooltip khi isValidated === false
    isValidated: {
      immediate: true,
      handler(newVal) {
        let tmp = `${this.icon ? "icon input--icon " + this.icon : ""} ${
          newVal ? "" : " input__danger"
        }`;
        this.styleInput = tmp;
      },
    },
  },

  methods: {
    /*
      clear tooltip, danger when focus input
    */
    clearTool() {
      this.tooltip.active = false;
    },

    /*
      focus input
    */

    focusInput() {
      this.$nextTick(() => this.$refs.focusMe.focus());
    },

    /*
      clear tooltip
    */
    clearTooltip() {
      this.tooltip.active = false;
      this.isValidated = true;
    },

    /*
      Xử lý thay đổi dữ liệu
    */
    onChangeInput(event) {
      let tmp = event.target.value;

      if (this.format === EnumDataType.MONEY) {
        tmp = tmp.replaceAll(".", "");
        this.valueClone = FormatData.formatMoney(tmp);
      }

      this.$emit("handle-input", { id: this.id, value: tmp });
    },

    /*
      định dạng dữ liệu
    */
    formatData(type, value) {
      if (type === EnumDataType.MONEY) {
        return FormatData.formatMoney(value);
      }

      if (type === EnumDataType.DATE) {
        return FormatData.formatDateInput(value);
      }

      return value;
    },

    /*
        Validate value đúng theo validation chưa khi Blur
    */
    validate() {
      if (this.required === true) {
        let { isValidated, message } = Validation.validate(
          this.format,
          this.valueClone
        );

        this.tooltip = {
          active: !isValidated,
          message,
        };

        this.isValidated = isValidated;
      }
    },
  },
};
</script>

<style lang="css" scpoed>
@import url("../../css/common/Input.css");
@import url("../../css/common/Icon.css");
</style>
