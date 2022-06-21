import { WorkersResults, WorkerEntity } from './../models';
import { useRootStore } from ".";
import http from "../services/http";
import { defineStore } from "pinia";

let loaded = false;

export default defineStore("workers", {
  state: () => ({
    workers: []  as Array<WorkerEntity>
  }),
  getters: {
    isLoaded: (state) => loaded
  },
  actions: {
    async findWorker(id: Number) : Promise<WorkerEntity | undefined> {
      if (await this.loadWorkers()) {
        return this.workers.find(w => w.id === id);
      } else return undefined;
    },
    async loadWorkers() : Promise<Boolean> {
      
      // Don't reload
      if (loaded) return true;

      // Load the data
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const results = await http.get<WorkersResults>("workers");
        if (results.status == 200) {
          this.workers = results.data.results;
          loaded = true;
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
