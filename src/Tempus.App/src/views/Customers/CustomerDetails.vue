<script lang="ts">
import { defineComponent, onMounted, ref, watch } from "vue";
import { CustomerEntity } from "../../models";
import { useCustomerStore } from "../../store";
import { useRoute } from "vue-router";
import router from "../../router";
import { useToast } from "vue-toastification";
import { toAddressBlock, toMoney } from "../../filters";

export default defineComponent({
  props: ["id"],
  setup(props) {
    const customer = ref({} as CustomerEntity);
    const store = useCustomerStore();
    const toast = useToast();

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
      }
      toast.error("Bad id for worker");
      router.back();
    }

    return {
      customer,
      toMoney,
      toAddressBlock
    };
  },
});
</script>

<template>
  <div class="border rounded bg-slate-200">
    <dl>
      <dt>Name:</dt>
      <dd>{{ customer.companyName }}</dd>
    </dl>
    <dl>
      <dt>Phone:</dt>
      <dd>
        <a :href="`tel:${customer.companyPhone}`">{{ customer.companyPhone }}</a>
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
</template>

<style scoped>
dl dt {
  @apply font-bold;
}

dl dd {
  @apply italic ml-4;
}
</style>
