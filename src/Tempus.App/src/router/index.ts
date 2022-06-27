import { createRouter, createWebHistory } from "vue-router";

import HomeVue from "../views/Home.vue";

import { customersRoutes, customerRoutes } from "../views/Customers/routes";
import workerRoutes from "../views/Workers/routes";
import workTypeRoutes from "../views/WorkTypes/routes";

import { useStore } from "../store";

const routes = [
  {
    name: "home",
    path: "/",
    component: HomeVue,
  },
  workerRoutes,
  workTypeRoutes,
  customerRoutes,
  customersRoutes
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
