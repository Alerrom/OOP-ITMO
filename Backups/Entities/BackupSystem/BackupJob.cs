using System;
using System.Collections.Generic;
using Backups.Entities.RestoreObjects;
using Backups.Entities.Vfs.Impl;
using Backups.Entities.VfsAdapterSystem;

namespace Backups.Entities.BackupSystem
{
    public class BackupJob : IBackupJob
    {
        private readonly List<string> _jobObjects;
        private VfsAdapter _adapter;

        public BackupJob(VfsAdapter adapter, StorageType storageType)
        {
            _adapter = adapter;
            StorageType = storageType;
            CreationTime = DateTime.Now;
            RestorePoints = new List<RestorePoint>();
            _jobObjects = new List<string>();
        }

        public DateTime CreationTime { get; }
        public List<RestorePoint> RestorePoints { get; }

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
            CreateRestorePoint();
        }

        public void CreateRestorePoint()
        {
            string name = "RestorePoint_" + (RestorePoints.Count + 1);
            var rp = new RestorePoint(name, _jobObjects, StorageType);
            RestorePoints.Add(rp);
            _adapter.AddDirectory($@"C:\Backup\{rp.Name}");

            if (StorageType == StorageType.Split)
            {
                foreach (string fName in rp.Files)
                {
                    string[] sep = fName.Split(@"\");

                    _adapter.AddFile($@"C:\Backup\{rp.Name}\{sep[^1]}");
                    string content = _adapter.ReadContentOnFile(fName);
                    _adapter.AddContentOnFile($@"C:\Backup\{rp.Name}\{sep[^1]}", content);
                }
            }
            else if (StorageType == StorageType.Common)
            {
                Archive archive = _adapter.AddArchive($@"C:\Backup\{rp.Name}\Archive_{RestorePoints.Count + 1}.zip");
                foreach (string file in rp.Files)
                {
                    archive.Add(_adapter.FindFile(file));
                }
            }
        }

        public override string ToString()
        {
            return $"Creation time: {CreationTime}\n" +
                   $"Restore points amount: {RestorePoints.Count}";
        }
    }
}