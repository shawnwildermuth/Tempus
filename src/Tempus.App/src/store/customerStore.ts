import { useToast } from 'vue-toastification';
import { CustomerEntity, LocationEntity, ContactEntity } from "../models";
import { useRootStore } from ".";
import http from "../services/http";
import { defineStore } from "pinia";
import "../extensions";

let loaded = false;

export default defineStore("customers", {
  state: () => ({
    customers: [] as Array<CustomerEntity>,
  }),
  getters: {},
  actions: {
    async findCustomer(id: Number): Promise<CustomerEntity | undefined> {
      if (await this.loadCustomers()) {
        return this.customers.find((w) => w.id === id);
      } else return undefined;
    },
    async loadCustomers(): Promise<Boolean> {
      // Don't reload
      if (loaded) return true;

      // Load the data
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const results = await http.get<Array<CustomerEntity>>("customers");
        if (results) {
          this.customers.replaceEntities(results);
          loaded = true;
          return true;
        }
      } catch {
      } finally {
        rootStore.clearBusy();
      }
      rootStore.setError("Failed to load customers...");
      return false;
    },
    async saveCustomer(customer: CustomerEntity): Promise<Boolean> {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        if (customer.id === 0) {
          // Create new
          const result = await http.post<CustomerEntity>("customers", customer);
          if (result) {
            const toast = useToast();
            toast("Saved...")
            this.customers.push(result);
            return true;
          } else {
            rootStore.setError("Failed to save customer");
          }
        } else {
          // Update
          const result = await http.put<CustomerEntity>(
            `customers/${customer.id}`,
            customer
          );
          if (result) {
            const toast = useToast();
            toast("Saved...")
            this.customers.replaceEntityInArray(result);
            return true;
          }
        }
      } catch (e) {
        rootStore.setError("Failed to save customer...");
      } finally {
        rootStore.clearBusy();
      }
      return false;
    },
    async deleteCustomer(customerId: number) {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        const customer = await this.findCustomer(customerId);
        // Is it actually saved?
        if (!customer || customer.id === 0) {
          return false;
        } else {
          // Create new
          const result = await http.delete<CustomerEntity>(`customers/${customer.id}`);
          if (result) {
            this.customers.removeEntityFromArray(customer);
            const toast = useToast();
            toast("Deleted...")
          } else {
            rootStore.setError("Failed to delete customer");
          }
        }
      } catch (e) {
        rootStore.setError("Failed to delete customers...");
      } finally {
        rootStore.clearBusy();
      }
    },
  },
});
