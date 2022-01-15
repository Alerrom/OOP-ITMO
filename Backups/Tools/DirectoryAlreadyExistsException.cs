namespace Backups.Tools
{
    public class DirectoryAlreadyExistsException : BackupsException
    {
        public DirectoryAlreadyExistsException(string path)
            : base(path)
        { }
    }
}