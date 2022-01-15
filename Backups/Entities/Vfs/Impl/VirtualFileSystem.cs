namespace Backups.Entities.Vfs.Impl
{
    public class VirtualFileSystem : IFileSystem
    {
        private readonly Directory _root;

        public VirtualFileSystem()
        {
            _root = new Directory("C:");
            _root.AddDirectory(new Directory("Backup"));
        }

        public Directory GetRoot()
        {
            return _root;
        }
    }
}