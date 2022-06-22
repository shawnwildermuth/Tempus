import { createApp } from 'vue'
import App from './App.vue'
import "./app.css";
import router from "./router";
import createIcons from "./fa";
import { createPinia  } from "pinia";

import Toast from "vue-toastification";
import toastOptions from './toastOptions';
import "vue-toastification/dist/index.css";

createApp(App)
  .use(router)
  .use(createPinia ())
  .use(Toast, toastOptions)
  .component("fa", createIcons())
  .mount('#app');
