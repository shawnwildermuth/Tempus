import WorkersVue from "./Workers.vue";
import WorkerEditorVue from "./WorkerEditor.vue";
import WorkerDetailsVue from "./WorkerDetails.vue";
 
export default {
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
};