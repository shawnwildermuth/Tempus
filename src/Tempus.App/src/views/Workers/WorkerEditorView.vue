<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import { WorkerEntity } from "../../models";
import { useWorkersStore } from "../../store";
import { useRoute } from "vue-router";
import router from "../../router";
import useValidate from "@vuelidate/core";
import { email, minLength, numeric, required } from "@vuelidate/validators";
import ValidationError from "../../components/validation-error.vue";

export default defineComponent({
  components: {
    ValidationError
  },
  setup() {
    const worker = ref({ 
      id: 0,
      userName: "shawnwildermuth",
      firstName: "Shawn",
      lastName: "Wildermuth",
      baseRate: 300.00,
      email: "shawn@wildermuth.com",
      phone: "404-555-1212"
      } as WorkerEntity);
    const store = useWorkersStore();

    const route = useRoute();

    const rules = {
      userName: { required, minLength: minLength(5) },
      firstName: { required, minLength: minLength(5)  },
      lastName: { required, minLength: minLength(5) },
      phone: { required },
      email: { required, email },
      baseRate: { required, numeric },

    };

    var v = useValidate(rules, worker);

    onMounted(async () => {
      const id = Number(route.params.id);
      if (isNaN(id)) {
        if (route.params.id !== "new") {
          router.push({ name: "workers" });
        }
      } else {
        const foundWorker = await store.findWorker(id);
        if (foundWorker) worker.value = foundWorker;
      }
    });

    async function save() {
      const valid = await v.value.$validate();
      if (valid) {
        await store.saveWorker(worker.value);
        router.push({ name: "workers" });
      }
    }

    return {
      worker,
      save,
      v
    };
  },
});
</script>

<template>
  <div class="border bg-slate-50 rounded p-1">
    <form novalidate @submit.prevent="save" class="editor">
      <div class="flex flex-col">
        <label for="userName">Username</label>
        <input
          id="userName"
          v-model="worker.userName"
          placeholder="e.g. bobsmith"
        />
        <validation-error :result="v.userName"></validation-error>
        <label for="firstName">First Name</label>
        <input
          id="firstName"
          v-model="worker.firstName"
          placeholder="e.g. Bob"
        />
        <validation-error :result="v.firstName"></validation-error>
        <label for="lastName">Last Name</label>
        <input
          id="lastName"
          v-model="worker.lastName"
          placeholder="e.g. Smith"
        />
        <validation-error :result="v.lastName"></validation-error>
        <label for="email">Email</label>
        <input
          id="email"
          type="email"
          v-model="worker.email"
          placeholder="e.g. bob.smith@aol.com"
        />
        <validation-error :result="v.email"></validation-error>
        <label for="baseRate">Base Rate ($/hr)</label>
        <input
          id="baseRate"
          v-model="worker.baseRate"
          placeholder="e.g. 100.00"
        />
        <validation-error :result="v.baseRate"></validation-error>
        <label for="phone">Phone</label>
        <input
          id="phone"
          type="phone"
          v-model="worker.phone"
          placeholder="e.g. (404) 555-1212"
        />
        <validation-error :result="v.phone"></validation-error>
        <div>
          <input
            type="submit"
            class="button bg-green-500 hover:bg-green-700 mr-2"
            value="Save"
          />
          <router-link
            :to="{ name: 'workers' }"
            class="button bg-gray-500 hover:bg-gray-700"
            >Cancel</router-link
          >
        </div>
      </div>
    </form>
    <pre>{{ v }}</pre>
  </div>
</template>
