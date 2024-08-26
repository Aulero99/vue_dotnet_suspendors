<template>
    <div class="dropdown">

        <div class="d-flex flex-column fill-y justify-content-center">
            <div class="drop-btn">
                Create
            </div>
        </div>

        <div class="drop-content elevation-3">
            <div type="button" 
            class="drop-link" 
            @click="newKeep()">
                New Keep
            </div>
            <div type="button" 
            class="drop-link" 
            @click="newVault()">
                New Vault
            </div>
        </div>

    </div>
</template>
  
<script>
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger'
  export default {
    setup() {
      return {
        newKeep(){
            logger.log('newKeep()');
            Modal.getOrCreateInstance('#editKeepModal').show();
        },
        newVault(){
            logger.log('newVault()');
            Modal.getOrCreateInstance('#editVaultModal').show();
        }
      }
    }
  }
</script>

<style lang="scss" scoped>
.dropdown {
  position: relative;
  display: inline-block;
    &:hover .drop-content {
    display: block;
    max-height: 8rem;
    opacity: 100%;
    }
    &:hover .drop-btn {
    background-color: var(--cs-4);
    }
}
.drop-btn {
  cursor: pointer;
  padding: 0.25rem 0.5rem;
  border-radius: 0.5rem;
  color: var(--cs-6);
  font-weight: 500;
  transition: all var(--ease);
}
.drop-content {
  position: absolute;
  left: 0;
  max-height: 0;
  overflow: hidden;
  opacity: 0%;
  top: var(--nav-h);
  background-color: var(--cs-7);
  min-width: 160px;
  z-index: 1000;
  transition: opacity var(--ease);
    .drop-link {
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    transition: all var(--ease);
      &:hover { background-color: var(--cs-4) }
  }
}

@media screen and (max-width: 576px) { 
    .drop-content{
        top: auto;
        bottom: var(--nav-h);
        left: -40px;
    }
}
</style>