namespace Backups.Entities.Vfs.Impl
{
    public class File : IFile
    {
        private byte[] _content = System.Array.Empty<byte>();

        public File(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public byte[] Read()
        {
            return _content;
        }

        public void Write(byte[] content)
        {
            _content = content;
        }
    }
}