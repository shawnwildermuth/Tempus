import { reactive, ref } from "vue";
import WorkerStore from "./WorkerStore";

export class Store {

  constructor() {
    this.workers = new WorkerStore(this);
  }

  workers: WorkerStore;
  isBusy = true;
  error = "";

  setBusy() {
    this.isBusy = true;
  }
  setError(msg: string) {
    this.error = msg;
    console.error(msg);
  }
  clearError(msg: string) {
    this.error = "";
  }
  clearBusy() {
    this.isBusy = false;
  }
}

const theStore = reactive(new Store());

export const store = theStore;
export const workers = theStore.workers;