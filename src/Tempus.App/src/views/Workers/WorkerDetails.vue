useWorkerStore<script lang="ts">
import { defineComponent, onMounted, ref, watch } from "vue";
import { WorkerEntity } from "../../models";
import { useWorkerStore } from "../../store";
import { useRoute } from "vue-router";
import router from "../../router";
import { useToast } from "vue-toastification";
import { toMoney } from "../../filters";

export default defineComponent({
  props: ["id"],
  setup(props) {
    const worker = ref({} as WorkerEntity);
    const route = useRoute();
    const store = useWorkerStore();
    const toast = useToast();

    // watch for the property change
    watch(
      () => props.id,
      async () => {
        await loadWorker();
      }
    );

    onMounted(async () => await loadWorker());

    async function loadWorker() {
      const idNumber = Number(props.id);
      if (idNumber && !isNaN(idNumber)) {
        const found = await store.findWorker(idNumber);
        if (found) worker.value = found;
        return;
      }
      toast.error("Bad id for worker");
      router.back();
    }

    return {
      worker,
      toMoney,
    };
  },
});
</script>

<template>
  <div class="border rounded bg-slate-200">
    <dl>
      <dt>Name:</dt>
      <dd>{{ worker.lastName }}, {{ worker.firstName }}</dd>
    </dl>
    <dl>
      <dt>Username:</dt>
      <dd>{{ worker.userName }}</dd>
    </dl>
    <dl>
      <dt>Base Rate:</dt>
      <dd>{{ toMoney(worker.baseRate) }}</dd>
    </dl>
    <dl>
      <dt>Email:</dt>
      <dd>
        <a :href="`mailto:${worker.email}`">{{ worker.email }}</a>
      </dd>
    </dl>
    <dl>
      <dt>Phone:</dt>
      <dd>
        <a :href="`tel:${worker.phone}`">{{ worker.phone }}</a>
      </dd>
    </dl>
    <router-link :to="{ name: 'workers' }" class="button text-sm"
      >Close</router-link
    >
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
