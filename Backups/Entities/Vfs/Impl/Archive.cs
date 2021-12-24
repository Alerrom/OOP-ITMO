using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Entities.Vfs.Impl
{
    public class Archive : IArchive
    {
        private readonly List<IFile> _files;

        public Archive(string name)
        {
            Name = name;
            _files = new List<IFile>();
        }

        public string Name { get; }

        public void Add(IFile file)
        {
            if (CheckFile(file))
                throw new Exception();
            _files.Add(file);
        }

        public void Delete(IFile file)
        {
            if (!CheckFile(file))
                throw new Exception();
            _files.Remove(file);
        }

        private bool CheckFile(IFile file)
        {
            return _files.Any(varFile => varFile.Name == file.Name);
        }
    }
}