namespace keeper.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal void Delete(int vaultId, string id)
        {
            Vault original = _repo.GetVaultById(vaultId);
            if(original.CreatorId != id) throw new Exception("Unauthorized: Cannot delete this vault.");
            int rows = _repo.Delete(vaultId);
            if (rows > 1) throw new Exception($"Error: {rows} rows were deleted instead of 1.");
        }

        internal Vault Edit(Vault data)
        {
            Vault original = _repo.GetVaultById(data.Id);
            if(original.CreatorId != data.CreatorId) throw new Exception("Unauthorized: Cannot edit this keep.");

            data.Name = data.Name != null ? data.Name : original.Name;
            data.Description = data.Description != null ? data.Description : original.Description;
            data.Img = data.Img != null ? data.Img : original.Img;
            data.IsPrivate = data.IsPrivate != null ? data.IsPrivate : original.IsPrivate;

            return _repo.Edit(data);
        }

        internal object GetNonPrivateVaultsByCreatorId(string profileId)
        {
            return _repo.getNonPrivateVaultsByCreatorId(profileId);
        }

        internal Vault GetVaultById(int vaultId, string id)
        {
            Vault vault = _repo.GetVaultById(vaultId);
            if(vault.Name == null) throw new Exception($"No vault id:{vaultId} found.");
            if(vault.IsPrivate == true && vault.CreatorId != id) throw new Exception($"Vault id {vaultId} is private, and you do not have permission for access.");
            return vault;
        }

        internal ActionResult<List<VaultBase>> GetVaultsByUserId(string Id)
        {
            return _repo.GetVaultsByCreatorId(Id);
        }

        internal Vault PostNewVault(Vault data)
        {
            return _repo.PostNewVault(data);
        }
    }
}