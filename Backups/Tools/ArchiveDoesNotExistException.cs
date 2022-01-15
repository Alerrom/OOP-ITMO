namespace Backups.Tools
{
    public class ArchiveDoesNotExistException : BackupsException
    {
        public ArchiveDoesNotExistException(string name)
            : base(name)
        {
        }
    }
}