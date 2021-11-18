using System.Collections.Generic;
using Backups.Entities.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private BackupService _backupService;
        
        [SetUp]
        public void Setup()
        {
            _backupService = new BackupService(new List<string>() 
            {
                "-n", 
                "-s", 
                "C:\\Users\\Александр\\Desktop\\a.txt", 
                "C:\\Users\\Александр\\Desktop\\b.txt", 
                "-df", 
                "backup", 
                "-c", 
                "3"
            });
        }
        
        [Test]
        public void RunBackupSistem_AllActionsForTest1_SystemCreated()
        {
            _backupService.Run();
            
            DirectoryAssert.Exists("C:\\Users\\annchous\\Desktop\\a.txt_Backup");
            DirectoryAssert.Exists("C:\\Users\\annchous\\Desktop\\b.txt_Backup");
            
            FileAssert.Exists("C:\\Users\\annchous\\Desktop\\a.txt_Backup\\RestorePoint_0");
            FileAssert.Exists("C:\\Users\\annchous\\Desktop\\b.txt_Backup\\RestorePoint_0");
        }
    }
}