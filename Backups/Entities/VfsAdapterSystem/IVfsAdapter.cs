namespace Backups.Entities.VfsAdapterSystem
{
    public interface IVfsAdapter
    {
        void AddDirectory(string path);
        void AddFile(string path);
        void AddContentOnFile(string path, string content);
        string ReadContentOnFile(string path);

        void ClearContentOnFile(string path);
        void DeleteFile(string path);
    }
}