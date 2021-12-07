using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Backups.Abstractions;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePoint : RestorePointAbstract
    {
        private List<string> _fileNames;
        public RestorePoint(string fullPath, string originalFilePath, List<string> fileNames)
            : base(fullPath, originalFilePath)
        {
            _fileNames = fileNames;
        }

        public override void CreateRestore()
        {
            File.Create(FullPath).Close();
            File.WriteAllLines(
                FullPath,
                new[] { "Restore point\n", CreationTime.ToString(CultureInfo.CurrentCulture), Name, DirectoryName, FullPath, OriginalFilePath });

            foreach (string fileName in _fileNames)
            {
                File.AppendAllText(FullPath, "\n" + fileName);
            }
        }
    }
}