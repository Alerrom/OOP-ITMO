namespace Backups.Tools
{
    public class FileDoesNotExistException : BackupsException
    {
        public FileDoesNotExistException(string path)
            : base(path)
        { }
    }
}