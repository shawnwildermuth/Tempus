import CustomersVue from "./Customers.vue";
import CustomerEditorVue from "./CustomerEditor.vue";
import CustomerDetailsVue from "./CustomerDetails.vue";

export default {
  name: "customers",
  path: "/customers",
  component: CustomersVue,
  children: [
    {
      name: "customerEditor",
      path: "editor/:id",
      component: CustomerEditorVue,
      props: true 
    },
    {
      name: "customerDetails",
      path: "details/:id",
      component: CustomerDetailsVue,
      props: true 
    },
  ],
};