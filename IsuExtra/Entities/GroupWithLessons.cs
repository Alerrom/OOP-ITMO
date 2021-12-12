using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class GroupWithLessons
    {
        private readonly int _maxNumberOfStudents = 25;
        private readonly List<ProStudent> _students;
        private Timetable _timetable;

        public GroupWithLessons(GroupName groupName)
        {
            GroupName = groupName;
            _timetable = null;
            _students = new List<ProStudent>();
        }

        public GroupName GroupName { get; }

        public IReadOnlyCollection<ProStudent> StudentsOfGroup => _students;
        public Timetable Timetable => _timetable;

        public void AddStudent(ProStudent student)
        {
            if (StudentsOfGroup.Count == _maxNumberOfStudents) throw new MaxNumberOfStudentsReachedException();

            _students.Add(student);
            student.AddGroupTimetable(_timetable);
        }

        public void DeleteStudentById(int id)
        {
            for (int index = 0; index < _students.Count; index++)
            {
                Student student = _students[index];
                if (student.StudentId != id) continue;

                _students.RemoveAt(index);
                break;
            }
        }

        public void SetTimetable(Timetable timetable)
        {
            _timetable = timetable;
        }
    }
}