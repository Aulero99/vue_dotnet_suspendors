import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

const MDI = 'api/keeps'

class KeepsService {
    async getKeepsFromApi(){
        logger.log('getKeepsFromApi()')
        const res = await api.get(MDI)
        logger.log(res.data)
        AppState.keeps = res.data.map( k => new Keep(k) )
        logger.log(AppState.keeps)
    }
    async getKeepById(id){
        logger.log(`getKeepsFromApi(${id})`)
        const res = await api.get(MDI + `/${id}`)
        logger.log(res.data)
        AppState.activeKeep = new Keep(res.data)
        logger.log(AppState.activeKeep)
    }
    async postNewKeep(data){
        const res = await api.post('api/keeps', data)
        logger.log(res.data)
        AppState.keeps.unshift(new Keep(res.data))
    }

    async deleteKeep(id){
        const res = await api.delete(`api/keeps/${id}`)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k=> k.id != id)
    }
}

export const keepsService = new KeepsService()