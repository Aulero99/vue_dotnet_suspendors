<template>
<div class="modal-header d-flex flex-row justify-content-between">
    <h3 class="m-0">Create New Keep</h3>
    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
</div>
<div class="modal-body">
    <form @submit.prevent="newKeep()" class="d-flex flex-column">

        <div class="form-group mb-3">
            <label for="name">Keep Name*</label>
            <input type="text" required 
            v-model="editable.name" 
            class="form-control" 
            name="name"
            maxlength="45"
            placeholder="Name of the Keep">
        </div>

        <div class="form-group mb-3">
            <label for="description">Keep Description</label>
            <input type="text" 
            v-model="editable.description" 
            class="form-control" 
            name="description"
            maxlength="1950"
            placeholder="Description of the Keep">
        </div>

        <div class="form-group mb-3">
            <label for="img">Keep img*</label>
            <input type="url" required 
            v-model="editable.img" 
            class="form-control" 
            name="img"
            maxlength="5000"
            placeholder="ex: http://image.com/image.jpg">
        </div>
        
        <small class="mb-2">* indicates required field</small>
        
        <button type="submit" class="btn btn-dark">Submit</button>
    </form>
</div>
</template>

<script>
import { ref } from 'vue'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
export default {

setup() {
    const editable = ref({})
return {
    editable,
    async newKeep(){
        try {
            await keepsService.postNewKeep(editable.value);
            Pop.success;
            Modal.getOrCreateInstance('#editVaultModal').hide();
        } catch (error) {
            logger.log(error,'newKeep()');
            Pop.error(error);
        }
    }

}
}
}
</script>

<style scoped>

</style>