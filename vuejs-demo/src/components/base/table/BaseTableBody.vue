<template>
  <tbody>
    <tr-cus
      v-for="item in data"
      :key="item.EmployeeId"
      :columns="columns"
      :data="item"
      @click="checkBox(item.EmployeeId)"
      @dblclick="doubleClick(item.EmployeeId)"
    ></tr-cus>
  </tbody>
</template>

<script>
import BaseTableBodyTr from "./BaseTableBodyTr";

export default {
  name: "base-table-body",
  components: {
    "tr-cus": BaseTableBodyTr,
  },
  emits: ["check-box", "dblclick"],
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
    /*
      check box row
    */
    checkBox(id) {
      this.$emit("check-box", id);
    },
    /*
      xử lý double click vào row để xem thông tin chi tiết
    */
    doubleClick(id) {
      this.$emit("dblclick", id);
    },
  },
};
</script>

<style lang="css"></style>
