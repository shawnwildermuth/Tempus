<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  setup ()
  {
    
  }
});
</script>

<template>
  <div class="container mx-auto bg-slate-100 p-2">
    <header class="flex justify-between">
      <div class="text-lg font-bold">Tempus</div>
      <div class="flex justify-end">
        <router-link to="/" class="bg-gray-500 hover:bg-gray-800 text-white drop-shadow-md m-2 p-1">Home</router-link>
        <router-link to="/workers" class="bg-gray-500 hover:bg-gray-800 text-white drop-shadow-md m-2 p-1">Workers</router-link>
      </div>
    </header>
    <section class="min-h-screen">
      <router-view></router-view>
    </section>
  </div>
</template>
