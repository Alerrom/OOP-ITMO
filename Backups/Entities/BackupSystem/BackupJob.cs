using System;
using System.Collections.Generic;
using Backups.Entities.RestoreObjects;

namespace Backups.Entities.BackupSystem
{
    public class BackupJob
    {
        public BackupJob(string originalFilePath, string fullPath, StorageType storageType)
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            FullPath = fullPath;
            OriginalFilePath = originalFilePath;
            RestorePoints = new List<RestorePoint>();
            StorageType = storageType;
            JobObjects = new List<JobObject>();
            RestorePointsCount = 0;
        }

        public Guid Id { get; }
        public DateTime CreationTime { get; }
        public string FullPath { get; }
        public string OriginalFilePath { get; }
        public List<RestorePoint> RestorePoints { get; }
        public List<JobObject> JobObjects { get; }
        public int RestorePointsCount { get; set; }
        private StorageType StorageType { get; }

        public void CreateRestorePoint(RestorePoint restorePoint) => RestorePoints.Add(restorePoint);

        public void GetInfo() => Console.WriteLine($"BackupJob ID: {Id}\n" +
                                                   $"Creation time: {CreationTime}\n" +
                                                   $"Restore points amount: {RestorePoints.Count}");
    }
}