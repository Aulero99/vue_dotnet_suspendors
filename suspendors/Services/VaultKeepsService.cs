namespace keeper.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vs;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vs)
        {
            _repo = repo;
            _vs = vs;
        }

        internal void Delete(int vaultKeepId, string id)
        {
            VaultKeep original = _repo.GetVaultKeepById(vaultKeepId);
            if(original.CreatorId != id) throw new Exception("Unauthorized: Cannot remove this keep from this vault.");
            int rows = _repo.Delete(vaultKeepId);
            if (rows > 1) throw new Exception($"Error: {rows} rows were deleted instead of 1.");
        }

        internal List<VaultedKeep> GetKeepsFromVault(int vaultId, string id)
        {
            var keeps = _repo.GetKeepsFromVault(vaultId);
            return keeps;
        }

        internal VaultKeep Post(VaultKeep data)
        {
            // TODO users can currently make keeps for other users vaults
            // fix that
            Vault vault = _vs.GetVaultById(data.VaultId, data.CreatorId);
            if(vault.CreatorId != data.CreatorId) throw new Exception("You are not authorized to add to this vault.");
            return _repo.Post(data);
        }
    }
}