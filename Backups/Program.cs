using System;
using System.Collections.Generic;
using Backups.Entities;
using Backups.Entities.BackupSystem;
using Backups.Entities.RestoreObjects;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var rep = new Repository(StorageType.Split, "C:\\Users\\Александр\\Desktop", new List<string> { "C:\\Users\\Александр\\Desktop\\a.txt", "C:\\Users\\Александр\\Desktop\\b.txt" });
            rep.AddBackupJob("C:\\Users\\Александр\\Desktop\\a.txt", "C:\\Users\\Александр\\Desktop_Backup", StorageType.Split);
            rep.CreateRestore(new RestorePointFactory());
            rep.AddFile("C:\\Users\\Александр\\Desktop\\a.txt");
            rep.AddFile("C:\\Users\\Александр\\Desktop\\b.txt");
            rep.CreateRestore(new RestorePointFactory());

            rep.ShowConfigurationInfo();

            rep.Backups[0].GetInfo();
            Console.WriteLine();
            Console.WriteLine(rep.Backups[1].RestorePointsCount);

            rep.Backups[0].GetInfo();
            Console.WriteLine();
            Console.WriteLine(rep.Backups[2].RestorePointsCount);
        }
    }
}
