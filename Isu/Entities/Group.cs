using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private readonly List<Student> _students = new List<Student>();
        private readonly int _maxNumberOfStudents = 25;

        internal Group(GroupName groupName)
        {
            GetGroupName = groupName;
        }

        public GroupName GetGroupName { get; }

        public List<Student> GetStudentsOfGroup => _students;

        public void AddStudent(Student student)
        {
            if (_students.Count == _maxNumberOfStudents)
                throw new IsuException("Error: Max student per group reached");

            _students.Add(student);
        }

        public bool DeleteStudentById(int id)
        {
            return _students.Where(student => student.GetStudentId == id).Select(student => _students.Remove(student)).FirstOrDefault();
        }
    }
}