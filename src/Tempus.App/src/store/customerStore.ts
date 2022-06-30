import { useToast } from 'vue-toastification';
import { CustomerEntity, LocationEntity, ContactEntity } from "../models";
import { useRootStore } from ".";
import http from "../services/http";
import { defineStore, storeToRefs } from "pinia";
import "@/extensions";
import { mapToIEntity } from '../utils';

let loaded = false;

export default defineStore("customers", {
  state: () => ({
    customers: [] as Array<CustomerEntity>,
  }),
  getters: {
  },
  actions: {
    async findCustomer(id: Number): Promise<CustomerEntity | undefined> {
      if (await this.loadCustomers()) {
        const found = this.customers.find((w) => w.id === id);
        return found;
      } else return undefined;
    },
    async loadCustomers(): Promise<Boolean> {
      // Don't reload
      if (loaded) return true;

      // Load the data
      const rootStore = useRootStore();
      try {
        const rootStore = useRootStore();
        rootStore.setBusy();
        const results = await http.get<Array<CustomerEntity>>("customers");
        if (results) {
          this.customers.replaceEntities(results);
          loaded = true;
          return true;
        }
      } catch {
        // NOOP
      } finally {
        rootStore.clearBusy();
      }
      rootStore.showError("Failed to load customers...");
      return false;
    },
    async saveContact(customer: CustomerEntity, contact: ContactEntity): Promise<Boolean> {
      if (contact.id === 0) {
        // New
        customer.contacts.push(contact);
      } else {
        customer.contacts.replaceEntityInArray(contact);
      }
      return await this.saveCustomer(customer);
    },
    async saveCustomer(customer: CustomerEntity): Promise<Boolean> {
      const rootStore = useRootStore();
      try {
        rootStore.setBusy();
        if (customer.id === 0) {
          // Create new
          const result = await http.post<CustomerEntity>("customers", customer);
          if (result) {
            rootStore.showMessage("Saved...")
            this.customers.push(result);
            return true;
          } else {
            rootStore.showError("Failed to save customer");
          }
        } else {
          // Update
          const result = await http.put<CustomerEntity>(
            `customers/${customer.id}`,
            customer
          );
          if (result) {
            rootStore.showMessage("Saved...")
            const customer = this.customers.find(s => s.id == result.id);
            mapToIEntity(result, customer)
              return true;
          }
        }
      } catch (e) {
        rootStore.showError("Failed to save customer...");
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
            
            rootStore.showMessage("Deleted...")
          } else {
            rootStore.showError("Failed to delete customer");
          }
        }
      } catch (e) {
        rootStore.showError("Failed to delete customers...");
      } finally {
        rootStore.clearBusy();
      }
    },
  },
});
