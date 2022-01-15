using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Entities.Vfs.Impl
{
    public class Archive : IArchive
    {
        private readonly List<IFile> _files;

        public Archive(string name, string fullPath)
        {
            Name = name;
            FullPath = fullPath;
            _files = new List<IFile>();
        }

        public string Name { get; }
        public string FullPath { get; }

        public void Add(IFile file)
        {
            if (CheckFile(file))
                throw new FileAlreadyExistsException(file.Name);
            _files.Add(file);
        }

        public void Delete(IFile file)
        {
            if (!CheckFile(file))
                throw new FileDoesNotExistException(file.Name);
            _files.Remove(file);
        }

        private bool CheckFile(IFile file)
        {
            return _files.Any(varFile => varFile.Name == file.Name);
        }
    }
}