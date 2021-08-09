<template>
  <div :class="className">
    <div class="header__menu">
      <div class="header__menu__toggle" @click="minimize"></div>
      <div class="header__menu__logo"></div>
    </div>
    <div class="header__content">
      <div class="header__left">
        <div class="header__left__title">
          <div class="text text--title margin-right--10">
            Nhà hàng Biển Đông
          </div>
          <i class="fas fa-chevron-down icon icon--16"></i>
        </div>
        <div class="header__left__toggle"></div>
      </div>
      <div class="header__right">
        <div class="header__right__notify">
          <i class="far fa-bell icon icon--20"></i>
        </div>
        <div class="header__right__avatar"></div>
        <div class="header__right__name">Nguyễn Văn Mạnh</div>
        <div class="header__right__toggle">
          <div class="icon icon--20 icon--option"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "the-header",
  props: {
    className: {
      type: String,
      required: true,
    },
  },
  emits: ["minimize"],
  methods: {
    // thu nhỏ Menu
    minimize() {
      this.$emit("minimize");
    },
  },
};
</script>

<style lang="css">
@import url("../css/layout/Header.css");
</style>
