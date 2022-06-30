import { useToast } from 'vue-toastification';
import { WorkTypeEntity } from "../models";
import { useRootStore } from ".";
import http from "../services/http";
import { defineStore } from "pinia";
import "../extensions";

let loaded = false;

export default defineStore("workTypes", {
  state: () => ({
    workTypes: [] as Array<WorkTypeEntity>,
  }),
  getters: {},
  actions: {
    async findWorker(id: Number): Promise<WorkTypeEntity | undefined> {
      if (await this.loadWorkTypes()) {
        return this.workTypes.find((w) => w.id === id);
      } else return undefined;
    },
    async loadWorkTypes(): Promise<Boolean> {
      // Don't reload
      if (loaded) return true;

      // Load the data
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const results = await http.get<Array<WorkTypeEntity>>("worktypes");
        if (results) {
          this.workTypes.replaceEntities(results);
          loaded = true;
          return true;
        }
      } catch {
      } finally {
        rootStore.clearBusy();
      }
      rootStore.showError("Failed to load work types...");
      return false;
    },
    async saveWorkType(worker: WorkTypeEntity): Promise<Boolean> {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        if (worker.id === 0) {
          // Create new
          const result = await http.post<WorkTypeEntity>("worktypes", worker);
          if (result) {
            
            toast("Saved...")
            this.workTypes.push(result);
            return true;
          } else {
            rootStore.showError("Failed to save work type");
          }
        } else {
          // Update
          const result = await http.put<WorkTypeEntity>(
            `worktypes/${worker.id}`,
            worker
          );
          if (result) {
            const toast = userootStore.showMessage();
            toast("Saved...")
            this.workTypes.replaceEntityInArray(result);
            return true;
          }
        }
      } catch (e) {
        rootStore.showError("Failed to save work type...");
      } finally {
        rootStore.clearBusy();
      }
      return false;
    },
    async deleteWorkType(workerId: number) {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const worker = await this.findWorker(workerId);
        // Is it actually saved?
        if (!worker || worker.id === 0) {
          return false;
        } else {
          // Create new
          const result = await http.delete<WorkTypeEntity>(`worktypes/${worker.id}`);
          if (result) {
            this.workTypes.removeEntityFromArray(worker);
            const toast = userootStore.showMessage();
            toast("Deleted...")
          } else {
            rootStore.showError("Failed to delete work type");
          }
        }
      } catch (e) {
        rootStore.showError("Failed to delete worktype...");
      } finally {
        rootStore.clearBusy();
      }
    },
  },
});
