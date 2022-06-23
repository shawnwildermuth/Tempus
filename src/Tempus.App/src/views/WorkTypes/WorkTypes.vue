<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import { useWorkTypeStore } from "../../store";
import { toMoney } from "../../filters";
import router from "../../router";

export default defineComponent({
  setup() {
    const store = useWorkTypeStore();
    const showConfirm = ref(false);
    let toBeDeleted = -1;

    onMounted(async () => {
      await store.loadWorkTypes();
    });

    function showDetails(id: number) {
      router.push({ name: "workTypeDetails", params: { id } });
    }

    async function onClosed(success: boolean) {
      // Only used by delete
      if (success && toBeDeleted > 0) {
        if (await store.deleteWorkType(toBeDeleted)) {
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
      workTypes: store.workTypes,
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
  <h1>Work Types</h1>
  <confirm-dialog @closed="onClosed" :show="showConfirm">
    <p class="my-4">Are you sure you want to delete the Work Type?</p>
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
          v-for="w in workTypes"
          :key="w.id"
          class="hover:bg-sky-100 p-1 cursor-pointer"
        >
          <td @click="showDetails(w.id)">{{ w.name }}</td>
          <td class="hidden lg:table-cell" @click="showDetails(w.id)">{{ toMoney(w.defaultRate) }}</td>
          <td class="text-right">
            <router-link
              class="button text-xs mx-1"
              :to="{ name: 'workTypeEditor', params: { id: w.id } }"
              >Edit</router-link
            >
            <button class="button text-xs mx-1" @click="onDelete(w.id)">
              Delete
            </button>
          </td>
        </tr>
      </table>
      <router-link
        :to="{ name: 'workTypeEditor', params: { id: 'new' } }"
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
