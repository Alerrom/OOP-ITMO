namespace Backups.Tools
{
    public class DataFileAlreadyExistsException : BackupsException
    {
        public DataFileAlreadyExistsException(string message)
            : base(message) { }
    }
}