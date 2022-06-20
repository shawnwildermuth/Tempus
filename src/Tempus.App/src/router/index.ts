import Home from "../pages/Home.vue";
import Workers from "../pages/Workers.vue";
import WorkerEditor from "../pages/WorkerEditor.vue";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
  { path: "/", component: Home },
  {
    path: "/workers",
    component: Workers,
    children: [
      {
        path: "editor",
        component: WorkerEditor
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
