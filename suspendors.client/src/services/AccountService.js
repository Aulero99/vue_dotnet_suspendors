import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Keep } from '../models/Keep'
import { Vault } from '../models/Vault'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

const MDI = '/account'
class AccountService {
  async getAccount() {
    try {
      const res = await api.get(MDI)
      logger.log(res.data)
      AppState.account = new Account(res.data)
      logger.log(AppState.account)
      this.getVaultsByAccountId()
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getKeepsByAccountId(){
    logger.log('getKeepsByAccountId()')
    const res = await api.get(MDI + '/keeps')
    logger.log(res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
    logger.log(AppState.keeps)
  }

  async getVaultsByAccountId(){
    logger.log('getVaultsByAccountId()')
    const res = await api.get(MDI + '/vaults')
    logger.log(res.data)
    AppState.myVaults = res.data.map(v => new Vault(v))
    logger.log(AppState.vaults)
  }

  async editAccount(data){
    const res = await api.put('account', data)
    logger.log(res.data)
    AppState.account = new Account(res.data)
    logger.log(AppState.account)
  }
}

export const accountService = new AccountService()
