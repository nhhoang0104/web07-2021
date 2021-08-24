<template>
  <thead>
    <tr>
      <th @click="$emit('select-all', checked)">
        <div class="container">
          <input type="checkbox" :checked="checked" />
          <span class="checkmark"></span>
        </div>
      </th>
      <th class="text text--center">#</th>
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
    checked: {
      type: Boolean,
      required: true,
    },
  },

  emits: ["select-all"],
};
</script>

<style lang="css"></style>
