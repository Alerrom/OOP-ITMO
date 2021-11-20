using System.Globalization;
using System.IO;
using Backups.Abstractions;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePoint : RestorePointAbstract
    {
        public RestorePoint(string fullPath, string originalFilePath)
            : base(fullPath, originalFilePath) { }

        public override void CreateRestore()
        {
            File.Create(FullPath).Close();
            File.WriteAllLines(
                FullPath,
                new[] { "Restore point\n", CreationTime.ToString(CultureInfo.CurrentCulture), Name, DirectoryName, FullPath, OriginalFilePath });
        }
    }
}