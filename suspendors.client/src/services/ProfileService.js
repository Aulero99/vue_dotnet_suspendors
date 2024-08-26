import { AppState } from "../AppState"
import { Profile } from "../models/Account"
import { Keep } from "../models/Keep"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

const MDI = 'api/profiles'

class ProfileService{
    async getProfile(id){
        logger.log(`getProfile(${id})`)
        const res = await api.get(MDI + `/${id}`)
        AppState.profile = new Profile(res.data)
        logger.log('The profile is now',AppState.profile)

    }
    async getProfileKeeps(id){
        logger.log(`getProfileKeeps(${id})`)
        const res = await api.get(MDI + `/${id}/keeps`)
        AppState.keeps = res.data.map(k=>new Keep(k))
        logger.log('The keeps are now',AppState.keeps)
    }
    async getProfileVaults(id){
        logger.log(`getProfileVaults(${id})`)
        const res = await api.get(MDI + `/${id}/vaults`)
        AppState.vaults = res.data.map(v=>new Vault(v))
        logger.log('The keeps are now',AppState.keeps)
    }
}
export const profileService = new ProfileService()