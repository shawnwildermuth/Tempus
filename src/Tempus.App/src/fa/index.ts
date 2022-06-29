import { library } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import * as solid from "@fortawesome/free-solid-svg-icons";
import * as brands from "@fortawesome/free-brands-svg-icons";

export default function (): typeof FontAwesomeIcon {
  /* add icons to the library */
  library.add(
    solid.faClock,
    solid.faStopwatch,
    solid.faSpinner,
    solid.faEye,
    solid.faXmark,
    solid.faPencil,
    solid.faCirclePlus,
    solid.faFloppyDisk
  );
  library.add(brands.faTwitter, brands.faTiktok, brands.faYoutube);
  return FontAwesomeIcon;
}
