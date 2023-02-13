import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import Toast from "vue-toastification";

import "vue-toastification/dist/index.css";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

import "./assets/main.css"
const app = createApp(App);
app.use(Toast);
app.use(router);

app.mount("#app");
