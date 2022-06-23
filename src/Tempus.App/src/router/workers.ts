import WorkersVue from "../views/Workers/Workers.vue";
import WorkerEditorVue from "../views/Workers/WorkerEditor.vue";
import WorkerDetailsVue from "../views/Workers/WorkerDetails.vue";

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