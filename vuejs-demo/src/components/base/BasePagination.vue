<template>
  <div class="pagination">
    <div class="pagination__left">
      <div>Hiển thị <b>1-10/1000</b> nhân viên</div>
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
      <DxSelectBox :data-source="data" drop-down-button-template="imageIcon">
        <template #imageIcon="{}">
          <i class="icon icon--16 fas fa-sort"></i>
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
  data() {
    return {
      currentPage: 1,
      maxVisibleButtons: 4,
      totalPages: 10,
      data: [1, 2, 3, 4, 5, 6],
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

      // if (this.totalPages - this.currentPage < this.maxVisibleButtons) {
      //   return this.totalPages - this.maxVisibleButtons + 1;
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
  },

  methods: {
    // chọn index trang đầu tiên(=1)
    onClickFirstPage() {
      this.currentPage = 1;
    },

    // chọn index trang cuối cùng(=totalPages)
    onClickLastPage() {
      this.currentPage = this.totalPages;
    },

    // Chọn index trang ngẫu nhiễn từ range
    onClickPage(page = 1) {
      this.currentPage = page;
    },

    // chọn index trang tiếp theo của index hiện tại

    onClickNextPage() {
      this.currentPage = this.currentPage + 1;
    },

    // chọn index trang phía sau index hiện tại
    onClickPrevPage() {
      this.currentPage -= 1;
    },

    // xét index trang chọn thì class active
    isPageActive: function(page) {
      let tmp = "btn--border margin-left--10";

      if (this.currentPage === page) {
        tmp += " btn--active";
      } else tmp += " color-bg";

      return tmp;
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
</style>
