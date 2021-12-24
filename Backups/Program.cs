using Backups.Entities.VfsAdapter;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var adapter = new VfsAdapter();
            adapter.AddDirectory(@"C:\programming");
            adapter.AddDirectory(@"C:\programming\OOP");
            adapter.AddDirectory(@"C:\programming\Aleksandr");

            adapter.AddFile(@"C:\programming\OOP\a.txt");
            adapter.AddFile(@"C:\programming\OOP\b.txt");
            adapter.AddFile(@"C:\programming\OOP\c.txt");

            adapter.AddFile(@"C:\programming\Aleksandr\cat.png");
            adapter.AddFile(@"C:\programming\Aleksandr\dog.png");

            adapter.ShowContent(@"C:\programming\OOP");
            adapter.DeleteFile(@"C:\programming\OOP\b.txt");
            adapter.ShowContent(@"C:\programming\OOP");
        }
    }
}
