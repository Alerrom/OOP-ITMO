using Backups.Entities.BackupSystem;

namespace Backups.Abstractions
{
    public interface IRestore
    {
        void CreateRestore(Repository repository);
    }
}