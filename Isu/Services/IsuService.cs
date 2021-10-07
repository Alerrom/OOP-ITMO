using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly Dictionary<CourseNumber, List<Group>> _data = new Dictionary<CourseNumber, List<Group>>(6);

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
            if (FindGroup(groupName) != null)
                throw new InvalidGroupNameException();

            var group = new Group(groupName);
            _data[group.GroupName.Course].Add(group);
            return group;
        }

        public Student AddStudent(Group @group, string name)
        {
            var student = new Student(name);
            if (FindStudent(name) != null)
            {
                throw new IsuException();
            }

            int groupIndex = _data[group.GroupName.Course].IndexOf(group);
            _data[group.GroupName.Course][groupIndex].AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (CourseNumber courseNumber in Course.AllCourses())
            {
                foreach (Group group in _data[courseNumber])
                {
                    foreach (Student student in group.StudentsOfGroup)
                    {
                        if (student.StudentId == id)
                        {
                            return student;
                        }
                    }
                }
            }

            throw new InvalidStudentIdException();
        }

        public Student FindStudent(string name)
        {
            foreach (CourseNumber courseNumber in Course.AllCourses())
            {
                foreach (Group group in _data[courseNumber])
                {
                    foreach (Student student in group.StudentsOfGroup)
                    {
                        if (student.StudentName == name)
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
            foreach (CourseNumber courseNumber in Course.AllCourses())
            {
                foreach (Group group in _data[courseNumber])
                {
                    if (groupName.Name == group.GroupName.Name)
                        return group.StudentsOfGroup;
                }
            }

            return new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var answer = new List<Student>();

            foreach (Group group in _data[courseNumber])
            {
                Console.WriteLine(group.GroupName.Name);
                foreach (Student student in group.StudentsOfGroup)
                {
                    answer.Add(student);
                }
            }

            return answer;
        }

        public Group FindGroup(GroupName groupName)
        {
            foreach (CourseNumber courseNumber in Course.AllCourses())
            {
                foreach (Group group in _data[courseNumber])
                {
                    if (groupName.Name == group.GroupName.Name)
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
            if (FindStudent(student.StudentName).StudentName == null)
                throw new InvalidStudentException();

            foreach (CourseNumber courseNumber in Course.AllCourses())
            {
                foreach (Group group in _data[courseNumber])
                {
                    foreach (Student s in group.StudentsOfGroup)
                    {
                        if (s.StudentId == student.StudentId)
                        {
                            group.DeleteStudentById(student.StudentId);
                            break;
                        }
                    }
                }
            }

            newGroup.AddStudent(student);
        }
    }
}