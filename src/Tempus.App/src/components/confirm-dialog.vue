<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "confirm-dialog",
  props: {
    show: {
      type: Boolean,
      required: true,
    },
  },
  setup(props, { emit }) {
    function close(result: boolean) {
      emit("closed", result);
    }

    return {
      close,
    };
  },
});
</script>

<template>
  <Modal v-model="show" :close="close">
    <div class="modal">
      <div class="border rounded bg-white shadow-xl p-2">
        <slot> </slot>
        <div class="flex justify-center">
          <button class="button button-success mr-2 w-20" @click="close(true)">
            Yes
          </button>
          <button class="button w-20" @click="close(false)">No</button>
        </div>
      </div>
    </div>
  </Modal>
</template>
