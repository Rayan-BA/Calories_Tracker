<script setup></script>

<template>
  <section class="h-100">
    <div class="d-flex flex-column flex-shrink-0 p-3 bg-dark-subtle" id="sidebar-wrapper">
      <a href="/" class="d-flex align-items-center mb-3 mb-md-0 me-md-auto link-dark text-decoration-none">
        <span class="fs-4">Sidebar</span>
      </a>
      <!--<hr>-->
      <ul class="nav nav-pills flex-column mb-auto mt-5">
        <li>
          <a href="/" class="nav-link active">
            <svg class="bi me-2" width="16" height="16"><use xlink:href="#speedometer2"></use></svg>
            Dashboard
          </a>
        </li>
        <li>
          <a href="/foods" class="nav-link link-dark">
            <svg class="bi me-2" width="16" height="16"><use xlink:href="#table"></use></svg>
            Foods
          </a>
        </li>
        <li>
          <a href="/meals" class="nav-link link-dark">
            <svg class="bi me-2" width="16" height="16"><use xlink:href="#grid"></use></svg>
            Meals
          </a>
        </li>
      </ul>
    </div>
  </section>
</template>

<style scoped>
  #sidebar-wrapper {
    width: 280px;
    height: 100vh;
  }
</style>
