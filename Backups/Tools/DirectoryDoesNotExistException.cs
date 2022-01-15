namespace Backups.Tools
{
    public class DirectoryDoesNotExistException : BackupsException
    {
        public DirectoryDoesNotExistException(string path)
            : base(path)
        { }
    }
}