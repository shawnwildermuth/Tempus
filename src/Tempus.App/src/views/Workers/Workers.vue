useWorkerStore<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import { useWorkerStore } from "../../store";
import { toMoney } from "../../filters";
import router from "../../router";

export default defineComponent({
  setup() {
    const store = useWorkerStore();
    const showConfirm = ref(false);
    let toBeDeleted = -1;

    onMounted(async () => {
      await store.loadWorkers();
    });

    function showDetails(id: number) {
      router.push({ name: "workerDetails", params: { id } });
    }

    async function onClosed(success: boolean) {
      // Only used by delete
      if (success && toBeDeleted > 0) {
        if (await store.deleteWorker(toBeDeleted)) {
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
      workers: store.workers,
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
  <h1>Workers</h1>
  <confirm-dialog @closed="onClosed" :show="showConfirm">
    <p class="my-4">Are you sure you want to delete the worker?</p>
  </confirm-dialog>
  <div class="flex flex-col lg:flex-row">
    <div class="w-full lg:w-2/3 pr-4">
      <table class="table-auto border-slate-300">
        <thead class="bg-sky-800 text-white">
          <th class="hidden lg:table-cell">Username</th>
          <th>Name</th>
          <th class="hidden lg:table-cell">Rate</th>
          <td></td>
        </thead>
        <tr
          v-for="w in workers"
          :key="w.id"
          class="hover:bg-sky-100 p-1 cursor-pointer"
        >
          <td class="hidden lg:table-cell" @click="showDetails(w.id)">{{ w.userName }}</td>
          <td @click="showDetails(w.id)">{{ w.lastName }}, {{ w.firstName }}</td>
          <td class="hidden lg:table-cell" @click="showDetails(w.id)">{{ toMoney(w.baseRate) }}</td>
          <td class="text-right">
            <router-link
              class="button text-xs mx-1"
              :to="{ name: 'workerEditor', params: { id: w.id } }"
              ><fa icon="fa-solid fa-pencil"></fa> Edit</router-link
            >
            <button class="button text-xs mx-1" @click="onDelete(w.id)">
              <fa icon="fa-solid fa-xmark"></fa> Delete
            </button>
          </td>
        </tr>
      </table>
      <router-link
        :to="{ name: 'workerEditor', params: { id: 'new' } }"
        class="button"
        ><fa icon="fa-solid fa-circle-plus " />  Add New</router-link
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
