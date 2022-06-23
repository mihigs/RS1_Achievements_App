import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [
  { path: "/", alias: "/home", component: Home },
  { path: "/details", props: true, component: DetailsPageComponent },
  { path: "*", component: Home }, //could lead to a 404 not found page
];

const router = new VueRouter({
  // mode: 'history',
  base: process.env.BASE_URL,
  routes,
  //scroll to top when redirecting to new page
  scrollBehavior(to, from, savedPosition) {
    return { x: 0, y: 0 };
  },
});

export default router;
