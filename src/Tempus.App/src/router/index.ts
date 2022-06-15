import Home from "../pages/Home.vue";
import Workers from "../pages/Workers.vue";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
  { path: '/', component: Home },
  { path: '/workers', component: Workers },
]

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;