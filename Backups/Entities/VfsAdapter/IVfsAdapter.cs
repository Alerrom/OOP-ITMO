namespace Backups.Entities.VfsAdapter
{
    public interface IVfsAdapter
    {
        void AddDirectory(string path);
        void AddFile(string path);
        void DeleteFile(string path);
    }
}