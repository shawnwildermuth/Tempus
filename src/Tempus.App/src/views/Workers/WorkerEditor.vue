useWorkerStore<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import { WorkerEntity } from "../../models";
import { useWorkerStore } from "../../store";
import router from "../../router";
import useValidate from "@vuelidate/core";
import { email, minLength, numeric, required } from "@vuelidate/validators";
import ValidationError from "../../components/validation-error.vue";
import { useToast } from "vue-toastification";

export default defineComponent({
  components: {
    ValidationError,
  },
  props: ["id"],
  setup(props) {
    const worker = ref({
      id: 0,
      userName: "",
      firstName: "",
      lastName: "",
      baseRate: 300.0,
      email: "",
      phone: "",
    } as WorkerEntity);

    const store = useWorkerStore();

    const rules = {
      userName: { required, minLength: minLength(5) },
      firstName: { required, minLength: minLength(5) },
      lastName: { required, minLength: minLength(5) },
      phone: { required },
      email: { required, email },
      baseRate: { required, numeric },
    };

    var v = useValidate(rules, worker);

    onMounted(async () => {
      const id = Number(props.id);
      if (isNaN(id)) {
        if (props.id !== "new") {
          
          rootStore.showError("Bad ID for Worker");
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
        if (await store.saveWorker(worker.value)) {
          router.push({ name: "workers" });
        }
      }
    }

    return {
      worker,
      save,
      v,
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
          v-valid="v.userName"
        />
        <validation-error :result="v.userName"></validation-error>
        <label for="firstName">First Name</label>
        <input
          id="firstName"
          v-model="worker.firstName"
          placeholder="e.g. Bob"
          v-valid="v.firstName"
        />
        <validation-error :result="v.firstName"></validation-error>
        <label for="lastName">Last Name</label>
        <input
          id="lastName"
          v-model="worker.lastName"
          placeholder="e.g. Smith"
          v-valid="v.lastName"
        />
        <validation-error :result="v.lastName"></validation-error>
        <label for="email">Email</label>
        <input
          id="email"
          type="email"
          v-model="worker.email"
          placeholder="e.g. bob.smith@aol.com"
          v-valid="v.email"
        />
        <validation-error :result="v.email"></validation-error>
        <label for="baseRate">Base Rate ($/hr)</label>
        <input
          id="baseRate"
          v-model="worker.baseRate"
          placeholder="e.g. 100.00"
          v-valid="v.baseRate"
        />
        <validation-error :result="v.baseRate"></validation-error>
        <label for="phone">Phone</label>
        <input
          id="phone"
          type="phone"
          v-model="worker.phone"
          placeholder="e.g. (404) 555-1212"
          v-valid="v.phone"
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
            ><fa icon="fa-solid fa-xmark"></fa> Cancel</router-link
          >
        </div>
      </div>
    </form>
  </div>
</template>
