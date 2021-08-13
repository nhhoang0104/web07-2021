<template>
  <div :class="classNameComboBox" @blur="outFocus" tabindex="0">
    <div class="combo-box__label">
      <input
        type="text"
        @focus="onFocusInput"
        @blur="outFocusInput"
        v-model="textSearch"
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
      <slot name="combo-box-options" :options="dataClone"></slot>
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

  data() {
    return {
      isShowed: false,
      classNameComboBox: "combo-box",
      classNameComboBoxMenu: "combo-box__select combo-box__select--hide",
      dataClone: _.cloneDeep(this.data),
      textSearch: "",
      valueSelected: "",
    };
  },

  watch: {
    /*
      Xử lý style combobox khi đóng(hoặc mở) combobox
    */
    isShowed(value) {
      if (!value) {
        this.classNameComboBox = "combo-box";
        this.classNameComboBoxMenu =
          "combo-box__select combo-box__select--hide";
      } else {
        this.dataClone = _.cloneDeep(this.data);
        this.classNameComboBox = "combo-box combo-box--active";
        this.classNameComboBoxMenu = "combo-box__select";
      }
    },

    data: {
      deep: true,
      handler(newVal) {
        this.dataClone = newVal;
        let tmp = this.dataClone.find((item) => item.id === this.value);

        if (tmp) this.textSearch = tmp.label;
      },
    },

    /*
      Lấy label theo props.value từ dataClone
    */
    value: {
      immediate: true,
      handler(newVal) {
        let tmp = this.dataClone.find((item) => item.id === newVal);
        if (tmp) this.textSearch = tmp.label;
      },
    },

    textSearch(newVal) {
      const text = _.trim(newVal);

      if (text === "" || text === undefined || text === null) {
        this.dataClone = _.cloneDeep(this.data);
      } else {
        this.dataClone = _.filter(
          this.data,
          (item) => item.label.toLowerCase().indexOf(text.toLowerCase()) > -1
        );
      }
    },
  },

  computed: {
    /*
      Tìm kiếm các option theo text search từ dataClone.
    */
    getOptions() {
      const text = _.trim(this.textSearch);

      if (text === "" || text === undefined || text === null) {
        return _.cloneDeep(this.dataClone);
      } else {
        return _.filter(
          this.dataClone,
          (item) => item.label.toLowerCase().indexOf(text.toLowerCase()) > -1
        );
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
    /*
      xử lý outfocus vào combobox
    */

    outFocus() {
      this.isShowed = false;
    },

    /*
      xử lý outfocus vào input 
    */
    outFocusInput() {
      let tmp = this.dataClone.find((item) => item.id === this.value);
      this.textSearch = tmp.label;

      setTimeout(() => {
        this.isShowed = false;
      }, 100);
    },

    /*
      xử lý focus vào input
    */
    onFocusInput() {
      this.isShowed = true;
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../../css/common/ComboBox.css");
</style>
