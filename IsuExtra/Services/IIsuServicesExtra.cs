using System.Collections.Generic;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuServicesExtra
    {
        MegaFaculty AddMegaFaculty(string name);
        void AddGroupWithLessonsToMegaFaculty(GroupWithLessons group, string megaFacultyName);

        ProStudent AddStudent(string name, GroupWithLessons group);

        Ognp AddOgnp(string megaFacultyName, string ognpName, List<StudentFlow> courses);
        void RegisterStudentOnOgnp(string ognpName, ProStudent student, string flowName);
        void DeleteRegistrationOnOgnp(string ognpName, ProStudent student, string flowName);
        List<StudentFlow> FindFlowsInOgnp(string ognpName);
        List<ProStudent> FindStudentFlowsByCourseName(string ognpName, string courseName);
        List<ProStudent> FindStudentsWithoutOgnpByGroup(GroupWithLessons groupWithLessons);
    }
}