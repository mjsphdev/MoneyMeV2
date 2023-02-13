<script setup lang="ts">
import { RouterView } from "vue-router";
import NavBar from "./components/layouts/nav-bar.vue";

</script>

<template>
  <div class="container">
    <NavBar />
    <RouterView />
  </div>
</template>
<style>
body {
  background-color: #ececec !important;
}
</style>
