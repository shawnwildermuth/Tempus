/** @type {import('tailwindcss').Config} */
const plugin = require("tailwindcss/plugin");

module.exports = plugin(function ({ addBase, theme, addComponents }) {
  addBase({
    h1: { fontSize: theme("fontSize.3xl"), fontWeight: "bold" },
    h2: { fontSize: theme("fontSize.2xl"), fontWeight: "bold" },
    h3: { fontSize: theme("fontSize.xl"), fontWeight: "bold" },
    h4: { fontSize: theme("fontSize.lg") },
  }),
    addComponents({
      ".button": {
        borderWidth: theme("borderWidth.DEFAULT"),
        borderColor: theme("colors.slate.800"),
        borderRadius: theme("borderRadius.DEFAULT"),
        padding: theme("spacing.2"),
        marginTop: theme("spacing.1"),
        marginBottom: theme("spacing.1"),
        backgroundColor: theme("colors.slate.500"),
        color: theme("colors.white"),
        fontWeight: "bold",
        display: "inline-block",
      },
      ".button:hover": {
        backgroundColor: theme("colors.slate.700"),
      },
    });
});
