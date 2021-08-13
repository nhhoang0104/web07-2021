<template>
  <div
    class="dropdown__option"
    :value="value"
    :checked="checked"
    @click="selectItem(value)"
    @keydown="keydown"
  >
    <div class="dropdown__option__check">
      <i class="fas fa-check icon icon--13"></i>
    </div>
    <div class="dropdown__option__label">{{ label }}</div>
  </div>
</template>

<script>
export default {
  name: "dropdown-option",
  emits: ["select-item"],
  inject: ["pkey"],
  props: {
    label: {
      type: String,
      requied: true,
    },
    value: {
      type: String,
      requied: true,
    },
    checked: {
      type: Boolean,
      requied: true,
    },
  },

  methods: {
    /*
      Xử lý chọn option trong dropdown
    */

    selectItem(value) {
      this.$emit("select-item", { key: this.pkey, id: value });
    },

    keydown(event) {
      if (event.code === "ArrowUp") {
        event.preventDefault();
        console.log(1);
      }

      if (event.code === "ArrowDown") {
        event.preventDefault();
        console.log(1);
      }
    },
  },
};
</script>
