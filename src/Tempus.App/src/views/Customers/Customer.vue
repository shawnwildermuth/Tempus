<script lang="ts">
import { defineComponent, onMounted, ref, watch } from "vue";
import { CustomerEntity } from "../../models";
import { useCustomerStore, useRootStore } from "../../store";
import router from "../../router";
import { toAddressBlock, toMoney } from "../../filters";

export default defineComponent({
  props: ["id"],
  setup(props) {
    const customer = ref({} as CustomerEntity);
    const store = useCustomerStore();
    const rootStore = useRootStore();

    // watch for the property change
    watch(
      () => props.id,
      async () => {
        await loadCustomer();
      }
    );

    onMounted(async () => await loadCustomer());

    async function loadCustomer() {
      const idNumber = Number(props.id);
      if (idNumber && !isNaN(idNumber)) {
        const found = await store.findCustomer(idNumber);
        if (found) customer.value = found;
        return;
      } else {
        rootStore.showError("Bad id for worker");
        router.back();
      }
    }

    return {
      customer,
      toMoney,
      toAddressBlock,
    };
  },
});
</script>

<template>
  <div class="flex flex-col md:flex-row">
    <div class="flex flex-col">
      <div class="border rounded bg-slate-200 w-full">
        <dl>
          <dt>Name:</dt>
          <dd>{{ customer.companyName }}</dd>
        </dl>
        <dl>
          <dt>Phone:</dt>
          <dd>
            <a :href="`tel:${customer.companyPhone}`">{{
              customer.companyPhone
            }}</a>
          </dd>
        </dl>
        <dl>
          <dt>Address:</dt>
          <dd v-html="toAddressBlock(customer.location)"></dd>
        </dl>
        <router-link :to="{ name: 'customers' }" class="button text-sm"
          >Close</router-link
        >
      </div>
      <div>
        <h3>Contacts</h3>
        <table class="table-auto border-slate-300">
          <thead class="bg-sky-800 text-white text-sm p-0">
            <th class="">Name</th>
            <th class="hidden md:table-cell">Phone</th>
            <th class="hidden md:table-cell">Email</th>
            <td></td>
          </thead>
          <tr
            v-for="c in customer.contacts"
            :key="c.id"
            class="hover:bg-sky-100 p-1 cursor-pointer"
          >
            <td class="px-2">{{ c.firstName }} {{ c.lastName }}</td>
            <td class="hidden px-2 md:table-cell">
              <a :href="`tel:${c.phone}`">{{ c.phone }}</a>
            </td>
            <td class="hidden px-2 md:table-cell">
              <a :href="`mailto:${c.email}?subject=Tempus`">{{ c.email }}</a>
            </td>
            <td class="text-right">
              <router-link
                class="button text-xs mx-1 button-success"
                :to="{
                  name: 'contactEditor',
                  params: { id: id, contactId: c.id },
                }"
                ><fa icon="fa-solid fa-eye" /> Edit</router-link
              >
            </td>
          </tr>
        </table>
        <router-link
          :to="{ name: 'contactEditor', params: { contactId: 'new' } }"
          class="button text-sm"
          ><fa icon="fa-solid fa-circle-plus " /> Add New</router-link
        >
      </div>
    </div>
    <div>
      <router-view></router-view>
    </div>
  </div>
</template>

<style scoped>
dl dt {
  @apply font-bold;
}

dl dd {
  @apply italic ml-4;
}
</style>
