<script lang="ts">
import { defineComponent, onMounted } from "vue";
import { useWorkersStore } from "../store";

export default defineComponent({
  setup() {

    const store = useWorkersStore();

    onMounted(async () => {
      await store.loadWorkers();
    });

    return {
      workers: store.workers
    }
  }
})

</script>

<template>
  <h1>Workers</h1>
  <div>#/workers: {{ workers.length }}</div>
  <div v-for="w in workers" :key="w.id">
    <div>{{ w.lastName }}, {{ w.firstName}}</div>
  </div>
</template>

