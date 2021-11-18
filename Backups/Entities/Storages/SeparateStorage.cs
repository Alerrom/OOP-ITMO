using System.IO;
using Backups.Abstractions;

namespace Backups.Entities.Storages
{
    public class SeparateStorage : IStorage
    {
        private readonly string _filePath;

        public SeparateStorage(string splitStorage)
        {
            _filePath = splitStorage;
        }

        public string GetStorage() => Directory
            .CreateDirectory(Path.GetDirectoryName(_filePath) + "\\" + Path.GetFileName(_filePath) + "_Backup")
            .ToString();
    }
}