using System;
using System.Collections.Generic;
using Backups.Entities.CommandLineParser;

namespace Backups.Entities
{
    [Serializable]
    public class Backup
    {
        public Backup(string originalFilePath, string fullPath, StorageType storageType)
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            FullPath = fullPath;
            OriginalFilePath = originalFilePath;
            RestorePoints = new List<RestorePoint>();
            StorageType = storageType;
            RestorePointsCount = 0;
        }

        public Guid Id { get; }
        public DateTime CreationTime { get; }
        public string FullPath { get; }
        public string OriginalFilePath { get; }
        public List<RestorePoint> RestorePoints { get; }
        public int RestorePointsCount { get; }
        private StorageType StorageType { get; }

        public void CreateRestorePoint(RestorePoint restorePoint)
        {
            RestorePoints.Add(restorePoint);
        }

        public void GetInfo()
        {
            Console.WriteLine($"Backup ID: {Id}\n" +
                              $"Creation time: {CreationTime}\n" +
                              $"Restore points amount: {RestorePoints.Count}");
        }
    }
}