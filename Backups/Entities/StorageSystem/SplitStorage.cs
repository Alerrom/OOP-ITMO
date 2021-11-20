using System;
using System.IO;
using Backups.Abstractions;

namespace Backups.Entities.StorageSystem
{
    public class SplitStorage : IStorage
    {
        private readonly string _filePath;
        public SplitStorage(string filePath)
        {
            _filePath = filePath;
        }

        public string GetStorage() => Directory
            .CreateDirectory(Path.GetDirectoryName(_filePath) + "\\" + Path.GetFileName(_filePath) + "_Backup")
            .ToString();
    }
}