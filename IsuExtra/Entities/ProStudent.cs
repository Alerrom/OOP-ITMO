using Isu.Entities;

namespace IsuExtra.Entities
{
    public class ProStudent : Student
    {
        private bool _hasOgnpStatus;

        private Timetable _timetable;
        public ProStudent(string name)
            : base(name)
        {
            _hasOgnpStatus = false;
        }

        public bool HasOgnp => _hasOgnpStatus;
        public Timetable Timetable => _timetable;

        public void AddGroupTimetable(Timetable timetable)
        {
            _timetable = timetable;
        }

        public void ChangeOgnpStatus()
        {
            _hasOgnpStatus = !_hasOgnpStatus;
        }
    }
}