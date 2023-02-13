import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    proxy: {
      // Proxy requests to the .NET Web API
      "/api": {
        target: "http://localhost:5070",
        changeOrigin: true,
        rewrite: (path) => path.replace("^/api", ""),
      },
    },
    port: 8035
  },
  plugins: [vue()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
});
