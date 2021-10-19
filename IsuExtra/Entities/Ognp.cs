using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class Ognp
    {
        private List<StudentFlow> _courses;

        public Ognp(string name, List<StudentFlow> courses)
        {
            Name = name;
            _courses = courses;
        }

        public List<StudentFlow> Courses => _courses;
        public string Name { get; }
    }
}