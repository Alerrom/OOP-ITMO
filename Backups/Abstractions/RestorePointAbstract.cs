using System;
using System.IO;

namespace Backups.Abstractions
{
    [Serializable]
    public abstract class RestorePointAbstract
    {
        protected RestorePointAbstract(string fullPath, string originalFilePath)
        {
            CreationTime = DateTime.Now;
            FullPath = fullPath;
            Name = new FileInfo(fullPath).Name;
            DirectoryName = new FileInfo(fullPath).DirectoryName;
            OriginalFilePath = originalFilePath;
        }

        public DateTime CreationTime { get; }
        public string FullPath { get; }
        public string Name { get; }
        public string DirectoryName { get; }
        public string OriginalFilePath { get; }

        public abstract void CreateRestore();
    }
}