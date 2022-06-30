<script lang="ts">
import { defineComponent, ref, onMounted, reactive, onUpdated } from "vue";
import { ContactEntity, CustomerEntity } from "../../models";
import { useCustomerStore, useRootStore } from "../../store";
import router from "../../router";
import useValidate from "@vuelidate/core";
import { email, minLength, numeric, required } from "@vuelidate/validators";
import ValidationError from "../../components/validation-error.vue";
import { phone } from "../../validators";
import { clone } from "@/utils";

export default defineComponent({
  components: {
    ValidationError,
  },
  props: ["id", "contactId"],
  setup(props, { emit }) {
    const contact = ref({
      id: 0,
      firstName: "",
      middleName: "",
      lastName: "",
      phone: "",
      email: "",
    } as ContactEntity);

    const customer = ref(null as CustomerEntity | null);
    const rootStore = useRootStore();

    const store = useCustomerStore();

    const rules = {
      id: {},
      firstName: { required },
      lastName: { required },
      middleName: {},
      phone: { phone },
      email: { email },
    };

    var v = useValidate(rules, contact);

    onMounted(async () => {
      const id = Number(props.id);
      const contactId = Number(props.contactId);
      if (!isNaN(id)) {
        const foundCustomer = await store.findCustomer(id);
        if (foundCustomer) {
          customer.value = foundCustomer;
          if (isNaN(contactId)) {
            if (props.contactId === "new") return;
          } else {
            const foundContact = foundCustomer.contacts.find(
              (f) => f.id == contactId
            );
            if (foundContact) {
              contact.value = clone(foundContact);
              return;
            }
          }
        }
      } else {
        rootStore.showError("Bad Customer Number");
        return;
      }
      rootStore.showError("Bad Contact ID");
      router.push({ name: "customer", params: { id: props.id } });
    });

    async function save() {
      const valid = await v.value.$validate();
      if (customer.value && valid) {
        console.log("validation succeeded");
        const forSave = customer.value;
        if (await store.saveContact(forSave, contact.value)) {
          router.push({ name: "customer", params: { id: props.id } });
        }
      }
    }

    return {
      contact,
      save,
      v,
    };
  },
});
</script>

<template>
  <h1>Contact</h1>
  <div class="border bg-slate-50 rounded p-1">
    <form novalidate @submit.prevent="save()" class="editor">
      <div class="flex flex-col">
        <label for="firstName">First Name</label>
        <input
          id="firstName"
          v-model="contact.firstName"
          placeholder="James"
          v-valid="v.firstName"
        />
        <validation-error :result="v.firstName"></validation-error>
        <label for="middleName">Middle Name</label>
        <input
          id="middleName"
          v-model="contact.middleName"
          placeholder="T."
          v-valid="v.middleName"
        />
        <validation-error :result="v.middleName"></validation-error>
        <label for="lastName">Last Name Name</label>
        <input
          id="lastName"
          v-model="contact.lastName"
          placeholder="Kirk"
          v-valid="v.lastName"
        />
        <validation-error :result="v.lastName"></validation-error>
        <label for="phone">Phone</label>
        <input
          type="phone"
          id="phone"
          v-model="contact.phone"
          placeholder="(404) 555-1212"
          v-valid="v.phone"
        />
        <validation-error :result="v.phone"></validation-error>
        <label for="email">Email</label>
        <input
          id="email"
          v-model="contact.email"
          placeholder="foo@aol.com"
          v-valid="v.email"
        />
        <validation-error :result="v.email"></validation-error>
        <div>
          <button
            type="submit"
            class="button bg-green-500 hover:bg-green-700 mr-2"
          >
            <fa icon="fa-solid fa-floppy-disk" /> Save
          </button>
          <router-link
            :to="{ name: 'customer', params: { id: id } }"
            class="button bg-gray-500 hover:bg-gray-700"
            ><fa icon="fa-solid fa-xmark"></fa> Cancel</router-link
          >
        </div>
      </div>
    </form>
  </div>
</template>

<style scoped>
table thead th {
  @apply p-2 text-left;
}

table tr td {
  @apply px-2 border border-sky-500;
}
</style>
