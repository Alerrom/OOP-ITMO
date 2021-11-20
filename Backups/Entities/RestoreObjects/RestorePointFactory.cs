using Backups.Abstractions;
using Backups.Entities.BackupSystem;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePointFactory : IRestore
    {
        public void CreateRestore(Repository repository)
        {
            foreach (BackupJob bj in repository.Backups)
            {
                var newPoint = new RestorePoint(
                    $"{bj.FullPath}\\RestorePoint_{bj.RestorePointsCount++}",
                    bj.OriginalFilePath);
                newPoint.CreateRestore();

                bj.CreateRestorePoint(newPoint);
            }
        }
    }
}