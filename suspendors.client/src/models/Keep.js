import { Profile } from "./Account"

export class Keep {
    constructor(data){
        this.id = data.id
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
        this.creator = new Profile(data.creator)
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.kept = data.kept
        this.views = data.views
    }
}

export class VaultedKeep extends Keep{
    constructor(data){
        super(data)
        this.vaultKeepId = data.vaultKeepId
    }
}