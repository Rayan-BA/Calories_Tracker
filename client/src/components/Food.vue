<script setup>
  import { defineProps } from 'vue';

  defineProps({
    food: Object
  });
</script>

<template>
  <a>
    <li class="list-group-item">
      food.Name
    </li>
  </a>
</template>
