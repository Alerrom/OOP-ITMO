using System;
using System.Text;
using Backups.Entities.Vfs;
using Backups.Entities.Vfs.Impl;

namespace Backups.Entities.VfsAdapterSystem
{
    public class VfsAdapter : IVfsAdapter
    {
        private readonly VirtualFileSystem _virtualFileSystem;

        public VfsAdapter()
        {
            _virtualFileSystem = new VirtualFileSystem();
        }

        public Directory AddDirectory(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) != null)
                {
                    curDir = curDir.FindDir(dirName);
                }
                else
                {
                    var dir = new Directory(dirName);
                    curDir.AddDirectory(dir);
                    return dir;
                }
            }

            return null;
        }

        public File AddFile(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) != null)
                {
                    curDir = curDir.FindDir(dirName);
                }
                else
                {
                    var file = new File(dirName, path);
                    curDir.AddObject(file);
                    return file;
                }
            }

            return null;
        }

        public Archive AddArchive(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) != null)
                {
                    curDir = curDir.FindDir(dirName);
                }
                else
                {
                    var archive = new Archive(dirName, path);
                    curDir.AddArchive(archive);
                    return archive;
                }
            }

            return null;
        }

        public void AddContentOnFile(string path, string content)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) == null)
                {
                    curDir.FindObject(dirName).Write(new UTF8Encoding(true).GetBytes(content));
                    return;
                }

                curDir = curDir.FindDir(dirName);
            }
        }

        public string ReadContentOnFile(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) == null)
                {
                    var utf8 = new UTF8Encoding(true);
                    return utf8.GetString(curDir.FindObject(dirName).Read());
                }

                curDir = curDir.FindDir(dirName);
            }

            return null;
        }

        public void ClearContentOnFile(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) == null)
                {
                    curDir.FindObject(dirName).Write(Array.Empty<byte>());
                    return;
                }

                curDir = curDir.FindDir(dirName);
            }
        }

        public void DeleteFile(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) != null)
                {
                    curDir = curDir.FindDir(dirName);
                }
                else
                {
                    curDir.DeleteObject(curDir.FindObject(dirName));
                }
            }
        }

        public File FindFile(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) != null)
                {
                    curDir = curDir.FindDir(dirName);
                }
                else
                {
                    return curDir.FindObject(dirName);
                }
            }

            return null;
        }

        public void DeleteArchive(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) != null)
                {
                    curDir = curDir.FindDir(dirName);
                }
                else
                {
                    curDir.DeleteArchive(curDir.FindArchive(dirName));
                }
            }
        }

        /*
        public void ShowDir(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                curDir = curDir.FindDir(dirName);
            }

            foreach (Directory dir in curDir.ListDirectory())
            {
                Console.WriteLine(dir.Name);
            }
        }

        public void ShowContent(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                curDir = curDir.FindDir(dirName);
            }

            foreach (IStorageObject obj in curDir.ListObjects())
            {
                Console.WriteLine(obj.Name);
            }
        }
        */
    }
}