namespace Backups.Entities.VirtualFileSystem
{
    public interface IFileSystem
    {
        IDirectory GetRoot();
    }
}