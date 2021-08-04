<template>
  <div class="input">
    <div class="input__label" v-show="label">
      {{ label }}
      <div class="input__label__required" v-if="required">
        &nbsp;(<span>*</span>)
      </div>
    </div>
    <input
      :type="type"
      :class="icon ? `icon input--icon ${icon} ` : ''"
      :value="valueClone"
      :placeholder="placeholder"
      @input="onChangeInput"
    />
    <div class="input__clear input__clear--hidden">
      <i class="fas fa-times icon icon--16"></i>
    </div>
  </div>
</template>

<script>
import EnumDataType from "@/constants/EnumDataType.js";
import FormatData from "@/utils/FormatData.js";

export default {
  name: "base-input",

  props: {
    id: {
      type: String,
      required: true,
    },
    value: {
      type: [String, Number],
      required: true,
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
  },
  
  emits: ["handle-input"],

  data() {
    return { valueClone: "" };
  },

  watch: {
    value(newVal) {
      this.valueClone = this.formatData(this.format, newVal);
    },
  },

  methods: {
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
      if (type === EnumDataType.DATE) {
        return FormatData.formatDateInput(value);
      }

      if (type === EnumDataType.MONEY) {
        return FormatData.formatMoney(value);
      }

      return value;
    },
  },
};
</script>

<style lang="css" scpoed>
@import url("../../css/common/Input.css");
@import url("../../css/common/Icon.css");
</style>
