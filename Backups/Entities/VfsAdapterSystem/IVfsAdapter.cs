using Backups.Entities.Vfs.Impl;

namespace Backups.Entities.VfsAdapterSystem
{
    public interface IVfsAdapter
    {
        Directory AddDirectory(string path);
        File AddFile(string path);
        void AddContentOnFile(string path, string content);
        string ReadContentOnFile(string path);

        void ClearContentOnFile(string path);
        void DeleteFile(string path);
    }
}