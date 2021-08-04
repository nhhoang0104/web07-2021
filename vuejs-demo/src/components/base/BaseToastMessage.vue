<template>
  <div
    class="container__toast"
    :class="isShowed ? '' : 'container__toast--hidden'"
  >
    <div class="toast" :class="style.color">
      <div class="toast__icon">
        <i class="icon icon--24 icon--circle fas" :class="style.icon"></i>
      </div>
      <div class="toast__message">{{ message }}</div>
      <div class="toast__close" @click="this.$emit('close', false)">
        <i class="icon icon--24 fas fa-times"></i>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-toast-message",
  props: {
    type: {
      type: String,
      required: true,
    },
    message: {
      type: String,
      required: true,
    },
    isShowed: {
      type: Boolean,
      required: true,
    },
  },
  emits: ["close"],
  watch: {
    type: {
      immediate: true,
      handler(newVal) {
        if (newVal === "done") {
          this.style.icon = "fa-check-circle";
          this.style.color = "toast--done";
        }

        if (newVal === "danger") {
          this.style.icon = "fa-exclamation-triangle";
          this.style.color = "toast--danger";
        }
      },
    },
    isShowed(newVal) {
      if (newVal === true) {
        setTimeout(() => {
          this.$emit("close", false);
        }, 6000);
      }
    },
  },
  data() {
    return {
      style: {
        icon: "fa-check-circle",
        color: "toast--done",
      },
    };
  },
};
</script>

<style lang="css">
@import url("../../css/common/Toast.css");
</style>
