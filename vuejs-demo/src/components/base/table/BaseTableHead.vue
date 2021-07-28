<template>
  <thead>
    <tr>
      <td v-for="item in column" :key="item.id" :class="item.className">
        {{ item.label }}
      </td>
    </tr>
  </thead>
</template>

<script>
export default {
  name: "base-table-head",
  props: {
    column: {
      type: Array,
      required: true,
    },
  },
};
</script>

<style lang="css"></style>
