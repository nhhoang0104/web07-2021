<template>
  <div class="menu">
    <div class="menu__header">
      <div class="menu__header__toggle"></div>
      <div class="menu__header__logo"></div>
    </div>
    <div class="menu__content">
      <div class="menu__item">
        <div class="menu__item__icon">
          <div class="icon icon--20 icon--dashboard"></div>
        </div>
        <div class="menu__item__text">Tổng quan</div>
      </div>
      <div class="menu__item">
        <div class="menu__item__icon">
          <div class="icon icon--report icon--20"></div>
        </div>
        <div class="menu__item__text">Báo cáo</div>
      </div>
      <div class="menu__item">
        <div class="menu__item__icon ">
          <div class="icon icon--dashboard icon--20"></div>
        </div>
        <div class="menu__item__text">Mua hàng</div>
      </div>
      <div class="menu__item">
        <div class="menu__item__icon">
          <div class="icon icon--20 icon--dic-employee"></div>
        </div>
        <div class="menu__item__text">Danh mục nhân viên</div>
      </div>
      <div class="menu__item">
        <div class="menu__item__icon ">
          <div class="icon icon--dic-employee icon--20"></div>
        </div>
        <div class="menu__item__text">Danh mục cá nhân</div>
      </div>
      <div class="menu__item">
        <div class="menu__item__icon">
          <div class="icon icon--setting icon--20"></div>
        </div>
        <div class="menu__item__text">Thiết lập hệ thống</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "the-menu",
};
</script>

<style lang="css" scoped>
@import "../../css/layout/Menu.css";
</style>
