using System.Collections.Generic;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePoint : RestorePointAbstract
    {
       // private static int _cnt = 0;
        public RestorePoint(string name, List<string> fileNames, StorageType type)
            : base(name)
        {
            // _cnt++;
            StorageType = type;
            Files = fileNames;
        }

        public StorageType StorageType { get; }
        public List<string> Files { get; }

        public override string ToString()
        {
            string res = string.Empty;
            return res;
        }
    }
}