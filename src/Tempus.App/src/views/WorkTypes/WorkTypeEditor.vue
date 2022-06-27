<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import { WorkTypeEntity } from "../../models";
import { useWorkTypeStore } from "../../store";
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
    const workType = ref({
      id: 0,
      name: "",
      description: "",
      defaultRate: 300.0,
    } as WorkTypeEntity);

    const store = useWorkTypeStore();

    const rules = {
      name: { required, minLength: minLength(5) },
      description: {},
      defaultRate: { required, numeric },
    };

    var v = useValidate(rules, workType);

    onMounted(async () => {
      const id = Number(props.id);
      if (isNaN(id)) {
        if (props.id !== "new") {
          const toast = useToast();
          toast.error("Bad ID for Work Type");
          router.push({ name: "worktypes" });
        }
      } else {
        const foundWorker = await store.findWorker(id);
        if (foundWorker) workType.value = foundWorker;
      }
    });

    async function save() {
      const valid = await v.value.$validate();
      if (valid) {
        if (await store.saveWorkType(workType.value)) {
          router.push({ name: "worktypes" });
        }
      }
    }

    return {
      workType,
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
        <label for="name">Name</label>
        <input
          id="name"
          v-model="workType.name"
          placeholder="e.g. Consulting"
          v-valid="v.name"
        />
        <validation-error :result="v.name"></validation-error>
        <label for="description">Description</label>
        <textarea
          rows="2"
          id="description"
          v-model="workType.description"
          placeholder="e.g. The reason for this Work Type is..."
          v-valid="v.description"
        ></textarea>
        <validation-error :result="v.description"></validation-error>
        <label for="defaultRate">Default Rate ($/hr)</label>
        <input
          id="defaultRate"
          v-model="workType.defaultRate"
          placeholder="e.g. 100.00"
          v-valid="v.defaultRate"
        />
        <validation-error :result="v.defaultRate"></validation-error>
        <div>
          <input
            type="submit"
            class="button bg-green-500 hover:bg-green-700 mr-2"
            value="Save"
          />
          <router-link
            :to="{ name: 'worktypes' }"
            class="button bg-gray-500 hover:bg-gray-700"
            >Cancel</router-link
          >
        </div>
      </div>
    </form>
  </div>
</template>
