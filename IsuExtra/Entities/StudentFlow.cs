using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class StudentFlow
    {
        private readonly int _maxcapacity = 45;

        private List<ProStudent> _students;

        public StudentFlow(string name, Timetable timetable)
        {
            Name = name;
            Timetable = timetable;
            _students = new List<ProStudent>();
        }

        public string Name { get; }
        public Timetable Timetable { get; }
        public List<ProStudent> Students => _students;

        public void AddStudent(ProStudent student)
        {
            if (_students.Count > _maxcapacity)
                throw new IsuExtraException();
            _students.Add(student);
            student.ChangeOgnpStatus();
        }

        public void DeleteStudent(ProStudent student)
        {
            for (int index = 0; index < _students.Count; index++)
            {
                if (_students[index].StudentId != student.StudentId) continue;
                _students[index].ChangeOgnpStatus();
                _students.RemoveAt(index);
                return;
            }

            throw new CanNotDeleteStudentFromFlowException();
        }
    }
}