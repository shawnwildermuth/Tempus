import theWorkerStore from "./workerStore";
import theWorkTypeStore from "./workTypeStore";
import { defineStore } from "pinia";
import { useToast } from "vue-toastification";

export const useRootStore = defineStore("root", {
  state: () => ({
    isBusy: false
  }),
  actions: {
    setBusy() {
      this.isBusy = true;
    },
    setError(msg: string) {
      var toast = useToast();
      toast.error(msg);
      console.error(msg);
    },
    clearBusy() {
      this.isBusy = false;
    }
  }
});

export const useWorkersStore = theWorkerStore;
export const useWorkTypeStore = theWorkTypeStore;