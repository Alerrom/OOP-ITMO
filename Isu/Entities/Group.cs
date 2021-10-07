using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private readonly int _maxNumberOfStudents = 25;

        internal Group(GroupName groupName)
        {
            GroupName = groupName;
        }

        public GroupName GroupName { get; }

        public List<Student> StudentsOfGroup { get; } = new List<Student>();

        public void AddStudent(Student student)
        {
            if (StudentsOfGroup.Count == _maxNumberOfStudents) throw new MaxNumberOfStudentsReachedException();

            StudentsOfGroup.Add(student);
        }

        public void DeleteStudentById(int id)
        {
            for (int index = 0; index < StudentsOfGroup.Count; index++)
            {
                Student student = StudentsOfGroup[index];
                if (student.StudentId != id) continue;
                StudentsOfGroup.RemoveAt(index);
                break;
            }
        }
    }
}