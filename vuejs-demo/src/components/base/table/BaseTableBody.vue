<template>
  <tbody>
    <tr-cus
      v-for="item in data"
      :key="item.EmployeeId"
      :columns="columns"
      :data="item"
      @click="checkBox(item.EmployeeId)"
    ></tr-cus>
  </tbody>
</template>

<script>
import BaseTableBodyTr from "./BaseTableBodyTr";

export default {
  name: "base-table-tbody",
  components: {
    "tr-cus": BaseTableBodyTr,
  },
  emits: ["check-box"],
  props: {
    data: {
      type: Array,
      required: true,
    },
    columns: {
      type: Array,
      required: true,
    },
  },
  methods: {
    checkBox(id) {
      this.$emit("check-box", id);
    },
  },
};
</script>

<style lang="css"></style>
