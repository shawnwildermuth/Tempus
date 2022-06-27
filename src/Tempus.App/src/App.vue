<script lang="ts">
import { defineComponent, watch, watchEffect } from "vue";
import { useRootStore } from "./store";
import router from "./router";

export default defineComponent({
  setup() {
    const store = useRootStore();

    function is(url: string) {
      const current = router.currentRoute.value.path;
      let till = current.indexOf("/", 1);
      if (till === -1) till = current.length;
      const start = current.slice(0, till);
      console.log(`${start} - ${till} - ${current} - ${(url === start)}`);
      return (url === start);
    }

    return {
      store,
      is
    };
  },
});
</script>

<template>
  <div class="container mx-auto bg-slate-100">
    <header class="flex justify-between text-orange-300 p-1 bg-slate-800">
      <router-link to="/" class="self-center text-yellow-500 hover:no-underline"
        ><div class="text-2xl font-bold">
          <fa icon="fa-solid fa-stopwatch"></fa> Tempus
        </div>
      </router-link>
      <div class="flex justify-end p-2">
        <router-link to="/" class="menu" :class="{active: is('/')}">Home</router-link>
        <router-link to="/customers" class="menu" :class="{active: is('/customers')}">Customers</router-link>
        <router-link to="/workers" class="menu" :class="{active: is('/workers')}">Workers</router-link>
        <router-link to="/worktypes" class="menu" :class="{active: is('/worktypes')}">Work Types</router-link>
      </div>
    </header>
    <section class="min-h-screen">
      <div
        v-show="store.isBusy"
        class="bg-gray-800/50 absolute left-0 top-0 w-full h-full z-100 flex justify-center items-center"
      >
        <div class="text-3xl">
          <fa icon="fa-solid fa-spinner" spin></fa> Loading...
        </div>
      </div>
      <div class="p-2">
        <router-view></router-view>
      </div>
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
