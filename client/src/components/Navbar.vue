<script setup></script>

<template>
  <section class="w-100">
    <nav class="navbar navbar-expand-lg navbar-light bg-light justify-content-end" id="nav-wrapper">
      <div class="dropdown" id="user-dropdown">
        <a href="#" class="d-flex align-items-center link-dark text-decoration-none dropdown-toggle" id="dropdownUser2" data-bs-toggle="dropdown" aria-expanded="false">
          <img src="https://github.com/mdo.png" alt="" width="32" height="32" class="rounded-circle me-2">
          <strong>mdo</strong>
        </a>
        <ul class="dropdown-menu text-small shadow" aria-labelledby="dropdownUser2">
          <li><a class="dropdown-item" href="#">New project...</a></li>
          <li><a class="dropdown-item" href="#">Settings</a></li>
          <li><a class="dropdown-item" href="#">Profile</a></li>
          <li><hr class="dropdown-divider"></li>
          <li><a class="dropdown-item" href="#">Sign out</a></li>
        </ul>
      </div>
    </nav>
  </section>
</template>

<style scoped>
  #user-dropdown {
    margin-right: 5rem;
  }

  section {
    height: fit-content;
  }
</style>
