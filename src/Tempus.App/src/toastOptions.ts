import { PluginOptions, TYPE, POSITION } from "vue-toastification";

const options: PluginOptions = {
  toastDefaults: {
    [TYPE.DEFAULT]: {
      position: POSITION.BOTTOM_RIGHT,
      timeout: 3000,
    },
    [TYPE.ERROR]: {
      position: POSITION.BOTTOM_RIGHT,
      timeout: false,
    }
  },
};

export default options;
