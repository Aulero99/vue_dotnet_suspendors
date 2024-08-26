import { AppState } from "../AppState"
import { VaultedKeep } from "../models/Keep"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
    async getVaultsByAccountId(){
        const res = await api.get('account/vaults')
        AppState.vaults = res.data.map(v=> new Vault(v))
        logger.log(AppState.vaults)
    }

    async addKeepToVault(vaultKeep){
        const res = await api.post('api/vaultkeeps', vaultKeep)
        logger.log(res.data)
    }

    async getKeepsInVault(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log(res.data)
        AppState.keeps = res.data.map( k => new VaultedKeep(k) )
        logger.log(AppState.keeps)
    }

    async getActiveVault(id){
        const res = await api.get(`api/vaults/${id}`)
        logger.log(res.data)
        AppState.activeVault = new Vault(res.data)
        logger.log(AppState.activeVault)
    }

    async removeKeepFromVault(id){
        const res = await api.delete(`api/vaultkeeps/${id}`)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter( k => k.vaultKeepId != id )
    }

    async postNewVault(data){
        logger.log('Posting New Vault with data', data)
        const res = await api.post('api/vaults', data)
        logger.log(res.data)
        AppState.myVaults.push(new Vault(res.data))
    }

    async deleteVault(id){
        const res = await api.delete(`api/vaults/${id}`)
        logger.log(res.data)
        AppState.myVaults = AppState.myVaults.filter( v => v.id != id )
    }
}

export const vaultsService = new VaultsService()