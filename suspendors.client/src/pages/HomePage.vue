<template>
  <KeepsContainer class="mt-4" v-if="keeps"/>
  <section class="row mt-4" v-else>
    <Loader/>
  </section>
</template>

<script>
import { computed, onMounted, onUnmounted } from 'vue'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'

export default {
  setup() {
    
    async function getKeepsFromApi(){
      try {
        keepsService.getKeepsFromApi();
      } catch (error) {
        Pop.error(error);
        logger.log(error, '[getKeepsFromApi()]');
      }
    }

    onMounted(()=>{ getKeepsFromApi(); })
    onUnmounted(()=>{ AppState.keeps = []; })
    
    return { 
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.myVaults) 
    }
  }
}

</script>

<style scoped lang="scss">

</style>
