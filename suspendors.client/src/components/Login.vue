<template>
        <div class="account-menu" v-if="user.isAuthenticated">
          <div class="fill-y d-flex flex-row" >
            
            <div class="fill-y p-2">
              <img :src="account.picture" :alt="account.name" class="user-img fill-y elevation-2">
            </div>

            <div class="options elevation-3">
              <router-link :to="{ name: 'Account' }" class="fill">
              <div class="drop-link">
                  Account Page
                </div>
              </router-link>
              <div
              class="drop-link" 
              type="button" 
              @click="logout()" 
              title="logout"
              >
                Logout
              </div>
            </div>
          </div>
        </div>

        <div class="link fill-y" v-else>
          <div class="button" type="button" @click="login()">
            Login
          </div>
        </div>
    
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState?.user),
      account: computed(() => AppState?.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.user-img{
  aspect-ratio: 1/1;
  object-fit: cover;
  border-radius: 50%;
  cursor: pointer;
}
.button{
  padding: 0.25rem 0.5rem;
  border-radius: 0.5rem;
  color: var(--cs-6);
  font-weight: 500;
  transition: all var(--ease);
    &:hover{
      text-decoration: none;
      background-color: var(--cs-4);
    }
}
.link{
  color: var(--cs-6);
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  margin-right: 0.5rem;
}
.account-menu{
  position: relative;
  display: inline-block;
    &:hover .options {
      display: block;
      max-height: 8rem;
      opacity: 100%;
    }
}
.options{
  overflow: hidden;
  max-height: 0;
  opacity: 0%;
  position: absolute;
  right: 0;
  top: var(--nav-h);
  background-color: var(--cs-7);
  min-width: 160px;
  z-index: 1000;
  transition: opacity var(--ease);
}
.drop-link {
  color: var(--cs-6);
  padding: 12px 16px;
  text-decoration: none;
  transition: all var(--ease);
    &:hover { background-color: var(--cs-4) }
}

@media screen and (max-width: 576px) { 
    .options{
        top: auto;
        bottom: var(--nav-h);
    }
}
</style>
