<template>
    <section class="row">
        <div class="col-12 overflow-x-hidden  py-2 d-flex flex-row justify-content-center">
            <div class="limit-width">
                <div class="vaults-container">
                    <div v-for="v in vaults" :key="v.id" class="vault-slot">
                      <VaultCard :vault="v"/>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>
  
<script>
  export default {
    props:{
        vaults: {type: Array, required: true}
    },
    setup() {
      return {
  
      }
    }
  }
</script>

<style lang="scss" scoped>
.vaults-container{
  width: 100%;
  columns: 4 225px;
  column-gap: 1rem;
}
.vault-slot{
  width: 100%;
  min-width: 150px;
  margin: 0 1rem 0 0;
  display: inline-block;
}
</style>