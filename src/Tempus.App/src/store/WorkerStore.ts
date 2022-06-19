import { WorkersResults } from './../models/index';
import { useRootStore } from ".";
import { WorkerEntity } from "../models";
import http from "../services/http";
import { defineStore } from "pinia";

export default defineStore("workers", {
  state: () => ({
    workers: [] as Array<WorkerEntity>
  }),
  actions: {
    async loadWorkers() : Promise<Boolean> {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const results = await http.get<WorkersResults>("workers");
        if (results.status == 200) {
          this.workers = results.data.results;
          return true;
        }
      } catch {
      }
      finally {
        rootStore.clearBusy();
      }
      rootStore.setError("Failed to load workers...");
      return false;
    }

  }
});
