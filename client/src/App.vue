<script setup>
  import Sidebar from './components/Sidebar.vue';
  import Navbar from './components/Navbar.vue';
  import { RouterView } from 'vue-router';
</script>

<template>
  <section class="d-flex">
    <Sidebar />
    <div class="container-fluid">
      <Navbar />
      <RouterView />
    </div>
  </section>
</template>

<style scoped>
  .container-fluid {
    margin: 0;
    padding: 0;
  }
</style>
