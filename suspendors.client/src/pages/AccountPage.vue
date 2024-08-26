<template>
  <section class="row mt-4 py-2" v-if="account?.id">
    <ProfileCard :profile="account"/>
  </section>
  
  <section class="row" v-if="account?.id">
    <div class="col-12 text-center">
      {{ myVaults.length }} Vaults | {{ keeps.length }} Keeps
    </div>
  </section>
  
  <section class="row mt-4" v-if="account?.id">
    <div class="col-12 overflow-hidden d-flex flex-row justify-content-center">
      <div class="limit-width">
        <h2 class="mb-2 p-0">Vaults</h2>
      </div>
    </div>
  </section>

  <VaultsContainer :vaults="myVaults" v-if="account?.id"/>
  
  <section class="row mt-4" v-if="account?.id">
    <div class="col-12 d-flex flex-row justify-content-center">
      <div class="limit-width">
        <h2 class="p-0 mb-2">Keeps</h2>
      </div>
    </div>
  </section>
  
  <KeepsContainer v-if="keeps && account?.id"/>
  
  <section class="row mt-4" v-else>
    <Loader/>
  </section>
</template>

<script>
import { computed, onMounted, onUnmounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { accountService } from '../services/AccountService';
export default {
  setup() {

    async function getKeepsByAccountId(){
      try {
        await accountService.getKeepsByAccountId();
      } catch (error) {
        logger.log(error,"getKeepsByAccountId()");
        Pop.error(error);
      }
    }

    onMounted(()=>{
      getKeepsByAccountId();
    })
    onUnmounted(()=>{
        AppState.keeps = []
      })
    return {
      account: computed(() => AppState?.account),
      myVaults: computed(() => AppState?.myVaults),
      keeps: computed(() => AppState?.keeps)
    }
  }
}
</script>

<style scoped>
img { max-width: 100px; }
</style>
