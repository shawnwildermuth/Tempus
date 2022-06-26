import CustomersVue from "./Customers.vue";
import CustomerEditorVue from "./CustomerEditor.vue";
import CustomerDetailsVue from "./CustomerDetails.vue";

export default {
  name: "customers",
  path: "/customers",
  component: CustomersVue,
  children: [
    {
      name: "workerEditor",
      path: "editor/:id",
      component: CustomerEditorVue,
      props: true 
    },
    {
      name: "workerDetails",
      path: "details/:id",
      component: CustomerDetailsVue,
      props: true 
    },
  ],
};