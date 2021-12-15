using System.Collections.Generic;

namespace Backups.Entities.VirtualFileSystem.Impl
{
    public class MemoryDirectory : IDirectory
    {
        private readonly List<IStorageObject> _objects = new ();

        public IReadOnlyList<IStorageObject> ListDirectory()
        {
            return _objects;
        }

        public void Add(IStorageObject obj)
        {
            _objects.Add(obj);
        }
    }
}