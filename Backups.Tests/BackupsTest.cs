using System.Collections.Generic;
using Backups.Entities;
using Backups.Entities.BackupSystem;
using Backups.Entities.VfsAdapterSystem;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private VfsAdapter _adapter;
        private BackupJob _backupJob;
        [SetUp]
        public void Setup()
        {
            _adapter = new VfsAdapter();
            _backupJob = new BackupJob(_adapter, StorageType.Split);
        }

        [Test]
        public void CreateRestorePointSplit_ImplementVfsThenChangeJobObjects_RestorePointsCreated()
        {
            _adapter.AddDirectory(@"C:\programming");
            _adapter.AddDirectory(@"C:\programming\OOP");
            _adapter.AddDirectory(@"C:\programming\Aleksandr");

            _adapter.AddFile(@"C:\programming\OOP\a.txt");
            _adapter.AddFile(@"C:\programming\OOP\b.txt");
            _adapter.AddFile(@"C:\programming\OOP\c.txt");

            _adapter.AddFile(@"C:\programming\Aleksandr\cat.png");
            _adapter.AddFile(@"C:\programming\Aleksandr\dog.png");
            
            _backupJob.AddJobObject(@"C:\programming\Aleksandr\cat.png");
            _backupJob.AddJobObject(@"C:\programming\Aleksandr\dog.png");
            var obj = new List<string>()
            {
                @"C:\programming\Aleksandr\cat.png",
                @"C:\programming\Aleksandr\dog.png"
            };
            
            Assert.AreEqual(StorageType.Split, _backupJob.StorageType);
            Assert.AreEqual(obj, _backupJob.JobObjects);
            
            _backupJob.CreateRestorePoint();
            Assert.AreEqual(1,_backupJob.RestorePoints.Count);
            Assert.AreEqual(2,_backupJob.RestorePoints[0].Files.Count);
            _backupJob.DeleteJobObject(@"C:\programming\Aleksandr\cat.png");
            Assert.AreEqual(2,_backupJob.RestorePoints.Count);
            Assert.AreEqual(1,_backupJob.RestorePoints[1].Files.Count);
        }
        
        [Test]
        public void CreateRestorePointCommon_ImplementVfsThenChangeJobObjects_RestorePointsCreated()
        {
            _adapter.AddDirectory(@"C:\programming");
            _adapter.AddDirectory(@"C:\programming\OOP");
            _adapter.AddDirectory(@"C:\programming\Aleksandr");

            _adapter.AddFile(@"C:\programming\OOP\a.txt");
            _adapter.AddFile(@"C:\programming\OOP\b.txt");
            _adapter.AddFile(@"C:\programming\OOP\c.txt");

            _adapter.AddFile(@"C:\programming\Aleksandr\cat.png");
            _adapter.AddFile(@"C:\programming\Aleksandr\dog.png");
            
            _backupJob.ChangeStorageType(StorageType.Common);
            _backupJob.AddJobObject(@"C:\programming\OOP\a.txt");
            _backupJob.AddJobObject(@"C:\programming\OOP\b.txt");
            var obj = new List<string>()
            {
                @"C:\programming\OOP\a.txt",
                @"C:\programming\OOP\b.txt"
            };
            
            Assert.AreEqual(StorageType.Common, _backupJob.StorageType);
            Assert.AreEqual(obj, _backupJob.JobObjects);
            
            _backupJob.CreateRestorePoint();
            Assert.AreEqual(1,_backupJob.RestorePoints.Count);
            Assert.AreEqual(2,_backupJob.RestorePoints[0].Files.Count);
        }
    }
}