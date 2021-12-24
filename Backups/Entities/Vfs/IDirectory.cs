namespace Backups.Entities.Vfs
{
    public interface IDirectory : IStorageObject
    {
        void AddObject(IStorageObject obj);
        void DeleteObject(IStorageObject obj);
    }
}