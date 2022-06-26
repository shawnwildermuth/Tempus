import { createRouter, createWebHistory } from "vue-router";

import HomeVue from "../views/Home.vue";

import customerRoutes from "../views/Customers/routes";
import workerRoutes from "../views/Workers/routes";
import workTypeRoutes from "../views/WorkTypes/routes";

const routes = [
  {
    name: "home",
    path: "/",
    component: HomeVue,
  },
  workerRoutes,
  workTypeRoutes,
  customerRoutes
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
