namespace Backups.Entities.Vfs
{
    public interface IArchive : IStorageObject
    {
        void Add(IFile file);
        void Delete(IFile file);
    }
}