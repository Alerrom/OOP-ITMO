using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities.CommandLineParser;
using Backups.Entities.Storages;

namespace Backups.Entities
{
    [Serializable]
    public class BackupJob
    {
        public BackupJob(StorageType storageType, string commonFolder, List<string> fileList)
        {
            StorageType = storageType;
            CommonFolder = commonFolder;
            Backups = new List<Backup>();
            foreach (string file in fileList)
            {
                AddBackup(file, new Storage(storageType, file, commonFolder).GetStorage(), storageType);
            }
        }

        public List<Backup> Backups { get; }
        public StorageType StorageType { get; }
        public string CommonFolder { get; }

        public void CreateRestorePoint(BackupJob backupJob)
        {
            backupJob.CreateRestorePoint(this);
        }

        private void AddBackup(string filePath, string backupPath, StorageType storageType)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            Backups.Add(new Backup(filePath, backupPath, storageType));
        }
    }
}