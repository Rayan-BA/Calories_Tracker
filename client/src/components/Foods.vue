<script setup>
  import { ref, onMounted } from 'vue';
  import Food from './Food.vue';

  const foods: Array<Object>;
  onMounted(async () => {
    //const response = await fetch('http://localhost:5267/foods');
    //foods.value = await response.json();
    foods = [
      {
        Name: 'Apple',
        Description: 'A fruit that is red or green in color.',
        ServingSize: '100',
        ServingSizeUnit: 'g',
        NutrinotalFacts: {
          "Protein": 0.3,
          "Fat": 0.2,
          "Carbohydrate": 14,
        }
      },
      {
        Name: 'Banana',
        Description: 'A fruit that is yellow in color.',
        ServingSize: '100',
        ServingSizeUnit: 'g',
        NutrinotalFacts: {
          "Protein": 1.1,
          "Fat": 0.3,
          "Carbohydrate": 23,
        }
      },
      {
        Name: 'Orange',
        Description: 'A fruit that is orange in color.',
        ServingSize: '100',
        ServingSizeUnit: 'g',
        NutrinotalFacts: {
          "Protein": 1.2,
          "Fat": 0.2,
          "Carbohydrate": 12,
        }
      },
    ];
  });
</script>

<template>
  <section class="container">
    <div class="row">
      <h1>My Foods</h1>
    </div>
    <div class="row">
      <div class="card">
        <div class="card-header">
          <button class="btn btn-primary">Add Food</button>
        </div>
        <div class="card-body">
          <ul class="border list-group">
            <Food v-for="(food, idx) in foods" :key="idx" :food="food" />
          </ul>
        </div>
      </div>
    </div>
  </section>
</template>
