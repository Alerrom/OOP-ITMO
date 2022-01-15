namespace Backups.Entities.Vfs
{
    public interface IFile : IStorageObject
    {
        byte[] Read();
        void Write(byte[] content);
    }
}