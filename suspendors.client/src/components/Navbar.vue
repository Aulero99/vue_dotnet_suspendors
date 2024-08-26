<template>
  <section class="navbar fill elevation-3 d-flex flex-row justify-content-center">
    <div class="limit-width fill d-flex flex-row justify-content-between ">
      
      <div class="d-flex flex-row justify-content-sm-start justify-content-center links fill-y nav-section order-2 order-sm-1">
          <div class="link fill-y d-none d-sm-flex">
            <router-link :to="{ name: 'Home' }" class="d-flex">
              Home
            </router-link>
          </div>
          <CreateDropdown v-if="user.isAuthenticated"/>      
      </div>
        
      <div class="fill-y nav-section order-1 order-sm-2">
        <router-link class="navbar-brand d-flex flex-row justify-content-start justify-content-sm-center align-items-start fill" :to="{ name: 'Home' }">
          <Logo title="home"/>
        </router-link>
      </div>
        
      <div class="fill-y nav-section d-flex flex-row justify-content-end order-3">      
        <!-- LOGIN COMPONENT HERE -->
        <Login />
      </div>

    </div>
  </section>
</template>

<script>
import { computed } from 'vue';
import Login from './Login.vue';
import { AppState } from '../AppState';
export default {
  setup() {
    return {
      user: computed(()=>AppState.user)
    }
  },
  components: { Login }
}
</script>

<style lang="scss" scoped>
.navbar{
  background-color: var(--cs-1);
  padding: 0 1rem;
}
.nav-section{ width: 33%; }
.links{
  color: var(--cs-6);
    a{ 
      transition: all var(--ease); 
        &:hover {
          text-decoration: none;
          background-color: var(--cs-4);
        }
    }
}
.link{
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  margin-right: 0.5rem;
    a{
      padding: 0.25rem 0.5rem;
      border-radius: 0.5rem;
      color: var(--cs-6);
      font-weight: 500;
    }
    .router-link-exact-active { background-color: var(--cs-4); }
}
</style>
