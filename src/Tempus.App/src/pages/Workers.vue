<script lang="ts">
import { defineComponent, onMounted } from "vue";
import { workers, store } from "../store";

export default defineComponent({
  setup() {
    onMounted(async () => {
      await workers.loadWorkers();
    });

    return {
      workers
    }
  }
})

</script>

<template>
  <h1>Workers</h1>

</template>

