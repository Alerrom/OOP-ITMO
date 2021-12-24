using Backups.Entities.Vfs.Impl;

namespace Backups.Entities.Vfs
{
    public interface IDirectory : IStorageObject
    {
        void AddObject(File obj);
        void DeleteObject(File obj);
    }
}