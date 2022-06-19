import theWorkerStore from "./workerStore";
import { defineStore } from "pinia";

export const useRootStore = defineStore("root", {
  state: () => ({
    isBusy: false,
    error: ""
  }),
  actions: {
    setBusy() {
      this.isBusy = true;
    },
    setError(msg: string) {
      this.error = msg;
      console.error(msg);
    },
    clearError() {
      this.error = "";
    },
    clearBusy() {
      this.isBusy = false;
    }
  }
});

export const useWorkersStore = theWorkerStore;
