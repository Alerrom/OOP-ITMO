using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Timetable
    {
        private List<StudyDay> _timetable = new List<StudyDay>() { null, null, null, null, null, null, null, null };
        public Timetable(List<StudyDay> studyDays)
        {
            foreach (StudyDay studyDay in studyDays)
            {
                if (_timetable[(int)studyDay.WeekDay] != null)
                    throw new IncorrectTimetableException();

                _timetable[(int)studyDay.WeekDay] = studyDay;
            }
        }

        public StudyDay GetStudyDay(int i)
        {
            return _timetable[i];
        }
    }
}