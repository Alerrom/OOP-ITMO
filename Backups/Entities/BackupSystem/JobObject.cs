namespace Backups.Entities.BackupSystem
{
    public class JobObject
    {
        public JobObject(string fileName)
        {
            FileName = fileName;
        }

        private string FileName { get; }
    }
}