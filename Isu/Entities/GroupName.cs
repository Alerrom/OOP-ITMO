using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string groupName)
        {
            if (!IsNameAllowed(groupName)) throw new InvalidGroupName();
            Name = groupName;
            Course = ToCourseNumberFromString(groupName[2]);
        }

        public string Name { get; }

        public CourseNumber Course { get; }

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