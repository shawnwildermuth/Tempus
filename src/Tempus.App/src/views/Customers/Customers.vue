<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import { useCustomerStore } from "../../store";
import { toMoney } from "../../filters";
import router from "../../router";

export default defineComponent({
  setup() {
    const store = useCustomerStore();
    const showConfirm = ref(false);
    let toBeDeleted = -1;

    onMounted(async () => {
      await store.loadCustomers();
    });

    function showDetails(id: number) {
      router.push({ name: "customerDetails", params: { id } });
    }

    async function onClosed(success: boolean) {
      // Only used by delete
      if (success && toBeDeleted > 0) {
        if (await store.deleteCustomer(toBeDeleted)) {
          toBeDeleted = -1;
        }
      }
      showConfirm.value = false;
    }

    function onDelete(id: number) {
      showConfirm.value = true;
      toBeDeleted = id;
    }

    return {
      customers: store.customers,
      toMoney,
      showDetails,
      onClosed,
      onDelete,
      showConfirm,
    };
  },
});
</script>

<template>
  <h1>Customers</h1>
  <confirm-dialog @closed="onClosed" :show="showConfirm">
    <p class="my-4">Are you sure you want to delete the customer?</p>
  </confirm-dialog>
  <div class="flex flex-col lg:flex-row">
    <div class="w-full lg:w-2/3 pr-4">
      <table class="table-auto border-slate-300">
        <thead class="bg-sky-800 text-white">
          <th>Name</th>
          <th class="hidden lg:table-cell">Rate</th>
          <td></td>
        </thead>
        <tr
          v-for="c in customers"
          :key="c.id"
          class="hover:bg-sky-100 p-1 cursor-pointer"
        >
          <td @click="showDetails(c.id)">{{ c.companyName }}</td>
          <td class="hidden lg:table-cell" @click="showDetails(c.id)">{{ c.companyPhone }}</td>
          <td class="text-right">
            <router-link
              class="button text-xs mx-1"
              :to="{ name: 'workerEditor', params: { id: c.id } }"
              >Edit</router-link
            >
            <button class="button text-xs mx-1" @click="onDelete(c.id)">
              Delete
            </button>
          </td>
        </tr>
      </table>
      <router-link
        :to="{ name: 'workerEditor', params: { id: 'new' } }"
        class="button"
        >Add New</router-link
      >
    </div>
    <div class="md:w-1/3">
      <router-view></router-view>
    </div>
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
