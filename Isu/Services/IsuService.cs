using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class IsuService : IIsuService
    {
        private Dictionary<CourseNumber, List<Group>> _data = new Dictionary<CourseNumber, List<Group>>(6);

        public IsuService()
        {
            foreach (CourseNumber courseNumber in Enum.GetValues(typeof(CourseNumber)))
            {
                _data[courseNumber] = new List<Group>();
            }
        }

        public Group AddGroup(string name)
        {
            var groupName = new GroupName(name);
            var group = new Group(groupName);
            CourseNumber courseNumber = group.GetGroupName.GetCourse;
            foreach (var g in _data[courseNumber])
            {
                if (g.GetGroupName.GetName == name) throw new IsuException("Group has already created");
            }

            _data[courseNumber].Add(group);
            return group;
        }

        public Student AddStudent(Group @group, string name)
        {
            var student = new Student(name);
            if (FindStudent(name) != null)
            {
                throw new IsuException();
            }

            CourseNumber courseNumber = group.GetGroupName.GetCourse;
            int groupIndex = _data[courseNumber].IndexOf(group);
            _data[courseNumber][groupIndex].AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (KeyValuePair<CourseNumber, List<Group>> pair in _data)
            {
                foreach (Group group in pair.Value)
                {
                    foreach (Student student in group.GetStudentsOfGroup)
                    {
                        if (student.GetStudentId == id)
                        {
                            return student;
                        }
                    }
                }
            }

            throw new IsuException();
        }

        public Student FindStudent(string name)
        {
            foreach (KeyValuePair<CourseNumber, List<Group>> pair in _data)
            {
                foreach (Group group in pair.Value)
                {
                    foreach (Student student in group.GetStudentsOfGroup)
                    {
                        if (student.GetStudentName == name)
                        {
                            return student;
                        }
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            foreach (CourseNumber courseNumber in Enum.GetValues(typeof(CourseNumber)))
            {
                foreach (Group group in _data[courseNumber])
                {
                    if (groupName.GetName == group.GetGroupName.GetName)
                        return group.GetStudentsOfGroup;
                }
            }

            return new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var answer = new List<Student>();
            foreach (Group group in _data[courseNumber])
            {
                foreach (Student student in group.GetStudentsOfGroup)
                {
                    answer.Add(student);
                }
            }

            return answer;
        }

        public Group FindGroup(GroupName groupName)
        {
            foreach (CourseNumber courseNumber in Enum.GetValues(typeof(CourseNumber)))
            {
                foreach (var group in _data[courseNumber])
                {
                    if (groupName.GetName == group.GetGroupName.GetName)
                        return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var answer = new List<Group>();
            foreach (Group group in _data[courseNumber])
            {
                answer.Add(group);
            }

            return answer;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (FindStudent(student.GetStudentName).GetStudentName != null)
            {
                foreach (CourseNumber courseNumber in Enum.GetValues(typeof(CourseNumber)))
                {
                    foreach (Group group in _data[courseNumber])
                    {
                        foreach (Student s in group.GetStudentsOfGroup)
                        {
                            if (s.GetStudentId == student.GetStudentId)
                            {
                                group.DeleteStudentById(student.GetStudentId);
                                break;
                            }
                        }
                    }
                }

                foreach (CourseNumber courseNumber in Enum.GetValues(typeof(CourseNumber)))
                {
                    foreach (Group group in _data[courseNumber])
                    {
                        if (group.GetGroupName.GetName == newGroup.GetGroupName.GetName)
                            group.AddStudent(student);
                    }
                }
            }
            else
            {
                throw new IsuException();
            }
        }
    }
}