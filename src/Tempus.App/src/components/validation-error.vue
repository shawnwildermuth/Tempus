<script lang="ts">
import { defineComponent, ref, watch, watchEffect } from "vue";

export default defineComponent({
  name: "validationError",
  props: ["result"],
  setup(props) {
    const errorMessage = ref("");

    watchEffect(() => {
      errorMessage.value = 
        props.result.$errors?.length > 0 ? 
        props.result.$errors[0]?.$message : 
        "";
    });

    return {
      errorMessage
    };
  },
});
</script>

<template>
  <div class="text-red-700 italic text-sm">{{ errorMessage }}</div>
</template>
