using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private static int _counter = 0;
        private string _studentName;
        private int _id;

        public Student(string studentName)
        {
            _studentName = studentName ?? throw new IsuException("Incorrect name: null string is not allowed");
            _id = ++_counter;
        }

        public string GetStudentName => _studentName;
        public int GetStudentId => _id;
    }
}