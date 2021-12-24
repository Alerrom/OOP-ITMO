namespace Backups.Entities.StorageSystem
{
    public class SplitStorage
    {
        private readonly string _filePath;
        public SplitStorage(string filePath)
        {
            _filePath = filePath;
        }
    }
}