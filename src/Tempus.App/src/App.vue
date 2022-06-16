<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  setup() {},
});
</script>

<template>
  <header class="flex justify-between text-orange-300 p-1">
    <div class="text-2xl font-bold"><fa icon="fa-solid fa-stopwatch"></fa> Tempus</div>
    <div class="flex justify-end p-2">
      <router-link
        to="/"
        class="text-white drop-shadow-md p-1 hover:text-orange-200"
        >Home</router-link
      >
      <router-link
        to="/workers"
        class="text-white drop-shadow-md p-1 hover:text-orange-200"
        >Workers</router-link
      >
    </div>
  </header>
  <div class="container mx-auto bg-slate-100 p-2">
    <section class="min-h-screen">
      <router-view></router-view>
    </section>
  </div>
  <footer class="text-center w-full p-2 text-white">
    <div class="text-md font-bold">Tempus Example</div>
    <div class="text-sm font-bold">
      A microservice example by
      <a
        href="https://shawnl.ink/gh"
        class="text-orange-300 hover:text-yellow-200"
        >Shawn Wildermuth</a
      >
    </div>
    <div class="text-sm">
      UIcons by
      <a
        href="https://www.flaticon.com/uicons"
        class="text-orange-300 hover:text-yellow-200"
        >Flaticon</a
      >
    </div>
  </footer>
</template>
