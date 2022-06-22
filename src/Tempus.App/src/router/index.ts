import HomeVue from "../views/Home.vue";
import WorkersVue from "../views/Workers/Workers.vue";
import WorkerEditorVue from "../views/Workers/WorkerEditor.vue";
import { createRouter, createWebHistory } from "vue-router";
import WorkerDetailsVue from "../views/Workers/WorkerDetails.vue";

const routes = [
  {
    name: "home",
    path: "/",
    component: HomeVue,
  },
  {
    name: "workers",
    path: "/workers",
    component: WorkersVue,
    children: [
      {
        name: "workerEditor",
        path: "editor/:id",
        component: WorkerEditorVue,
        props: true 
      },
      {
        name: "workerDetails",
        path: "details/:id",
        component: WorkerDetailsVue,
        props: true 
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
