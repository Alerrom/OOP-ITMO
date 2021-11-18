using Backups.Entities.CommandLineParser;

namespace Backups.Entities.Services
{
    public interface IBackupService
    {
        public void Run();
        public void CreateBackup(ParsedData parsedData);
        public void CreateRestorePoint(ParsedData parsedData);
        public void AddFile(ParsedData parsedData);
        public void DeleteFile(ParsedData parsedData);
        public void ShowInformation(ParsedData parsedData);
        public void SaveData(string dataFile);
        public BackupJob ReadData(string dataFile);
    }
}