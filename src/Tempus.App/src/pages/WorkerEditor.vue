<script lang="ts">
import { defineComponent, ref } from "vue";
import { WorkerEntity } from "../models";
import { useWorkersStore } from "../store";

export default defineComponent({
  setup() {
    const worker = ref({} as WorkerEntity);
    const store = useWorkersStore();

    return {
      worker,
    };
  },
});
</script>

<template>
  <div>
    <form>
      <div class="flex flex-col">
        <label>First Name</label>
        <input v-model="worker.userName" />
        <label>First Name</label>
        <input v-model="worker.firstName" />
        <label>First Name</label>
        <input v-model="worker.lastName" />
        <label>First Name</label>
        <input v-model="worker.email" />
        <label>First Name</label>
        <input v-model="worker.baseRate" />
        <label>First Name</label>
        <input v-model="worker.phone" />
        <input type="submit" value="Save" />
      </div>
    </form>
  </div>
</template>
