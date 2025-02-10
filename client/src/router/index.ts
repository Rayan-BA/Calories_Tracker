import { createRouter, createWebHistory } from 'vue-router';

import FoodsView from '../views/FoodsView.vue';
import DashboardView from '../views/DashboardView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: DashboardView
    },
    {
      path: '/foods',
      name: 'foods',
      component: FoodsView
    },
  ]
});

export default router;
