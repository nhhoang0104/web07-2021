<template>
  <div class="header">
    <div class="header__left">
      <div class="header__left__text">
        <div class="text text--title">Nhà hàng Biển Đông</div>
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
</template>

<script>
export default {
  name: "the-header",
};
</script>

<style lang="css">
@import url("../../css/layout/Header.css");
</style>
