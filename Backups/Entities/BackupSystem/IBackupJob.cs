namespace Backups.Entities.BackupSystem
{
    public interface IBackupJob
    {
        void AddJobObject(string path);
        void DeleteJobObject(string path);
        void CreateRestorePoint();
    }
}