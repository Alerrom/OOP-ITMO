using System;

namespace Isu.Entities
{
    public static class Course
    {
        public static Array AllCourses()
        {
            return Enum.GetValues(typeof(CourseNumber));
        }
    }
}