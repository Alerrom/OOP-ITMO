using Backups.Tools;

namespace Backups.Entities.StorageSystem
{
    public class StorageFactory
    {
        private readonly StorageType _storageType;
        private readonly string _filePath;
        private readonly string _commonFolder;

        public StorageFactory(StorageType storageType, string filePath, string commonFolder)
        {
            _storageType = storageType;
            _filePath = filePath;
            _commonFolder = commonFolder;
        }

        public string GetStorage()
        {
            return _storageType switch
            {
                StorageType.Common => new SingleStorage(_filePath).GetStorage(),

                // StorageType.Split => new SplitStorage(_commonFolder).GetStorage(),
                _ => throw new InvalidStorageTypeException()
            };
        }
    }
}