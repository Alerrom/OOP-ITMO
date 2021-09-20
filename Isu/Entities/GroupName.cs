using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        private string _groupName;
        private CourseNumber _courseNumber;

        public GroupName(string groupName)
        {
            if (!IsNameAllowed(groupName)) throw new IsuException(nameof(groupName));
            _groupName = groupName;
            _courseNumber = ToCourseNumberFromString(groupName[2]);
        }

        public string GetName => _groupName;
        public CourseNumber GetCourse => _courseNumber;

        private static bool IsNameAllowed(string name)
        {
            return name.Length == 5 && name[0] == 'M' &&
                   name[1] == '3' && !(name[2] < '1' || name[2] > '4');
        }

        private CourseNumber ToCourseNumberFromString(char c)
        {
            return (CourseNumber)(int)(c - '0');
        }
    }
}