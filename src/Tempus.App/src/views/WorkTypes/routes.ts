import WorkTypeVue from "./WorkTypes.vue";
import WorkTypeEditorVue from "./WorkTypeEditor.vue";
import WorkTypeDetailsVue from "./WorkTypeDetails.vue";


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