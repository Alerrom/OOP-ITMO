namespace Backups.Entities.VirtualFileSystem.Impl
{
    public class MemoryFileSystem : IFileSystem
    {
        private readonly MemoryDirectory _root = new ();
        public IDirectory GetRoot()
        {
            return _root;
        }
    }
}