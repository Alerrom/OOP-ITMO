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

        public void AddDirectory(string path)
        {
            string[] sep = path.Split(@"\");
            Directory curDir = _virtualFileSystem.GetRoot();
            foreach (string dirName in sep)
            {
                if (dirName == "C:") continue;
                if (curDir.FindDir(dirName) == null)
                {
                    curDir.AddDirectory(new Directory(dirName));
                }
                else
                {
                    curDir = curDir.FindDir(dirName);
                }
            }
        }

        public void AddFile(string path)
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
                    curDir.AddObject(new File(dirName));
                }
            }
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
    }
}