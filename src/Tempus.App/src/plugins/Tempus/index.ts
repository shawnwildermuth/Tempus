import { Plugin } from "vue";

const vInvalid = "v-invalid";

const plugin: Plugin = {
  install(app) {
    app.directive("valid", (el, binding) => {
      el.toggleAttribute(vInvalid, binding.value.$dirty && binding.value.$invalid);
    });
  }
};

export default plugin;
