namespace keeper.Models
{
    public class KeepBase
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public Profile Creator { get; set; }
    }

        public class Keep : KeepBase
    {
        public int Kept { get; set; }
    }

    public class VaultedKeep : Keep
    {
        public int VaultKeepId { get; set; }
    }
}