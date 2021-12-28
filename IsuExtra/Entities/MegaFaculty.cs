using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        private Ognp _ognp = null;

        private List<GroupWithLessons> _megaFaculty;
        public MegaFaculty(string name)
        {
            Name = name ?? throw new IncorrectMegaFacultyNameException();
            _megaFaculty = new List<GroupWithLessons>();
        }

        public string Name { get; }

        public Ognp Ognp => _ognp;

        public void SetOgnpToMegaFaculty(Ognp ognp)
        {
            _ognp = ognp;
        }

        public GroupWithLessons FindGroup(GroupWithLessons groupWithLessons)
        {
            foreach (GroupWithLessons group in _megaFaculty)
            {
                if (group.GroupName != groupWithLessons.GroupName) continue;
                return group;
            }

            return null;
        }

        public void AddGroupToFaculty(GroupWithLessons group)
        {
            if (FindGroup(group) == null)
                throw new GroupAlreadyExistException();
            _megaFaculty.Add(group);
        }
    }
}