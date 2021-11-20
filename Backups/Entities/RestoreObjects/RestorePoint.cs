using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Backups.Abstractions;

namespace Backups.Entities.RestoreObjects
{
    public class RestorePoint : RestorePointAbstract
    {
        public RestorePoint(string fullPath, string originalFilePath, List<string> fileNames)
            : base(fullPath, originalFilePath)
        {
            FileNames = fileNames;
        }

        private List<string> FileNames { get; }

        public override void CreateRestore()
        {
            File.Create(FullPath).Close();
            File.WriteAllLines(
                FullPath,
                new[] { "Restore point\n", CreationTime.ToString(CultureInfo.CurrentCulture), Name, DirectoryName, FullPath, OriginalFilePath });

            foreach (string fileName in FileNames)
            {
                File.AppendAllText(FullPath, "\n" + fileName);
            }
        }
    }
}