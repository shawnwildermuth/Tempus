import { reactive } from "vue";
import { Store } from ".";
import { WorkerEntity } from "../models/";
import http from "../services/http";

export default class WorkerStore {

  constructor(private store: Store) {

  }
  workers = reactive(Array<WorkerEntity>());

  async loadWorkers() : Promise<Boolean> {
    try {
      this.store.setBusy();
      const results = await http.get<WorkerEntity[]>("workers");
      if (results.status == 200) {
        this.workers = results.data;
        return true;
      }
    } catch {
    }
    finally {
      this.store.clearBusy();
    }
    this.store.setError("Failed to load workers...");
    return false;
  }
}
