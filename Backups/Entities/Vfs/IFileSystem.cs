using Backups.Entities.Vfs.Impl;

namespace Backups.Entities.Vfs
{
    public interface IFileSystem
    {
        Directory GetRoot();
    }
}