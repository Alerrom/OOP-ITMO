namespace Backups.Entities.StorageSystem
{
    public class SingleStorage
    {
        private readonly string _singleStorage;
        public SingleStorage(string singleStorage)
        {
            _singleStorage = singleStorage;
        }

        public string GetStorage() => _singleStorage;
    }
}