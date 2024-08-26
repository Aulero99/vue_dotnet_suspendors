<template>
  <div class="d-flex flex-sm-row flex-column overflow-hidden modal-content-container fill" v-if="keep">

    <div class="modal-slot keep-img">
      <img :src="keep.img" :alt="keep.name" :title="keep.name">
    </div>

    <div class="modal-slot keep-content d-flex flex-column">

      <div class="head d-flex flex-row justify-content-between">
        <div class="d-flex flex-row fill-y">

          <div class="views fill-y d-flex flex-row">
            <i class="mdi mdi-eye me-1" title="Views"></i>
            {{ keep.views }} 
          </div>

          <div class="kept fill-y d-flex flex-row align-items-center mx-2">
            <img src="../assets/img/keep_icon.svg" alt="kept" class="ms-2 me-1 kept-icon" title="Keeps">
            {{ keep.kept }}
          </div>
        
        </div>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      
      <div class="body flex-grow-1 d-flex flex-column">
        <h2>
          {{ keep.name }}
        </h2>
        <p>
          {{ keep.description }}
        </p>
      </div>
      
      <div class="foot d-flex flex-row">
        
        <div class="foot-section flex-grow-1 pe-2">
          <form @submit.prevent="addKeepToVault()" 
            v-if="myVaults?.length > 0 && !activeVault"
            class="d-flex flex-row">
              <label for="vaults" class="d-none">Vaults</label>
              <select name="vaults" v-model="editable.vaultId" class="form-select">
                <option disabled selected value="">Vault</option>
                <option v-for="v in myVaults" :key="v.id" :value="v.id">{{ v.name }}</option>
              </select>
              <button type="submit" 
              class="btn btn-dark ms-2"
              :disabled="editable.vaultId == ''">
                Add
              </button>
          </form>

        <button class="btn btn-dark d-flex flex-row fill-x d-flex flex-row justify-content-center" 
        v-if="activeVault && account.id == activeVault.creatorId"
        @click="removeKeepFromVault(keep.id)">
          Remove
        </button>
        </div>

        <div class="foot-section flex-grow-1 d-flex flex-row justify-content-end">
          <router-link class="txt-cs-6" :to="{ name: 'Profile', params: { id: keep.creator.id } }" :title="keep.creator.name"
          @click="closeModal()">
            <div class="creator d-flex flex-row justify-content-start align-items-center">
              <span class="creator-name">
                {{ keep.creator.name }}
              </span>
              <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" class="elevation-1"> 
            </div>
          </router-link>
        </div>

      </div>

    </div>

  </div>
</template>
  
<script>
import { computed, ref } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
  export default {
    setup() {
      const editable = ref({ vaultId: ''})
      return {
        editable,
        keep: computed(()=>AppState?.activeKeep),
        myVaults: computed(()=>AppState?.myVaults),
        activeVault: computed(()=>AppState?.activeVault),
        account: computed(()=>AppState?.account),

        async addKeepToVault(){
          logger.log('adding keep to vault');
          if(editable.value.vaultId == ''){
            logger.log('no vault selected');
            return;
          }
          editable.value.keepId = AppState.activeKeep.id;
          logger.log(editable.value);
          try {
            await vaultsService.addKeepToVault(editable.value);
            AppState.activeKeep.kept++;
            Pop.success('Added to Vault');
          } catch (error) {
            logger.log(error, 'addKeepToVault()');
            Pop.error(error);
          }
        },

        async removeKeepFromVault(id){
          try {
            const vaultKeepId = AppState.keeps.find(k=> k.id == id).vaultKeepId;
            if(await Pop.confirm('Do you want to remove this keep from the vault?')){
              vaultsService.removeKeepFromVault(vaultKeepId);
              this.closeModal();
            }
          } catch (error) {
            Pop.error(error);
            logger.error(`removeKeepFromVault(${id})`, error);
          }
        },

        closeModal(){
          logger.log('closing the modal');
          Modal.getOrCreateInstance('#keepModal').hide();
        }
      }
    }
  }
</script>

<style lang="scss" scoped>
.modal-content-container{
  max-height: 100vh;
}
.modal-slot{
  width: 0;
  min-width: 45%;
}
.keep-img{
  flex-grow: 1;
    img {
      width: 100%;
      height: 100%;
      object-fit: cover;
    }
}
.keep-content{
  padding: 0.5rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
    .body{
      display: flex;
      flex-direction: column;
      justify-content: center;
      text-align: left;
      padding: 0.5rem 0;
        p{ font-size: 0.85rem; }
        h2{
          font-size: 1.8rem;
          font-weight: 500;
        }
    }
    .foot{
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
    }
}
.creator-name{
  text-align: right;
  line-height: 1;
  font-size: 0.8rem;
  margin-right: 0.25rem;
  font-weight: 500;
}
.kept-icon{ height: 60%; }
.creator img {
    height: var(--avatar-sm-size);
    aspect-ratio: 1/1;
    object-fit: cover;
    border-radius: 50%;
}
@media screen and (max-width: 576px) { 
  .modal-slot{
    width: 100% !important;
    min-height: 45vh;
  }
  .keep-img{ height: 50vh; }
}
</style>