import { PluginOptions, TYPE, POSITION } from "vue-toastification";

const options: PluginOptions = {
  toastDefaults: {
    [TYPE.DEFAULT]: {
      position: POSITION.BOTTOM_RIGHT,
      timeout: 5000,
    },
    [TYPE.ERROR]: {
      position: POSITION.BOTTOM_RIGHT,
      timeout: 5000,
    }
  },
};

export default options;
