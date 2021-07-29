<template>
  <thead>
    <tr>
      <th v-for="col in columns" :key="col.id" :class="col.className">
        {{ col.label }}
      </th>
    </tr>
  </thead>
</template>

<script>
export default {
  name: "base-table-head",
  props: {
    columns: {
      type: Array,
      required: true,
    },
  },
};
</script>

<style lang="css"></style>
