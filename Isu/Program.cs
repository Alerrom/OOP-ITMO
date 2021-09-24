using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;

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
            Console.WriteLine($"Student {stud1.StudentName} has id {stud1.StudentId}");
            Console.WriteLine(isu.FindStudent("Nick"));
            Student stud2 = isu.GetStudent(5);
            Console.WriteLine($"Student {stud2.StudentName} has id {stud2.StudentId}");
            Console.WriteLine("\n======  TESTS 2  ======\n");

            Group group2 = isu.AddGroup("M3100");
            isu.AddStudent(group2, "Mark");
            isu.AddStudent(group2, "Lili");
            isu.AddStudent(group2, "Petr");

            Console.WriteLine("FindStudents by course");
            List<Student> students = isu.FindStudents(CourseNumber.Second);
            foreach (Student student in students) Console.WriteLine(student.StudentName);

            Group group3 = isu.AddGroup("M3104");
            Group group4 = isu.AddGroup("M3201");

            Student s1 = isu.AddStudent(group3, "Marko");
            Student s2 = isu.AddStudent(group3, "Lilo");
            Student s3 = isu.AddStudent(group4, "Petro");

            Console.WriteLine("\n======  TESTS 3  ======\n");
            Console.WriteLine("Change group");
            Console.WriteLine(group3.GroupName.Name);
            foreach (Student student in group3.StudentsOfGroup)
            {
                Console.WriteLine(student.StudentName);
            }

            Console.WriteLine(group4.GroupName.Name);

            foreach (Student student in group4.StudentsOfGroup)
            {
                Console.WriteLine(student.StudentName);
            }

            isu.ChangeStudentGroup(s2, group4);
            Console.WriteLine(group3.GroupName.Name);

            foreach (Student student in group3.StudentsOfGroup)
            {
                Console.WriteLine(student.StudentName);
            }

            Console.WriteLine(group4.GroupName.Name);

            foreach (Student student in group4.StudentsOfGroup)
            {
                Console.WriteLine(student.StudentName);
            }
        }
    }
}
