using Backups.Entities.CommandLineParser;

namespace Backups.Abstractions
{
    public interface IParseable
    {
        public ParsedData Parse();
    }
}