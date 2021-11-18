using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Backups.Entities.CommandLineParser;
using Backups.Entities.Storages;
using Backups.Tools;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace Backups.Entities.Services
{
    public class BackupService
    {
        private readonly List<string> _args;
        private readonly BinaryFormatter _formatter;
        private string _dataFile;
        private BackupJob _backupSystem;

        public BackupService(List<string> args)
        {
            _args = args;
            _formatter = new BinaryFormatter();
        }

        public void Run()
        {
            ParsedData parsedData = new Parser(_args).Parse();
            switch (parsedData.ActionType)
            {
                case ActionType.CreateBackupJob:
                    CreateBackup(parsedData);
                    break;
                case ActionType.CreateRestorePoint:
                    CreateRestorePoint(parsedData);
                    break;
                case ActionType.AddBackup:
                    AddFile(parsedData);
                    break;
                case ActionType.Info:
                    ShowConfigurationInfo(parsedData);
                    break;
                default:
                    throw new ArgumentException(_args[0]);
            }

            SaveData(_dataFile);
        }

        private void CreateBackup(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            if (File.Exists(_dataFile + ".dat"))
            {
                var exception = new DataFileAlreadyExistsException(_dataFile + ".dat");
                throw exception;
            }

            File.Create(_dataFile + ".dat").Close();

            _backupSystem = parsedData.BackupSystem;
            _backupSystem.CreateRestorePoint(new RestoreFactory(RestoreType.Full));
        }

        private void CreateRestorePoint(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            _backupSystem.CreateRestorePoint(new RestorePoint(parsedData.RestoreType));
        }

        private void DeleteFile(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            _backupSystem.Backups.Remove(_backupSystem.Backups
                .FirstOrDefault(x => x.OriginalFilePath == parsedData.FilePath));
        }

        private void AddFile(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            _backupSystem.Backups.Add(new Backup(
                parsedData.FilePath,
                new Storage(_backupSystem.StorageType, parsedData.FilePath, _backupSystem.CommonFolder).GetStorage(),
                _backupSystem.StorageType));
        }

        private void ShowConfigurationInfo(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            Console.WriteLine();
            Console.WriteLine($"Storage Type: {_backupSystem.StorageType}");
            Console.WriteLine($"Files for backup amount: {_backupSystem.Backups.Count}");
            Console.WriteLine();
        }

        private void SaveData(string dataFile)
        {
            var fs = new FileStream(dataFile + ".dat", FileMode.OpenOrCreate);
            _formatter.Serialize(fs, _backupSystem);
        }

        private BackupJob ReadData(string dataFile)
        {
            if (!File.Exists(dataFile + ".dat"))
                throw new FileNotFoundException();

            var fs = new FileStream(dataFile + ".dat", FileMode.OpenOrCreate);
            return (BackupJob)_formatter.Deserialize(fs);
        }
    }
}