namespace Backups.Tools
{
    public class ArchiveAlreadyExistsException : BackupsException
    {
        public ArchiveAlreadyExistsException(string name)
            : base(name)
        {
        }
    }
}