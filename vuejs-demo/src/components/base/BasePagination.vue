<template>
  <div class="pagination">
    <div class="pagination__left">
      <div class="text">
        Hiển thị
        <b>{{ informationPage }}/{{ totalRecord }}</b> nhân viên
      </div>
    </div>
    <div class="pagination__center">
      <base-button
        className="color-bg padding-left--10 padding-right--10"
        :disable="isInFirstPage"
        @onclick="onClickFirstPage"
      >
        <div class="icon icon--20 icon--firstpage"></div>
      </base-button>
      <base-button
        className="color-bg padding-left--10 padding-right--10 margin-left--10"
        @onclick="onClickPrevPage"
        :disable="isInFirstPage"
      >
        <div class="icon icon--20 icon--prevpage"></div>
      </base-button>
      <base-button
        v-for="page in pages"
        :key="page.name"
        @onclick="onClickPage(page.name)"
        :disable="page.isDisabled"
        :circle="true"
        :className="isPageActive(page.name)"
        ><div class="text">{{ page.name }}</div>
      </base-button>
      <base-button
        className="color-bg padding-left--10 padding-right--10 margin-left--10"
        :disable="isInLastPage"
        @onclick="onClickNextPage"
      >
        <div class="icon icon--20 icon--nextpage"></div>
      </base-button>
      <base-button
        className="color-bg padding-left--10 padding-right--10 margin-left--10"
        :disable="isInLastPage"
        @onclick="onClickLastPage"
      >
        <div class="icon icon--20 icon--lastpage"></div>
      </base-button>
    </div>
    <div class="pagination__right">
      <DxSelectBox
        :value="pageSize"
        :data-source="data"
        value-expr="value"
        display-expr="label"
        drop-down-button-template="imageIcon"
        item-template="field"
        @value-changed="valueChanged"
      >
        <template #imageIcon="{}">
          <i class="fas fa-sort custom-icon"></i>
        </template>
        <template #field="{ data }">
          <div class="custom-item">
            {{ data.label }}
          </div>
        </template>
      </DxSelectBox>
    </div>
  </div>
</template>

<script>
import BaseButton from "./button/BaseButton.vue";
import { DxSelectBox } from "devextreme-vue/select-box";

export default {
  components: { BaseButton, DxSelectBox },
  name: "base-pagination",

  props: {
    currentPage: {
      type: Number,
      required: true,
    },

    totalRecord: {
      type: Number,
      required: true,
    },

    totalPages: {
      type: Number,
      required: true,
    },

    pageSize: {
      type: Number,
      required: true,
    },
  },

  emits: ["select-page", "select-size"],

  data() {
    return {
      maxVisibleButtons: 3,
      data: [
        {
          value: 10,
          label: "10 nhân viên/trang",
        },
        {
          value: 20,
          label: "20 nhân viên/trang",
        },
        {
          value: 30,
          label: "30 nhân viên/trang",
        },
        {
          value: 40,
          label: "40 nhân viên/trang",
        },
        {
          value: 50,
          label: "50 nhân viên/trang",
        },
      ],
    };
  },

  computed: {
    // nếu đang ở index đầu (===1) thì disable btn
    isInFirstPage: function() {
      return this.currentPage === 1;
    },

    // nếu đang ở index cuối(===totalPage) thì disable btn
    isInLastPage: function() {
      return this.currentPage === this.totalPages;
    },

    //tạo index đầu
    startPage: function() {
      if (this.currentPage === 1) {
        return 1;
      }

      if (this.currentPage === this.totalPages) {
        return this.totalPages - this.maxVisibleButtons + 1;
      }

      // if (this.pageTotal - this.currentPage < this.maxVisibleButtons) {
      //   return this.pageTotal - this.maxVisibleButtons + 1;
      // }

      return this.currentPage - 1;
    },

    //tạo index cuối
    endPage: function() {
      return Math.min(
        this.startPage + this.maxVisibleButtons - 1,
        this.totalPages
      );
    },

    // tạo các index từ index đầu(startPage) đến index cuối(endPage)
    pages: function() {
      const range = [];

      for (let i = this.startPage; i <= this.endPage; i += 1) {
        range.push({
          name: i,
          isDisabled: i === this.currentPage,
        });
      }

      return range;
    },

    // hien thi thong tin trang
    informationPage: function() {
      if (this.currentPage < this.totalPages)
        return `${(this.currentPage - 1) * this.pageSize + 1}-${this
          .currentPage * this.pageSize}`;
      return `${this.totalRecord}`;
    },
  },

  methods: {
    // chọn index trang đầu tiên(=1)
    onClickFirstPage() {
      this.$emit("select-page", 1);
    },

    // chọn index trang cuối cùng(=pageTotal)
    onClickLastPage() {
      this.$emit("select-page", this.totalPages);
    },

    // Chọn index trang ngẫu nhiễn từ range
    onClickPage(page = 1) {
      this.$emit("select-page", page);
    },

    // chọn index trang tiếp theo của index hiện tại

    onClickNextPage() {
      this.$emit("select-page", this.currentPage + 1);
    },

    // chọn index trang phía sau index hiện tại
    onClickPrevPage() {
      this.$emit("select-page", this.currentPage - 1);
    },

    // xét index trang chọn thì class active
    isPageActive: function(page) {
      let tmp = "btn--border margin-left--10";

      if (this.currentPage === page) {
        tmp += " btn--active";
      } else tmp += " color-bg";

      return tmp;
    },

    // chọn kích cỡ trang
    valueChanged(e) {
      this.$emit("select-size", e.value);
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../css/common/Pagination.css");

.color-bg {
  background-color: var(--color-bg-hover);
  border-color: #bbb;
  transition: 0.2s all ease-in-out;
}

button.btn:hover {
  background-color: var(--color-white);
  border: none;
}

button.btn:active {
  background-color: var(--color-primary-default);
  color: var(--color-white);
}

button.btn.btn--active:hover {
  background-color: var(--color-primary-default);
  border: none;
}

button.btn:active div.text {
  color: var(--color-white);
}

.custom-icon {
  height: 100%;
  width: 100%;
}

.custom-item {
  margin-left: 5px;
  font-family: "GoogleSans" !important;
}

.dx-item.dx-list-item.dx-list-item-selected {
  background-color: #019160 !important;
  color: #fff !important;
}
</style>
