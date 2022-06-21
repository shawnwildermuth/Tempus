import HomeView from "../views/HomeView.vue";
import WorkersView from "../views/Workers/WorkersView.vue";
import WorkerEditorView from "../views/Workers/WorkerEditorView.vue";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
  {
    name: "home",
    path: "/",
    component: HomeView,
  },
  {
    name: "workers",
    path: "/workers",
    component: WorkersView,
    children: [
      {
        name: "workerEditor",
        path: "editor/:id",
        component: WorkerEditorView,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
