<template>
  <section class="row" v-if="keeps.length > 0">
    <div class="col-12 overflow-x-hidden py-2 d-flex flex-row justify-content-center">
      <div class="limit-width">
        <div class="keeps-container">
          <div v-for="k in keeps" :key="k.id" class="keep-slot">
            <KeepCard :keep="k"/>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
  
<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
  export default {
    props:{

    },
    setup() {
      return {
        keeps: computed(() => AppState.keeps)
      }
    }
  }
</script>

<style scoped>
.keeps-container{
  width: 100%;
  columns: 6 150px;
  column-gap: 1rem;
}
.keep-slot{
  width: 100%;
  min-width: 100px;
  margin: 0 1rem 1rem 0;
  display: inline-block;
}
</style>