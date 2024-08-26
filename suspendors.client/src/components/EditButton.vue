<template>
    <div type="button" 
    class="edit-button d-flex justify-content-center align-items-center elevation-3"
    >
        <i class="mdi mdi-pencil-outline"></i>
    </div>
</template>

<style lang="scss" scoped>
.edit-button{
    background-color: var(--cs-1);
    width: 2rem;
    height: 1.5rem;
    border-radius: 0.5rem;
    cursor: pointer;
    transition: all var(--ease);
      &:hover{
        background-color: var(--cs-6);
        color: var(--cs-1);
      }
}
</style>