using System;
using System.Collections.Generic;
using Backups.Entities;
using Backups.Entities.BackupSystem;
using Backups.Entities.RestoreObjects;
using Backups.Entities.VirtualFileSystem;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            /*
            var rep = new Repository(StorageType.Split, "C:\\Users\\Александр\\Desktop", new List<string> { "C:\\Users\\Александр\\Desktop\\a.txt", "C:\\Users\\Александр\\Desktop\\b.txt" });
            rep.AddBackupJob("C:\\Users\\Александр\\Desktop\\a.txt", "C:\\Users\\Александр\\Desktop_Backup", StorageType.Split);
            rep.CreateRestore(new RestorePointFactory());
            rep.AddFile("C:\\Users\\Александр\\Desktop\\a.txt");
            rep.AddFile("C:\\Users\\Александр\\Desktop\\b.txt");
            rep.DeleteFile("C:\\Users\\Александр\\Desktop\\b.txt");

            rep.ShowConfigurationInfo();
            */

            string filePath = new FilePathBuilder()
                .SetRoot("C")
                .SetFolder("Users")
                .SetFolder("Александр")
                .SetFolder("Desktop")
                .SetFileName("test.txt")
                .GetFilePath();
            Console.WriteLine(filePath);
        }
    }
}
