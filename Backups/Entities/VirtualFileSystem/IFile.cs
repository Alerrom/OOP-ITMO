namespace Backups.Entities.VirtualFileSystem
{
    public interface IFile : IStorageObject
    {
        byte[] Read();
        void Write(byte[] content);
    }
}