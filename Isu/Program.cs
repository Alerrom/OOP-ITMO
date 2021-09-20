using System;
using System.Collections.Generic;
using Isu.Entities;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            var isu = new IsuService();

            Group group1 = isu.AddGroup("M3204");
            isu.AddStudent(group1, "Alex");
            isu.AddStudent(group1, "Bob");
            isu.AddStudent(group1, "Mike");
            isu.AddStudent(group1, "Roman");
            isu.AddStudent(group1, "Victor");

            Console.WriteLine("\n======  TESTS 1  ======\n");
            Student stud1 = isu.GetStudent(2);
            Console.WriteLine("FindStudent by Id");
            Console.WriteLine($"Student {stud1.GetStudentName} has id {stud1.GetStudentId}");
            Console.WriteLine(isu.FindStudent("Nick"));
            Student stud2 = isu.GetStudent(5);
            Console.WriteLine($"Student {stud2.GetStudentName} has id {stud2.GetStudentId}");
            Console.WriteLine("\n======  TESTS 2  ======\n");

            Group group2 = isu.AddGroup("M3100");
            isu.AddStudent(group2, "Mark");
            isu.AddStudent(group2, "Lili");
            isu.AddStudent(group2, "Petr");

            Console.WriteLine("FindStudents by course");
            List<Student> students = isu.FindStudents(CourseNumber.Second);
            foreach (Student student in students)
            {
                Console.WriteLine(student.GetStudentName);
            }

            Console.WriteLine("\n======  TESTS 3  ======\n");
            Group group3 = isu.AddGroup("M3104");
            Group group4 = isu.AddGroup("M3201");

            Student s1 = isu.AddStudent(group3, "Marko");
            Student s2 = isu.AddStudent(group3, "Lilo");
            Student s3 = isu.AddStudent(group4, "Petro");

            Console.WriteLine("FindStudents by groupName");

            students = isu.FindStudents(group1.GetGroupName);
            foreach (var student in students) Console.WriteLine(student.GetStudentName);

            Console.WriteLine("\n======  TESTS 4  ======\n");
            Console.WriteLine("FindStudents by course after adding new group");

            students = isu.FindStudents(CourseNumber.First);
            foreach (Student student in students)
            {
                Console.WriteLine(student.GetStudentName);
            }

            Console.WriteLine("\n======  TESTS 5 ======\n");
            Console.WriteLine("FindGroup by groupName");

            Group findGroup = isu.FindGroup(group3.GetGroupName);
            Console.WriteLine(findGroup.GetGroupName.GetName);

            Console.WriteLine("\n======  TESTS 6  ======\n");
            Console.WriteLine("FindGroups by courseNumber");

            List<Group> findGroups = isu.FindGroups(CourseNumber.Fifth);
            foreach (Group group in findGroups)
            {
                Console.WriteLine(group.GetGroupName.GetName);
            }

            Console.WriteLine("\n======  TESTS 7  ======\n");
            Console.WriteLine("FindGroups by courseNumber");
            Console.WriteLine(group3.GetGroupName.GetName);
            foreach (Student student in group3.GetStudentsOfGroup)
            {
                Console.WriteLine(student.GetStudentName);
            }

            Console.WriteLine(group4.GetGroupName.GetName);

            foreach (Student student in group4.GetStudentsOfGroup)
            {
                Console.WriteLine(student.GetStudentName);
            }

            isu.ChangeStudentGroup(s2, group4);
            Console.WriteLine(group3.GetGroupName.GetName);

            foreach (Student student in group3.GetStudentsOfGroup)
            {
                Console.WriteLine(student.GetStudentName);
            }

            Console.WriteLine(group4.GetGroupName.GetName);

            foreach (Student student in group4.GetStudentsOfGroup)
            {
                Console.WriteLine(student.GetStudentName);
            }
        }
    }
}
