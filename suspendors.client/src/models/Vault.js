export class Vault{
    constructor(data){
        this.id = data.id
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
        this.creatorId = data.creatorId
        this.description = data.description
        this.isPrivate = data.isPrivate
        this.name = data.name
        this.img = data.img
    }
}