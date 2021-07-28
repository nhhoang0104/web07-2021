<template>
  <tr :id="data.id">
    <td v-for="item in column" :key="item.id" :class="item.className">
      {{ data[item.id] }}
    </td>
  </tr>
</template>

<script>
export default {
  name: "base-tr",
  props: {
    column: {
      type: Array,
      required: true,
    },
    data: {
      type: Object,
      required: true,
    },
  },
};
</script>

<style lang="css"></style>
