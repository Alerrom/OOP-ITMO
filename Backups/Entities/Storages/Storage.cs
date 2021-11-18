using Backups.Abstractions;
using Backups.Entities.CommandLineParser;
using Backups.Tools;

namespace Backups.Entities.Storages
{
    public class Storage : IStorage
    {
        private readonly StorageType _storageType;
        private readonly string _filePath;
        private readonly string _commonStorage;

        public Storage(StorageType storageType, string filePath, string commonFolder)
        {
            _storageType = storageType;
            _filePath = filePath;
            _commonStorage = commonFolder;
        }

        public string GetStorage()
        {
            return _storageType switch
            {
                StorageType.Separate => new SeparateStorage(_filePath).GetStorage(),
                StorageType.Common => new CommonStorage(_commonStorage).GetStorage(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}