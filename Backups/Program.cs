using System;
using Backups.Entities;
using Backups.Entities.BackupSystem;
using Backups.Entities.VfsAdapterSystem;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var adapter = new VfsAdapter();
            adapter.AddDirectory(@"C:\programming");
            adapter.AddDirectory(@"C:\programming\OOP");
            adapter.AddDirectory(@"C:\programming\Aleksandr");

            adapter.AddFile(@"C:\programming\OOP\a.txt");
            adapter.AddFile(@"C:\programming\OOP\b.txt");
            adapter.AddFile(@"C:\programming\OOP\c.txt");

            adapter.AddFile(@"C:\programming\Aleksandr\cat.png");
            adapter.AddFile(@"C:\programming\Aleksandr\dog.png");

            adapter.AddContentOnFile(@"C:\programming\OOP\c.txt", "pups");
            adapter.AddContentOnFile(@"C:\programming\Aleksandr\dog.png", "pups");
            adapter.ClearContentOnFile(@"C:\programming\Aleksandr\dog.png");
            adapter.AddContentOnFile(@"C:\programming\Aleksandr\dog.png", "pups");
            adapter.AddContentOnFile(@"C:\programming\Aleksandr\dog.png", "dogs");

            Console.WriteLine(adapter.ReadContentOnFile(@"C:\programming\OOP\c.txt"));
            Console.WriteLine(adapter.ReadContentOnFile(@"C:\programming\Aleksandr\dog.png"));

            var backupJob = new BackupJob(adapter, StorageType.Common);
            backupJob.AddJobObject(@"C:\programming\Aleksandr\dog.png");
            backupJob.AddJobObject(@"C:\programming\Aleksandr\cat.png");
            backupJob.CreateRestorePoint();
            adapter.ShowDir(@"C:\Backup");
        }
    }
}
