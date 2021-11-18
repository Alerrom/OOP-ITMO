using Backups.Abstractions;

namespace Backups.Entities.Storages
{
    public class CommonStorage : IStorage
    {
        private readonly string _commonFolder;

        public CommonStorage(string commonFolder)
        {
            _commonFolder = commonFolder;
        }

        public string GetStorage() => _commonFolder;
    }
}