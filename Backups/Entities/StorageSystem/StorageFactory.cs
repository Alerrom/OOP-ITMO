using System.Collections.Generic;
using Backups.Tools;

namespace Backups.Entities.StorageSystem
{
    /*
    public class StorageFactory
    {
        private readonly StorageType _storageType;
        private readonly List<string> _fileNames;

        public StorageFactory(StorageType storageType, List<string> fileNames)
        {
            _storageType = storageType;
            _fileNames = fileNames;
        }

        public string GetStorage()
        {
            return _storageType switch
            {
                StorageType.Common => new SingleStorage("_fileNames").GetStorage(),
                StorageType.Split => new SplitStorage("_fileNames").GetStorage(),
                _ => throw new InvalidStorageTypeException()
            };
        }
    }
    */
}