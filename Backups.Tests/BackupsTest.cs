using System.Collections.Generic;
using Backups.Entities;
using Backups.Entities.BackupSystem;
using Backups.Entities.VfsAdapterSystem;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private readonly VfsAdapter _adapter = new VfsAdapter();
        private BackupJob _backupJob;
        [SetUp]
        public void Setup()
        {
            _adapter.AddDirectory(@"C:\programming");
            _adapter.AddDirectory(@"C:\programming\OOP");
            _adapter.AddDirectory(@"C:\programming\Aleksandr");

            _adapter.AddFile(@"C:\programming\OOP\a.txt");
            _adapter.AddFile(@"C:\programming\OOP\b.txt");
            _adapter.AddFile(@"C:\programming\OOP\c.txt");

            _adapter.AddFile(@"C:\programming\Aleksandr\cat.png");
            _adapter.AddFile(@"C:\programming\Aleksandr\dog.png");

            _backupJob = new BackupJob(_adapter, StorageType.Common);
        }

        [Test]
        public void Test1()
        {
            _backupJob.AddJobObject(@"C:\programming\Aleksandr\cat.png");
            _backupJob.AddJobObject(@"C:\programming\Aleksandr\dog.png");
            var obj = new List<string>()
            {
                @"C:\programming\Aleksandr\cat.png",
                @"C:\programming\Aleksandr\dog.png"
            };
            
            Assert.AreEqual(StorageType.Common, _backupJob.StorageType);
            Assert.AreEqual(obj, _backupJob.JobObjects);
        }
    }
}