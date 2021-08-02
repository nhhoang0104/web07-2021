<template>
  <div class="page">
    <EmployeeList />
  </div>
</template>

<script>
import EmployeeList from "../views/employee/EmployeeList.vue";

export default {
  components: { EmployeeList },
};
</script>

<style lang="css">
@import url("../css/layout/Page.css");
</style>
