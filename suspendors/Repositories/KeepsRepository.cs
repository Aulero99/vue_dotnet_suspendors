namespace keeper.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal int Delete(int keepId)
        {
            string sql = @"
            DELETE FROM keeps
            WHERE id = @keepId
            LIMIT 1
            ;";
            int rows = _db.Execute(sql, new{keepId});
            return rows;
        }

        internal Keep Edit(Keep data)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @Views 
            WHERE id = @Id
            ;";
            _db.Execute(sql, data);
            return data;
        }

        internal List<Keep> GetAll()
        {
            string sql = @"
            SELECT 
            k.*, 
            COUNT(v.id) AS Kept,
            a.*
            FROM keeps k
            LEFT JOIN vaultKeeps v ON v.keepId = k.id
            LEFT JOIN accounts a ON k.creatorId = a.id 
            GROUP BY (k.id)
            ORDER BY k.id DESC
            ;";
            
            return _db.Query<Keep, Profile, Keep>(sql,(k,p)=>{
                k.Creator = p;
                return k;
            }).ToList();
        }

        internal Keep GetKeepById(int Id)
        {
            string sql = @"
            SELECT 
            k.*, 
            COUNT(v.id) AS Kept,
            a.*
            FROM keeps k 
            LEFT JOIN vaultKeeps v ON v.keepId = k.id
            LEFT JOIN accounts a ON k.CreatorId = a.id
            WHERE k.id = @Id
            GROUP BY (k.id)
            ;";
            
            return _db.Query<Keep, Profile, Keep>(sql,(k,p)=>{
                k.Creator = p;
                return k;
            }, new{Id}).FirstOrDefault();
        }

        internal List<Keep> GetKeepsByCreatorId(string Id)
        {
            string sql = @"
            SELECT 
            k.*, 
            COUNT(v.id) AS Kept,
            a.*
            FROM keeps k
            LEFT JOIN vaultKeeps v ON v.keepId = k.id
            LEFT JOIN accounts a ON k.creatorId = a.id 
            WHERE k.CreatorId = @Id
            GROUP BY (k.id)
            ;";
            
            return _db.Query<Keep, Profile, Keep>(sql,(k,p)=>{
                k.Creator = p;
                return k;
            }, new{Id}).ToList();
        }

        internal Keep PostNewKeep(Keep data)
        {
            string sql = @"
            INSERT INTO keeps
            (Name, Description, Img, CreatorId)
            VALUES
            (@Name, @Description, @Img, @CreatorId);

            SELECT 
            k.*, 
            COUNT(v.id) AS Kept,
            a.*
            FROM keeps k 
            LEFT JOIN vaultKeeps v ON v.keepId = k.id
            LEFT JOIN accounts a ON k.CreatorId = a.id
            WHERE k.id = LAST_INSERT_ID()
            GROUP BY (k.id);
            ;";

            var keep = _db.Query<Keep, Profile, Keep>(sql, (k,p)=>{
                k.Creator = p;
                return k;
            }, data).FirstOrDefault();
            return keep;
        }
    }
}