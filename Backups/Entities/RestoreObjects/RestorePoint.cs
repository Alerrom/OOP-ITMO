using System.Collections.Generic;
using System.Globalization;
using Backups.Entities.VfsAdapterSystem;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePoint : RestorePointAbstract
    {
        private VfsAdapter _adapter;

        public RestorePoint(string name, List<string> fileNames, StorageType type, VfsAdapter adapter)
            : base(name)
        {
            _adapter = adapter;
        }

        public override void CreateRestore()
        {
            _adapter.AddFile(@"C:\Backup\" + Name);
            string content = @"C:\Backup\" + Name;

            // _adapter.AddContentOnFile(@"C:\Backup\" + Name, content);
        }
    }
}