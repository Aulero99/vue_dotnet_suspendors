<template>
  <section class="row mt-4 py-2" v-if="vault && keeps">
    <VaultCardLg :vault="vault" :keeps="keeps.length"/>
  </section>

  <KeepsContainer class="mt-4" v-if="vault && keeps"/>
  
  <section class="row mt-4" v-else>
    <Loader/>
  </section>
  
</template>
  
<script>
import { computed, onMounted, onUnmounted } from 'vue'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { router } from '../router'

  export default {
    setup() {
      const route = useRoute().params.id

      async function getKeepsInVault(){
        try {
          await vaultsService.getKeepsInVault(route);
        } catch (error) {
          logger.log(error, 'getKeepsInVault()');
          Pop.error(error);
        }
      }

      async function getActiveVault(){
        try {
          await vaultsService.getActiveVault(route);
          getKeepsInVault();
        } catch (error) {
          router.push('/');
          logger.log(error, 'getActiveVault()');
          Pop.error(error);
        }
      }

      onMounted(()=>{ getActiveVault() })

      onUnmounted(()=>{
        AppState.keeps = [];
        AppState.activeVault = null;
      })

      return {
        keeps: computed(()=> AppState?.keeps ),
        vault: computed(()=> AppState?.activeVault ),
        async deleteVault(id){
          try {
            if(await Pop.confirm('Do you want to delete this vault?')){
              await vaultsService.deleteVault(id);
              router.push('/');
            }
          } catch (error) {
            Pop.error(error);
            logger.error(`deleteVault(${id})`,error);
          }
        }
      }
    }
  }
</script>

<style scoped>
</style>