using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        private const int MAXLENGTHOFGROUPNAME = 5;
        private const char MINCOURSENUMBER = '1';
        private const char MAXCOURSENUMBER = '4';

        public GroupName(string groupName)
        {
            if (!IsNameAllowed(groupName)) throw new InvalidGroupNameException();
            Name = groupName;
            Course = ToCourseNumberFromString(groupName[2]);
        }

        public string Name { get; }

        public CourseNumber Course { get; }

        private static bool IsNameAllowed(string name)
        {
            return name.Length == MAXLENGTHOFGROUPNAME && !(name[2] < MINCOURSENUMBER || name[2] > MAXCOURSENUMBER);
        }

        private CourseNumber ToCourseNumberFromString(char c)
        {
            return (CourseNumber)(c - '0');
        }
    }
}