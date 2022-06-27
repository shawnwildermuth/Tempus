// Order is important!
import "vue-toastification/dist/index.css";
import 'vue-universal-modal/dist/index.css'
import "./app.css";

import { createApp } from 'vue'
import App from './App.vue'
import router from "./router";
import createIcons from "./fa";
import { createPinia  } from "pinia";
import Tempus from "./plugins/Tempus";
import Toast from "vue-toastification";
import toastOptions from './toastOptions';
import VueUniversalModal from 'vue-universal-modal'
import ConfirmDialog from "./components/confirm-dialog.vue";


createApp(App)
  .use(router)
  .use(createPinia ())
  .use(Toast, toastOptions)
  .use(VueUniversalModal, { teleportTarget: "#modals"})
  .use(Tempus)
  .component("fa", createIcons())
  .component("confirm-dialog", ConfirmDialog)
  .mount('#app');
