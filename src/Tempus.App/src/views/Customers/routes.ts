import CustomersVue from "./Customers.vue";
import CustomerEditorVue from "./CustomerEditor.vue";
import CustomerVue from "./Customer.vue";

export const customersRoutes = {
  name: "customers",
  path: "/customers",
  component: CustomersVue,
};
export const customerRoutes = {
  name: "customer",
  path: "/customers/:id",
  component: CustomerVue,
  children: [
    {
      name: "customerEditor",
      path: "editor",
      component: CustomerEditorVue,
      props: true,
    },
  ],
};
