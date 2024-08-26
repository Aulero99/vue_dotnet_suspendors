<template>
    <div type="button"
    class="delete-button elevation-2 d-flex flex-row justify-content-center align-items-center" 
    >
      <i class="mdi mdi-trash-can"></i>
    </div>
</template>

<style lang="scss" scoped>
.delete-button{
    background-color: var(--cs-4);
    color: var(--cs-6);
    height: 1.5rem;
    aspect-ratio: 1/1;
    border-radius: 50%;
    cursor: pointer;
    transition: all var(--ease);
      &:hover{
        background-color: var(--cs-6);
        color: var(--cs-1);
      }
}
</style>