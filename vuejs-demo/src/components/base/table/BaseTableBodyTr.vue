<template>
  <tr :id="data.id">
    <td>
      <div class="container">
        <input type="checkbox" :checked="data.checked ? true : false" />
        <span class="checkmark"></span>
      </div>
    </td>
    <td v-for="col in columns" :key="col.id" :class="col.className">
      {{ formatText(col, data[col.id]) }}
    </td>
  </tr>
</template>

<script>
import Common from "@/utils/Common.js";

export default {
  name: "base-tr",
  props: {
    columns: {
      type: Array,
      required: true,
    },
    data: {
      type: Object,
      required: true,
    },
  },
  methods: {
    formatText(col, item) {
      let tmp = item;

      if (col.format === "date") {
        tmp = Common.formatDate(tmp);
      }

      if (col.format === "money") {
        tmp = Common.formatMoney(tmp);
      }

      return tmp;
    },
  },
};
</script>

<style lang="css"></style>
