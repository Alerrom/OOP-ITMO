using System.Collections.Generic;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePoint : RestorePointAbstract
    {
        private static int _cnt = 0;

        public RestorePoint(string name, List<string> fileNames, StorageType type)
            : base(name)
        {
            _cnt++;
            StorageType = type;
            foreach (string fileName in fileNames)
            {
                Files.Add($"{fileName}_{_cnt}");
            }
        }

        public StorageType StorageType { get; }
        public List<string> Files { get; } = new List<string>();

        public override string ToString()
        {
            string res = string.Empty;
            return res;
        }
    }
}