<template>
  <div class="pagination">
    <div class="pagination__left">
      <div>Hiển thị 1-10/1000 nhân viên</div>
    </div>
    <div class="pagination__center">
      <base-button className="color-bg padding-left--10 padding-right--10">
        <div class="icon icon--20 icon--firstpage"></div>
      </base-button>
      <base-button
        className="color-bg padding-left--10 padding-right--10 margin-left--10"
      >
        <div class="icon icon--20 icon--prevpage"></div>
      </base-button>
      <base-button
        :circle="true"
        className="btn--border color-bg margin-left--10"
        ><div class="text">1</div>
      </base-button>
      <base-button
        :circle="true"
        className="btn--border color-bg margin-left--10"
        ><div class="text">2</div>
      </base-button>
      <base-button
        :circle="true"
        className="btn--border color-bg margin-left--10"
        ><div class="text">3</div>
      </base-button>
      <base-button
        :circle="true"
        className="btn--border color-bg margin-left--10"
        ><div class="text">4</div>
      </base-button>
      <base-button
        className="color-bg padding-left--10 padding-right--10 margin-left--10"
      >
        <div class="icon icon--20 icon--nextpage"></div>
      </base-button>
      <base-button
        className="color-bg padding-left--10 padding-right--10 margin-left--10"
      >
        <div class="icon icon--20 icon--lastpage"></div>
      </base-button>
    </div>
    <div class="pagination__right">
      <div>10 nhân viên/trang</div>
    </div>
  </div>
</template>

<script>
import BaseButton from "./button/BaseButton.vue";
export default {
  components: { BaseButton },
  name: "base-pagination",
};
</script>

<style lang="css" scoped>
@import url("../../css/common/Pagination.css");
@import url("../../css/common/Margin.css");
@import url("../../css/common/Icon.css");
.color-bg {
  background-color: var(--color-white);
}

div.btn:hover {
  background-color: var(--color-bg-hover);
}

div.btn:active {
  background-color: var(--color-primary-default);
}

div.btn:active div.text {
  color: var(--color-white);
}
</style>
