<template>
    <div class="col-12 d-flex flex-row justify-content-center align-items-center">
      <div class="limit-width d-flex flex-row justify-content-center">
        <div class="vault-container d-flex flex-column align-items-center elevation-3">
          <div class="vault-img fill">
            <img :src="vault.img" :alt="vault.name" :title="vault.name">
          </div>
          
          <div class="vault-details">
            <div class="title">
              {{ vault.name }}
            </div>
          </div>
          
          <DeleteButton @click="deleteVault(vault.id)" class="delete-button" title="Delete This Vault" v-if="vault.creatorId == account.id"/>
        
        </div>
      </div>
    </div>
    <div class="col-12 text-center mt-3 d-flex flex-row justify-content-center" v-if="keeps">
      <div class="keep-counter elevation-2">
        {{keeps}} Keeps
      </div>
    </div>
</template>
  
<script>
import { computed } from 'vue'
import { Vault } from '../models/Vault'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { router } from '../router'
  export default {
    props:{ 
        vault: { type: Vault, required: true },
        keeps: { type: Number, required: false }
    },
    
    setup() {

      
        return {
          account: computed(()=> AppState.account),

          async deleteVault(id){
            try {
              if(await Pop.confirm("Are you sure you want to delete this vault?")){
                await vaultsService.deleteVault(id)
                router.push('/')
              }
            } catch (error) {
              logger.log(error)
              Pop.error(error)
            }
          }
      }
    }
  }
</script>

<style lang="scss" scoped>
.keep-counter{
  color: var(--cs-6);
  background-color: var(--cs-3);
  width: max-content;
  padding: 0.25rem 0.5rem;
  border-radius: 0.5rem;
  font-weight: 500;
}
.vault-container{
  position: relative;
  width: 70%;
  aspect-ratio: 11/7;
  max-height: 20rem;
  overflow: hidden;
  border-radius: 0.5rem;
}
.vault-img img{
  height: 100%;
  width: 100%;
  object-fit: cover;
}
.vault-details{
  position: absolute;
  height: 100%;
  width: 100%;
  z-index: 1;
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  text-align: center;
  padding: 0.75rem;
  background: rgb(0,0,0);
  background: linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0.25) 50%, rgba(0,0,0,0) 100%);
  color: var(--cs-1);
  font-size: 1.7rem;
  font-weight: 500;
}
.delete-button{
  position: absolute;
  z-index: 2;
  top: 0.85rem;
  right: 1rem;
}
</style>