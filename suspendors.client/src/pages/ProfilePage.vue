<template>
  <section class="row mt-4 py-2" v-if="profile?.id">
    <ProfileCard :profile="profile"/>
  </section>

  <section class="row" v-if="profile?.id">
    <div class="col-12 text-center">
      {{ vaults.length }} Vaults | {{ keeps.length }} Keeps
    </div>
  </section>

  <section class="row mt-4" v-if="profile?.id">
    <div class="col-12 overflow-hidden d-flex flex-row justify-content-center">
      <div class="limit-width">
        <h2>Vaults</h2>
      </div>
    </div>
  </section>

  <VaultsContainer :vaults="vaults" v-if="profile?.id"/>

  <section class="row mt-4" v-if="profile?.id">
    <div class="col-12 d-flex flex-row justify-content-center">
      <div class="limit-width">
        <h2>Keeps</h2>
      </div>
    </div>
  </section>

  <KeepsContainer v-if="keeps && profile?.id"/>

  <section class="row mt-4" v-else>
    <Loader/>
  </section>
</template>
  
<script>
import { computed, onMounted, onUnmounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profileService } from '../services/ProfileService'
import { useRoute } from 'vue-router'
  export default {
    setup() {

      const route = useRoute().params.id

      async function getProfile(){
        try {
          await profileService.getProfile(route);
        } catch (error) {
          logger.log(error, "getProfileKeeps()");
          Pop.error(error);
        }
      }
      
      async function getProfileVaults(){
        try {
          await profileService.getProfileVaults(route);
        } catch (error) {
          logger.log(error, "getProfileVaults()");
          Pop.error(error);
        }
      }

      async function getProfileKeeps(){
        try {
          await profileService.getProfileKeeps(route);
        } catch (error) {
          logger.log(error, "getProfileKeeps()");
          Pop.error(error);
        }
      }

      onMounted(()=>{
        getProfileKeeps();
        getProfileVaults();
        getProfile();
      })
      onUnmounted(()=>{ AppState.keeps = [] })

      return {
        profile: computed(()=>AppState?.profile),
        vaults: computed(()=>AppState?.vaults),
        keeps: computed(()=> AppState?.keeps)
      }
    }
  }
</script>

<style scoped>

</style>