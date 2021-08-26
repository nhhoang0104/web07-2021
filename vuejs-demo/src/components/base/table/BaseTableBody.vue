<template>
  <tbody>
    <tr-cus
      v-for="(item, index) in data"
      :key="item.EmployeeId"
      :columns="columns"
      :data="item"
      :index="indexStarted + index"
      :checked="
        employeeDeleteList.indexOf(item.EmployeeId) === -1 ? false : true
      "
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

    employeeDeleteList: {
      type: Array,
      required: true,
    },

    indexStarted: {
      type: Number,
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
