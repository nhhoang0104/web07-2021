<template>
  <tbody>
    <tr-cus
      v-for="item in data"
      :key="item.id"
      :columns="columns"
      :data="item"
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
};
</script>

<style lang="css"></style>
