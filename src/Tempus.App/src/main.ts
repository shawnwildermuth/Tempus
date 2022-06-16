import { createApp } from 'vue'
import App from './App.vue'
import "./app.css";
import router from "./router";
import createIcons from "./fa";

createApp(App)
  .use(router)
  .component("fa", createIcons())
  .mount('#app');
