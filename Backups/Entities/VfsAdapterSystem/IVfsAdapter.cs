namespace Backups.Entities.VfsAdapterSystem
{
    public interface IVfsAdapter
    {
        void AddDirectory(string path);
        void AddFile(string path);
        void AddContentOnFile(string path, string content);
        void ClearContentOnFile(string path);
        void DeleteFile(string path);
    }
}