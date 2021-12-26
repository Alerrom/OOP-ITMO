using System.Collections.Generic;
using Backups.Entities.StorageSystem;
using Backups.Entities.VfsAdapterSystem;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePoint : RestorePointAbstract
    {
        private readonly VfsAdapter _adapter;
        private List<IStorage> _storages = new List<IStorage>();

        public RestorePoint(string name, List<string> fileNames, StorageType type, VfsAdapter adapter)
            : base(name)
        {
            _adapter = adapter;
            StorageType = type;
            FileNames = fileNames;
        }

        public StorageType StorageType { get; }
        public List<string> FileNames { get; }

        public override void CreateRestore()
        {
            _adapter.AddFile(@"C:\Backup\" + Name);
            var storage = new StorageFactory(StorageType, FileNames);
            string content = @"C:\Backup\" + Name + "\n" + storage.GetStorage();

            _adapter.AddContentOnFile(@"C:\Backup\" + Name, content);
        }
    }
}