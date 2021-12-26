namespace Backups.Entities.StorageSystem
{
    public class SplitStorage : IStorage
    {
        private readonly string _filePath;
        public SplitStorage(string filePath)
        {
            _filePath = filePath;
        }

        public string GetStorage()
        {
            return null;
        }
    }
}