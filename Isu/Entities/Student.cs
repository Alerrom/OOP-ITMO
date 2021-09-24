using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private static int _counter = 0;

        public Student(string studentName)
        {
            StudentName = studentName ?? throw new IsuException("Incorrect name: null string is not allowed");
            StudentId = ++_counter;
        }

        public string StudentName { get; }

        public int StudentId { get; }
    }
}