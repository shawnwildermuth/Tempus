import { WorkersResults, WorkerEntity } from './../models';
import { useRootStore } from ".";
import http from "../services/http";
import { defineStore } from "pinia";
import { Ref } from 'vue';
import { replaceArray } from '../utils';

let loaded = false;

export default defineStore("workers", {
  state: () => ({
    workers: []  as Array<WorkerEntity>
  }),
  getters: {
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
        const results = await http.get<Array<WorkerEntity>>("workers");
        if (results) {
          replaceArray(this.workers, results);
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
    },
    async saveWorker(worker: WorkerEntity) {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        if (worker.id === 0) {
          // Create new
          const result = await http.post<WorkerEntity>("workers", worker);
          if (result) {
            this.workers.push(result);
          } else {
            rootStore.setError("Failed to save worker");
          }
        } else {
          // Update
          const results = await http.put<WorkerEntity>(`workers/${worker.id}`, worker);
          if (results.status === 200) {
            this.workers.push(results.data);
          }
        }
      } catch (e) {
        rootStore.setError("Failed to save worker...");
      }
      finally {
        rootStore.clearBusy();
      }

    }

  }
});
