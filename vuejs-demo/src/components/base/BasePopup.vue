<template>
  <base-modal :show="popup.isShowed"></base-modal>
  <div :class="className">
    <div class="pop-up__close" @click="this.$emit('show', false)">
      <i class="fas fa-times"></i>
    </div>
    <div class="pop-up__header">{{ popup.title }}</div>
    <div class="pop-up__content">
      <div class="pop-up__content__icon">
        <i class="fas fa-exclamation-triangle"></i>
      </div>
      <div class="pop-up__content__label">{{ popup.content }}</div>
    </div>
    <div class="pop-up__footer">
      <div class="pop-up__footer__btn btn--cancel" @click="handleCancel">
        Huỷ
      </div>
      <div class="pop-up__footer__btn btn--done" @click="handleAction">
        Đồng ý
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-popup",

  props: {
    popup: {
      type: Object,
      required: true,
    },
    action: {},
  },

  emits: ["show"],

  watch: {
    popup: {
      deep: true,
      immediate: true,
      handler(newVal) {
        if (newVal.isShowed === true) {
          this.className = "pop-up";
        } else this.className = "pop-up pop-up--hidden";
      },
    },
  },

  data() {
    return {
      className: "pop-up pop-up--hidden",
    };
  },

  methods: {
    /*
      Khi ấn nút done dẽ thực hiện hành động đã được truyền vào
    */
    handleAction() {
      this.$emit("show", false);
      this.popup.action();
    },

    /*
      Khi ấn nút cancel sẽ không thực hành động đẫ được truyền vào
    */
    handleCancel() {
      this.$emit("show", false);
      this.popup.cancel();
    },
  },
};
</script>

<style lang="css">
@import url("../../css/common/Popup.css");
</style>
