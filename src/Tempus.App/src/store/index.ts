import theWorkerStore from "./workerStore";
import theWorkTypeStore from "./workTypeStore";
import theCustomerStore from "./customerStore";
import { defineStore } from "pinia";
import { useToast } from "vue-toastification";

const toast = useToast();

export const useRootStore = defineStore("root", {
  state: () => ({
    isBusy: false
  }),
  actions: {
    setBusy() {
      this.isBusy = true;
    },
    showMessage(msg: string) {
      toast(msg);
    },
    showError(msg: string) {
      toast.error(msg);
      console.error(msg);
    },
    clearBusy() {
      this.isBusy = false;
    }
  }
});

export const useWorkerStore = theWorkerStore;
export const useWorkTypeStore = theWorkTypeStore;
export const useCustomerStore = theCustomerStore;
