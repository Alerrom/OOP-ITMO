using System;
using System.Globalization;
using System.IO;

namespace Backups.Entities
{
    [Serializable]
    public class RestorePoint
    {
        public RestorePoint(string fullPath, string originalFilePath)
        {
            CreationTime = DateTime.Now;
            Length = new FileInfo(originalFilePath).Length;
            FullPath = fullPath;
            Name = new FileInfo(fullPath).Name;
            DirectoryName = new FileInfo(fullPath).DirectoryName;
            OriginalFilePath = originalFilePath;
        }

        public DateTime CreationTime { get; }
        public long Length { get; }
        public string FullPath { get; }
        public string Name { get; }
        public string DirectoryName { get; }
        public string OriginalFilePath { get; }
        public void CreateRestorePoint()
        {
            File.Create(FullPath).Close();
        }
    }
}