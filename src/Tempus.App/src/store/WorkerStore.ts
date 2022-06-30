import { useToast } from 'vue-toastification';
import { WorkerEntity } from "./../models";
import { useRootStore } from ".";
import http from "../services/http";
import { defineStore } from "pinia";
import "../extensions";

let loaded = false;

export default defineStore("workers", {
  state: () => ({
    workers: [] as Array<WorkerEntity>,
  }),
  getters: {},
  actions: {
    async findWorker(id: Number): Promise<WorkerEntity | undefined> {
      if (await this.loadWorkers()) {
        return this.workers.find((w) => w.id === id);
      } else return undefined;
    },
    async loadWorkers(): Promise<Boolean> {
      // Don't reload
      if (loaded) return true;

      // Load the data
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const results = await http.get<Array<WorkerEntity>>("workers");
        if (results) {
          this.workers.replaceEntities(results);
          loaded = true;
          return true;
        }
      } catch {
      } finally {
        rootStore.clearBusy();
      }
      rootStore.showError("Failed to load workers...");
      return false;
    },
    async saveWorker(worker: WorkerEntity) : Promise<boolean> {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        if (worker.id === 0) {
          // Create new
          const result = await http.post<WorkerEntity>("workers", worker);
          if (result) {
            
            rootStore.showMessage("Saved...")
            this.workers.push(result);
            return true;
          } else {
            rootStore.showError("Failed to save worker");
          }
        } else {
          // Update
          const result = await http.put<WorkerEntity>(
            `workers/${worker.id}`,
            worker
          );
          if (result) {
            
            rootStore.showMessage("Saved...")
            this.workers.replaceEntityInArray(result);
          }
        }
      } catch (e) {
        rootStore.showError("Failed to save worker...");
      } finally {
        rootStore.clearBusy();
      }
      return false;
    },
    async deleteWorker(workerId: number): Promise<Boolean> {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const worker = await this.findWorker(workerId);
        // Is it actually saved?
        if (worker && worker.id > 0) {
          // Delete
          const result = await http.delete<WorkerEntity>(`workers/${worker.id}`);
          if (result) {
            this.workers.removeEntityFromArray(worker);
            
            rootStore.showMessage("Deleted...")
            return true;
          } else {
            rootStore.showError("Failed to delete worker");
          }
        }
      } catch (e) {
        rootStore.showError("Failed to delete worker...");
      } finally {
        rootStore.clearBusy();
      }
      return false;
    },
  },
});
