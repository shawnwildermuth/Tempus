<script lang="ts">
import { defineComponent, onMounted } from "vue";
import { useWorkersStore } from "../../store";

export default defineComponent({
  setup() {
    const store = useWorkersStore();

    onMounted(async () => {
      await store.loadWorkers();
    });

    return {
      workers: store.workers,
    };
  },
});
</script>

<template>
  <h1>Workers</h1>
  <router-link :to="{ name: 'workerEditor', params: { id: 'new' } }" class="button">Add New</router-link>
  <div>#/workers: {{ workers.length }}</div>
  <div class="flex">
    <div class="w-2/3">
      <h3>Contact List</h3>
      <div v-for="w in workers" :key="w.id">
        <div>{{ w.lastName }}, {{ w.firstName }}</div>
      </div>
    </div>
    <div class="w-1/3">
      <router-view></router-view>
    </div>
  </div>
</template>
