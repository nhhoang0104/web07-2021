import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "@/views/employee/EmployeeList";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      name: "Home",
      component: EmployeeList,
    },
    {
      path: "/employees",
      name: "EmployeeList",
      component: EmployeeList,
    },
  ],
  linkActiveClass: "menu__item--active",
});

export default router;
