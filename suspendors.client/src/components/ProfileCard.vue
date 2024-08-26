<template>
        <div class="col-12 d-flex flex-row justify-content-center">
            <div class="profile-card-container">
                <div class="img-elements fill">
                    <img :src="profile.coverImg" :alt="profile.name" class="cover-img elevation-1">
                    <img :src="profile.picture" :alt="profile.name" :title="profile.name" class="avatar elevation-3">
                </div>
                <EditButton class="edit-button" @click="editAccount()" v-if="profile?.email" title="Edit Account"/>
            </div>
        </div>
        <div class="col-12 text-center mt-2">
            <h1>
                {{ profile.name }}
            </h1>
        </div>
</template>
  
<script>
import { Modal } from 'bootstrap'
import { Account, Profile } from '../models/Account'
import { logger } from '../utils/Logger'
  export default {
    props:{
        profile: { type:Profile || Account, required: true }
    },
    setup() {
      return {
        editAccount(){
            logger.log('newKeep()')
            Modal.getOrCreateInstance('#editAccountModal').show()
        }
      }
    }
  }
</script>

<style lang="scss" scoped>
.profile-card-container{
    width: 50%;
    max-width: 650px;
    aspect-ratio: 2/1;
    position: relative;
    margin-bottom: calc((var(--avatar-size)/2) - 0.5rem);
}
.edit-button{
    position: absolute;
    top: 0.5rem;
    right: 0.5rem;
}
.cover-img{
    width: 100%;
    height: 100%;
    object-fit: cover;
    border-radius: 0.5rem;
}
.avatar{
    height: var(--avatar-size);
    aspect-ratio: 1/1;
    object-fit: cover;
    border-radius: 50%;
    position: absolute;
    bottom: calc(-1 * ((var(--avatar-size)/2) - 0.5rem));
    margin-left: calc(-50% - (var(--avatar-size)/2));
    border: solid 0.2rem var(--cs-1);
}
@media screen and (max-width: 576px) { 
    .profile-card-container{
        width: 100%;
    }
}
</style>