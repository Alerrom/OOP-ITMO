using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class StudyDay
    {
        public StudyDay(List<Lesson> lessons, WeekDay weekDay)
        {
            WeekDay = weekDay;
            Lessons = lessons;
        }

        public List<Lesson> Lessons { get; }
        public WeekDay WeekDay { get; }
    }
}