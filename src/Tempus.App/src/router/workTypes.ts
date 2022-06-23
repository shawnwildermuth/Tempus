import WorkTypeVue from "../views/WorkTypes/WorkTypes.vue";
import WorkTypeEditorVue from "../views/WorkTypes/WorkTypeEditor.vue";
import WorkTypeDetailsVue from "../views/WorkTypes/WorkTypeDetails.vue";


export default {
  name: "worktypes",
  path: "/worktypes",
  component: WorkTypeVue,
  children: [
    {
      name: "workTypeEditor",
      path: "editor/:id",
      component: WorkTypeEditorVue,
      props: true 
    },
    {
      name: "workTypeDetails",
      path: "details/:id",
      component: WorkTypeDetailsVue,
      props: true 
    },
  ],
};