<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "validationError",
  props: ["result"],
  setup() {
    return {};
  },
});
</script>

<template>
  <div v-for="error of result.$errors" :key="error.$uid">
    <div class="text-red-700 italic text-sm">{{ error.$message }}</div>
  </div>
</template>
