<script lang="ts">
import { defineComponent, ref, onMounted, reactive } from "vue";
import { ContactEntity, CustomerEntity } from "../../models";
import { useCustomerStore } from "../../store";
import router from "../../router";
import useValidate from "@vuelidate/core";
import { email, minLength, numeric, required } from "@vuelidate/validators";
import ValidationError from "../../components/validation-error.vue";
import { useToast } from "vue-toastification";
import { phone } from "../../validators";

export default defineComponent({
  components: {
    ValidationError,
  },
  props: ["id"],
  setup(props) {
    const customer = ref({
      id: 0,
      companyName: "",
      companyPhone: "",
      location: {
        id: 0,
        lineOne: "",
        lineTwo: null,
        lineThree: null,
        city: "",
        stateProvince: "",
        postalCode: "",
        country: null
      },
      contacts: new Array<ContactEntity>(),
    } as CustomerEntity);

    const location = ref(customer.value.location);

    const store = useCustomerStore();

    const rules = {
      id: {},
      companyName: { required, minLength: minLength(5) },
      companyPhone: { required, phone },
    };

    const locationRules = {
      id: {},
      lineOne: { required },
      lineTwo: {},
      lineThree: {},
      city: { required },
      stateProvince: { required },
      postalCode: { required },
      country: {},
    };

    var v = useValidate(rules, customer);
    var lv = useValidate(locationRules, customer.value.location);

    onMounted(async () => {
      const id = Number(props.id);
      if (isNaN(id)) {
        if (props.id !== "new") {
          const toast = useToast();
          toast.error("Bad Customer");
          router.push({ name: "customers" });
        }
      } else {
        const foundWorker = await store.findCustomer(id);
        if (foundWorker) {
         customer.value = foundWorker;
         location.value = foundWorker.location;
        }
      }
    });

    async function save() {
      const valid = await v.value.$validate();
      const locationValid = await lv.value.$validate();
      if (valid && locationValid) {
        const forSave = customer.value;
        forSave.location = location.value;
        if (await store.saveCustomer(forSave)) {
          router.push({ name: "customers" });
        }
      }
    }

    return {
      customer,
      location,
      save,
      v,
      lv
    };
  },
});
</script>

<template>
  <div class="border bg-slate-50 rounded p-1">
    <form novalidate @submit.prevent="save" class="editor">
      <div class="flex flex-col">
        <label for="companyName">Company Name</label>
        <input
          id="companyName"
          v-model="customer.companyName"
          placeholder="Acme Goods, LLC"
          v-valid="v.companyName"
        />
        <validation-error :result="v.companyName"></validation-error>
        <label for="companyPhone">Company Phone</label>
        <input
          id="companyPhone"
          v-model="customer.companyPhone"
          placeholder="(404) 555-1212"
          v-valid="v.companyPhone"
        />
        <validation-error :result="v.companyPhone"></validation-error>
        <label for="lineOne">Address</label>
        <input
          id="lineOne"
          v-model="location.lineOne"
          placeholder="123 Main Street"
          v-valid="lv.lineOne"
        />
        <validation-error :result="lv.lineOne"></validation-error>
        <input
          id="lineTwo"
          v-model="location.lineTwo"
          placeholder="Suite 500"
          v-valid="lv.lineTwo"
        />
        <validation-error :result="lv.lineTwo"></validation-error>
        <input
          id="lineThree"
          v-model="location.lineThree"
          placeholder="Attn: Bill"
          v-valid="lv.lineThree"
        />
        <validation-error :result="lv.lineThree"></validation-error>
        <label for="city">City</label>
        <input
          id="city"
          type="text"
          v-model="location.city"
          placeholder="Atlanta"
          v-valid="lv.city"
        />
        <validation-error :result="lv.city"></validation-error>
        <label for="stateProvince">State/Province</label>
        <input
          id="stateProvince"
          type="text"
          v-model="location.stateProvince"
          placeholder="GA"
          v-valid="lv.stateProvince"
        />
        <validation-error :result="lv.stateProvince"></validation-error>
        <label for="postalCode">Postal Code</label>
        <input
          id="postalCode"
          type="text"
          v-model="location.postalCode"
          placeholder="30303"
          v-valid="lv.postalCode"
        />
        <validation-error :result="lv.postalCode"></validation-error>
        <label for="country">Country</label>
        <input
          id="country"
          type="text"
          v-model="location.country"
          placeholder="USA"
          v-valid="lv.country"
        />
        <validation-error :result="lv.country"></validation-error>
        <div>
          <input
            type="submit"
            class="button bg-green-500 hover:bg-green-700 mr-2"
            value="Save"
          />
          <router-link
            :to="{ name: 'customers' }"
            class="button bg-gray-500 hover:bg-gray-700"
            ><fa icon="fa-solid fa-xmark"></fa> Cancel</router-link
          >
        </div>
      </div>
    </form>
  </div>
</template>
