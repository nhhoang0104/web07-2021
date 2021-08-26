<template>
  <div class="container__toast" :style="`top:${24 + index * 49}px`">
    <div class="toast" :class="style.color">
      <div class="toast__icon">
        <i class="icon icon--24 icon--circle fas" :class="style.icon"></i>
      </div>
      <div class="toast__message">{{ message }}</div>
      <div class="toast__close" @click="this.$emit('close')">
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
    index: {
      type: Number,
      required: true,
    },
  },

  emits: ["close"],

  watch: {
    // set css cho loại type toast
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

        if (newVal === "warning") {
          this.style.icon = "fa-exclamation-circle";
          this.style.color = "toast--warning";
        }
      },
    },
  },

  mounted() {
    setTimeout(() => {
      this.$emit("close");
    }, 3000);
  },

  data() {
    return {
      style: {
        icon: "fa-check-circle",
        color: "toast--done",
      },
      distanceToasts: {
        "margin-bottom": `${this.index * 48}px`,
      },
    };
  },
};
</script>

<style lang="css">
@import url("../../css/common/Toast.css");
</style>
