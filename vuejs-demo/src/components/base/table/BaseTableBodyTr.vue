<template>
  <tr :id="data.id" :checked="checked">
    <td>
      <div class="container">
        <input type="checkbox" :checked="checked" />
        <span class="checkmark"></span>
      </div>
    </td>
    <td class="text text--center">{{ index + 1 }}</td>
    <td v-for="col in columns" :key="col.id" :class="col.className">
      {{ formatText(col, data[col.id]) }}
    </td>
  </tr>
</template>

<script>
import FormatData from "@/utils/FormatData.js";

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

    index: {
      type: Number,
      required: true,
    },

    checked: {
      type: Boolean,
      required: true,
    },
  },

  methods: {
    formatText(col, item) {
      let tmp = item;

      if (col.format === "date") {
        tmp = FormatData.formatDate(tmp);
      }

      if (col.format === "money") {
        tmp = FormatData.formatMoney(tmp);
      }

      return tmp;
    },
  },
};
</script>

<style lang="css"></style>
