using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Entities.Vfs.Impl
{
    public class Directory : IDirectory
    {
        private readonly List<File> _objects;
        private readonly List<Archive> _archives;
        private readonly List<Directory> _directories;

        public Directory(string name)
        {
            Name = name;
            _objects = new List<File>();
            _directories = new List<Directory>();
            _archives = new List<Archive>();
        }

        public string Name { get; }

        public IReadOnlyList<Directory> ListDirectory()
        {
            return _directories;
        }

        public IReadOnlyList<IStorageObject> ListObjects()
        {
            return _objects;
        }

        public IReadOnlyList<Archive> ListArchive()
        {
            return _archives;
        }

        public void AddDirectory(Directory dir)
        {
            if (CheckDirectory(dir))
                throw new DirectoryAlreadyExistsException(dir.Name);
            _directories.Add(dir);
        }

        public void AddObject(File obj)
        {
            if (CheckObject(obj))
                throw new FileAlreadyExistsException(obj.Name);
            _objects.Add(obj);
        }

        public void DeleteDirectory(Directory dir)
        {
            if (!CheckDirectory(dir))
                throw new DirectoryDoesNotExistException(dir.Name);
            _directories.Remove(dir);
        }

        public void AddArchive(Archive archive)
        {
            if (CheckArchive(archive))
                throw new ArchiveAlreadyExistsException(archive.Name);
            _archives.Add(archive);
        }

        public void DeleteArchive(Archive archive)
        {
            if (!CheckArchive(archive))
                throw new ArchiveDoesNotExistException(archive.Name);
            _archives.Remove(archive);
        }

        public void DeleteObject(File obj)
        {
            if (!CheckObject(obj))
                throw new FileDoesNotExistException(obj.Name);
            _objects.Remove(obj);
        }

        public Directory FindDir(string dirName)
        {
            return _directories.FirstOrDefault(varDir => varDir.Name == dirName);
        }

        public File FindObject(string objName)
        {
            return _objects.FirstOrDefault(varObjName => varObjName.Name == objName);
        }

        public Archive FindArchive(string archiveName)
        {
            return _archives.FirstOrDefault(varObjName => varObjName.Name == archiveName);
        }

        private bool CheckObject(IStorageObject storageObject)
        {
            return _objects.Any(obj => obj.Name == storageObject.Name);
        }

        private bool CheckDirectory(IDirectory directory)
        {
            return _objects.Any(obj => obj.Name == directory.Name);
        }

        private bool CheckArchive(IArchive archive)
        {
            return _archives.Any(obj => obj.Name == archive.Name);
        }
    }
}