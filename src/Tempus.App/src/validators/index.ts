import { helpers } from "@vuelidate/validators"

// Phone
const phoneExp = /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/;

export const phone = helpers.withMessage(
    "Must be a North American phone number",
    (val: string) => phoneExp.test(val));

