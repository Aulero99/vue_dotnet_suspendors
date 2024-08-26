namespace keeper.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal void Delete(int keepId, string id)
        {
            Keep original = _repo.GetKeepById(keepId);
            if(original.CreatorId != id) throw new Exception("Unauthorized: Cannot delete this keep.");
            int rows = _repo.Delete(keepId);
            if (rows > 1) throw new Exception($"Error: {rows} rows were deleted instead of 1.");
        }

        internal Keep Edit(Keep data)
        {
            Keep original = _repo.GetKeepById(data.Id);
            if(original.CreatorId != data.CreatorId) throw new Exception("Unauthorized: Cannot edit this keep.");

            data.Description = data.Description != null ? data.Description : original.Description;
            data.Img = data.Img != null ? data.Img : original.Img;
            data.Name = data.Name != null ? data.Name : original.Name;
            data.Views = original.Views;

            return _repo.Edit(data);
        }

        internal List<Keep> GetAll()
        {
            return _repo.GetAll();
        }

        internal Keep GetKeepById(int Id)
        {
            Keep keep = _repo.GetKeepById(Id);
            if(keep.Name == null) throw new Exception($"No keep id:{Id} found.");
            keep.Views++;
            _repo.Edit(keep);
            return keep;
        }

        internal object GetKeepsByCreatorId(string profileId)
        {
            return _repo.GetKeepsByCreatorId(profileId);
        }

        internal Keep PostNewKeep(Keep data)
        {
            return _repo.PostNewKeep(data);
        }
    }
}