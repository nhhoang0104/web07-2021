import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "@/views/employee/EmployeeList";
import CustomerList from "@/views/customer/CustomerList";

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
    {
      path: "/customers",
      name: "CustomerList",
      component: CustomerList,
    },
  ],
  linkActiveClass: "menu__item--active",
});

export default router;
