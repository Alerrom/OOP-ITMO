namespace Backups.Tools;

public class FileAlreadyExistsException : BackupsException
{
    public FileAlreadyExistsException(string path)
        : base(path)
    { }
}