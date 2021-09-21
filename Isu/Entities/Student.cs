using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private static int _counter = 0;

        public Student(string studentName)
        {
            GetStudentName = studentName ?? throw new IsuException("Incorrect name: null string is not allowed");
            GetStudentId = ++_counter;
        }

        public string GetStudentName { get; }

        public int GetStudentId { get; }
    }
}