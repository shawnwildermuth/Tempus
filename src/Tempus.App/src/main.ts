import { createApp } from 'vue'
import App from './App.vue'
import "./app.css";
import router from "./router";
import createIcons from "./fa";
import { createPinia  } from "pinia";

createApp(App)
  .use(router)
  .use(createPinia ())
  .component("fa", createIcons())
  .mount('#app');
