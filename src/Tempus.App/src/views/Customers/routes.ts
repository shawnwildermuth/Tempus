import CustomersVue from "./Customers.vue";
import CustomerEditorVue from "./CustomerEditor.vue";
import CustomerVue from "./Customer.vue";
import ContactEditorVue from "./ContactEditor.vue";

export const customersRoutes = {
  name: "customers",
  path: "/customers",
  component: CustomersVue,
};
export const customerRoutes = {
  name: "customer",
  path: "/customer/:id",
  component: CustomerVue,
  props: true,
  children: [
    {
      name: "customerEditor",
      path: "editor",
      component: CustomerEditorVue,
      props: true,
    },
    {
      name: "contactEditor",
      path: "contact/edit/:contactId",
      component: ContactEditorVue,
      props: true,
    },
  ],
};
