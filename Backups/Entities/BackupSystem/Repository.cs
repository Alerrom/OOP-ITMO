using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Entities.RestoreObjects;
using Backups.Entities.StorageSystem;

namespace Backups.Entities.BackupSystem
{
    public class Repository
    {
        public Repository(StorageType storageType, string commonStorage, List<string> fileList)
        {
            StorageType = storageType;
            CommonStorage = commonStorage;
            Backups = new List<BackupJob>();
            Files = fileList;
            foreach (string file in fileList)
                AddBackupJob(file, new StorageFactory(storageType, file, commonStorage).GetStorage(), storageType);
        }

        public List<BackupJob> Backups { get; }
        public List<string> Files { get; }
        private StorageType StorageType { get; }
        private string CommonStorage { get; }

        public void CreateRestore(RestorePointFactory restorePointFactory) => restorePointFactory.CreateRestore(this);

        public void RemoveBackupJob(string filePath)
        {
            BackupJob backupJob = Backups.FirstOrDefault(b => b.OriginalFilePath == filePath);

            Backups.Remove(backupJob);
        }

        public void AddBackupJob(string filePath, string backupPath, StorageType storageType)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} already exist");
            }

            Backups.Add(new BackupJob(filePath, backupPath, storageType));
        }

        public void AddFile(string fileName)
        {
            Files.Add(fileName);

            Backups.Add(new BackupJob(
                fileName,
                new StorageFactory(StorageType, fileName, CommonStorage).GetStorage(),
                StorageType));

            CreateRestore(new RestorePointFactory());
        }

        public void DeleteFile(string fileName)
        {
            string file = Files.FirstOrDefault(f => f == fileName);
            Files.Remove(file);

            CreateRestore(new RestorePointFactory());
        }

        public void ShowConfigurationInfo()
        {
            Console.WriteLine("-----------------INFO---------------------");
            Console.WriteLine($"Storage Type: {StorageType}");
            Console.WriteLine($"Files for backup amount: {Backups.Count}");
            Console.WriteLine("------------------------------------------");
        }
    }
}