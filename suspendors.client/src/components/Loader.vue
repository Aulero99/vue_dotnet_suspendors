<template>
    <div class="col-12 d-flex flex-row justify-content-center">
        <div class="loader-container d-flex flex-row justify-content-center align-items-center">
            <span class="loader elevation-3"></span>
        </div>
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
.loader-container{
    height: calc(95vh - var(--nav-h));
    aspect-ratio: 1/1;
}
.loader {
    border: 3rem solid;
    border-color: var(--cs-1) var(--cs-2) var(--cs-4) var(--cs-5);
    border-radius: 50%;
    display: inline-block;
    box-sizing: border-box;
    animation: animloader 1s linear infinite;
}

@keyframes animloader {
    0% { border-color: var(--cs-1) var(--cs-2) var(--cs-4) var(--cs-5); }
    25% { border-color: var(--cs-5) var(--cs-1) var(--cs-2) var(--cs-4); }
    50% { border-color: var(--cs-4) var(--cs-5) var(--cs-1) var(--cs-2); }
    75% { border-color: var(--cs-2) var(--cs-4) var(--cs-5) var(--cs-1); }
    100% { border-color: var(--cs-1) var(--cs-2) var(--cs-4) var(--cs-5); }
  }
  
</style>