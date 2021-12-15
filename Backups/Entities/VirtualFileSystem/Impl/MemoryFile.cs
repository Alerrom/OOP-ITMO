namespace Backups.Entities.VirtualFileSystem.Impl
{
    public class MemoryFile : IFile
    {
        private byte[] _contents = System.Array.Empty<byte>();
        public byte[] Read()
        {
            return _contents;
        }

        public void Write(byte[] content)
        {
            _contents = content;
        }
    }
}