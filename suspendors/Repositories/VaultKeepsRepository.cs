namespace keeper.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;
        private readonly KeepsRepository _kr;

        public VaultKeepsRepository(IDbConnection db, KeepsRepository kr)
        {
            _db = db;
            _kr = kr;
        }

        internal int Delete(int Id)
        {
            string sql = @"
            DELETE FROM vaultKeeps
            WHERE id = @Id
            LIMIT 1
            ;";
            int rows = _db.Execute(sql, new{Id});
            return rows;
        }

        internal List<VaultedKeep> GetKeepsFromVault(int vaultId)
        {

            string sql = @"
            SELECT 
            vk.*,
            k.*,
            a.*
            FROM vaultKeeps     AS vk
            JOIN keeps          AS k ON k.id = vk.KeepId
            JOIN accounts       AS a on a.id = k.CreatorId
            WHERE vk.VaultId = @vaultId
            ;";

            List<VaultedKeep> keeps = _db.Query<VaultKeep, VaultedKeep, Profile, VaultedKeep>(sql, (vk,k, p)=>{
                k.VaultKeepId = vk.Id;
                k.Creator = p;
                return k;
            }, new{vaultId}).ToList();
            return keeps;
        }

        internal VaultKeep GetVaultKeepById(int Id)
        {
            string sql = @"
            SELECT * FROM vaultKeeps vk WHERE vk.id = @Id 
            ;";
            return _db.Query<VaultKeep>(sql, new{Id}).FirstOrDefault();
        }

        internal VaultKeep Post(VaultKeep data)
        // TODO this may not return fully valid data, test it out later
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (CreatorId, VaultId, KeepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            
            SELECT 
            vk.*
            FROM vaultKeeps vk
            WHERE vk.id = LAST_INSERT_ID()
            ;";

            VaultKeep vk = _db.Query<VaultKeep>(sql, data).FirstOrDefault();
            // vk.Keep = _kr.GetKeepById(vk.KeepId);
            return vk;
        }
    }
}