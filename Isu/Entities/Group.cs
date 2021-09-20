using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private List<Student> _students = new List<Student>();
        private int _maxNumberOfStudents = 25;

        private GroupName _groupName;
        internal Group(GroupName groupName)
        {
            _groupName = groupName;
        }

        public GroupName GetGroupName => _groupName;

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