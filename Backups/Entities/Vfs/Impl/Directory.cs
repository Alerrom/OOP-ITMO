using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Entities.Vfs.Impl
{
    public class Directory : IDirectory
    {
        private readonly List<IStorageObject> _objects;
        private readonly List<Directory> _directories;

        public Directory(string name)
        {
            Name = name;
            _objects = new List<IStorageObject>();
            _directories = new List<Directory>();
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

        public void AddDirectory(Directory dir)
        {
            if (CheckDirectory(dir))
                throw new DirectoryAlreadyExistsException(dir.Name);
            _directories.Add(dir);
        }

        public void AddObject(IStorageObject obj)
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

        public void DeleteObject(IStorageObject obj)
        {
            if (!CheckObject(obj))
                throw new FileDoesNotExistException(obj.Name);
            _objects.Remove(obj);
        }

        public Directory FindDir(string dirName)
        {
            return _directories.FirstOrDefault(varDir => varDir.Name == dirName);
        }

        public IStorageObject FindObject(string objName)
        {
            return _objects.FirstOrDefault(varObjName => varObjName.Name == objName);
        }

        private bool CheckObject(IStorageObject storageObject)
        {
            return _objects.Any(obj => obj.Name == storageObject.Name);
        }

        private bool CheckDirectory(IDirectory directory)
        {
            return _objects.Any(obj => obj.Name == directory.Name);
        }
    }
}