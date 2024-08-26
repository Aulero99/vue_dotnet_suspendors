<template>
    <div class="d-flex flex-column align-items-center justify-content-center fill-y">
        <img alt="the Keepr co." src="../assets/img/logo.svg" class="logo"/>
    </div>
</template>
  
<script>
  export default {
    props:{

    },
    setup() {
      return {
  
      }
    }
  }
</script>

<style lang="scss" scoped>
.logo{
    height: 90%;
    transition: all var(--ease);
    &:hover{
        height: 94.5%;
    }
}
</style>