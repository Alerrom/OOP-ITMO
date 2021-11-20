using Backups.Tools;

namespace Backups.Entities.Storages
{
    public class StorageFactory
    {
        private readonly StorageType _storageType;
        private readonly string _filePath;
        private readonly string _singleStorage;

        public StorageFactory(StorageType storageType, string filePath, string singleStorage)
        {
            _storageType = storageType;
            _filePath = filePath;
            _singleStorage = singleStorage;
        }

        public string GetStorage()
        {
            switch (_storageType)
            {
                case StorageType.Common:
                    return new SingleStorage(_filePath).GetStorage();
                case StorageType.Split:
                    return new SplitStorage(_singleStorage).GetStorage();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}