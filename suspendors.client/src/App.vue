<template>
  
  <nav class="rubik">
    <Navbar />
  </nav>
  
  <main class="container-fluid rubik">
    <router-view />
  </main>
  
  <footer class="rubik">

    <Modal id="keepModal">
      <ModalKeep/>
    </Modal>
  
    <ModalNotFull id="editVaultModal">
      <ModalVaultForm/>
    </ModalNotFull>

    <ModalNotFull id="editKeepModal">
      <ModalKeepForm/>
    </ModalNotFull>

    <ModalNotFull id="editAccountModal">
      <ModalAccountForm/>
    </ModalNotFull>
  </footer>
  
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

nav{
  width: 100%;
  position: fixed;
  top: 0;
  bottom: auto;
  z-index: 1000;
  height: var(--nav-h);
}
main{
  margin: var(--nav-h) 0 0 0;
}
@media screen and (max-width: 576px) { 
  nav{
    bottom: 0;
    top: auto; 
  } 
  main{
  margin: 0 0 var(--nav-h) 0;
}
}

</style>
