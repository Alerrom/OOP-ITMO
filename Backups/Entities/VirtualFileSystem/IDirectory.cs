using System.Collections.Generic;

namespace Backups.Entities.VirtualFileSystem
{
    public interface IDirectory : IStorageObject
    {
        IReadOnlyList<IStorageObject> ListDirectory();
        void Add(IStorageObject obj);
    }
}