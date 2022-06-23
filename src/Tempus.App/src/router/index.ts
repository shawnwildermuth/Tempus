import { createRouter, createWebHistory } from "vue-router";

import HomeVue from "../views/Home.vue";

import workerRoutes from "./workers";
import workTypeRoutes from "./workTypes";

const routes = [
  {
    name: "home",
    path: "/",
    component: HomeVue,
  },
  workerRoutes,
  workTypeRoutes
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
