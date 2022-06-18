/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}"
  ],
  theme: {
    extend: {
      fontFamily: {
        'sans': ['Lato', 'ui-serif', 'Helvetica', 'Arial', 'sans-serif'],
        'serif': ['Roboto Slab', 'ui-serif', 'Georgia', 'serif']
      }
    },
  },
  plugins: [],
}