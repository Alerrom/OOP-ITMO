using System;

namespace Backups.Entities.RestoreObjects
{
    public abstract class RestorePointAbstract
    {
        protected RestorePointAbstract(string name)
        {
            CreationTime = DateTime.Now;
            Name = name;
        }

        public DateTime CreationTime { get; }
        public string Name { get; }
    }
}