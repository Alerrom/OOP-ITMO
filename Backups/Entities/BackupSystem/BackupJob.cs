using System;
using System.Collections.Generic;
using Backups.Entities.RestoreObjects;
using Backups.Entities.VfsAdapterSystem;

namespace Backups.Entities.BackupSystem
{
    public class BackupJob : IBackupJob
    {
        private readonly List<string> _jobObjects;
        private List<RestorePoint> _restorePoints;
        private VfsAdapter _adapter;

        public BackupJob(VfsAdapter adapter, StorageType storageType)
        {
            _adapter = adapter;
            StorageType = storageType;
            CreationTime = DateTime.Now;
            _restorePoints = new List<RestorePoint>();
            _jobObjects = new List<string>();
        }

        public DateTime CreationTime { get; }
        public IReadOnlyList<string> JobObjects => _jobObjects;
        public StorageType StorageType { get; private set; }

        public void ChangeStorageType(StorageType type)
        {
            StorageType = type;
        }

        public void AddJobObject(string path)
        {
            _jobObjects.Add(path);
        }

        public void DeleteJobObject(string path)
        {
            _jobObjects.Remove(path);
        }

        public void CreateRestorePoint()
        {
            string name = "RestorePoint_" + (_restorePoints.Count + 1);
            _restorePoints.Add(new RestorePoint(name, _jobObjects, StorageType, _adapter));
        }

        public override string ToString()
        {
            return $"Creation time: {CreationTime}\n" +
                   $"Restore points amount: {_restorePoints.Count}";
        }
    }
}