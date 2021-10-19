using System.Collections.Generic;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuServicesExtra
    {
        public MegaFaculty AddMegaFaculty(string name);
        public void AddGroupWithLessonsToMegaFaculty(GroupWithLessons group, string megaFacultyName);

        public ProStudent AddStudent(string name, GroupWithLessons group);

        public Ognp AddOgnp(string megaFacultyName, string ognpName, List<StudentFlow> courses);
        public void RegisterStudentOnOgnp(string ognpName, ProStudent student, string flowName);
        public void DeleteRegistrationOnOgnp(string ognpName, ProStudent student, string flowName);
        public List<StudentFlow> FindFlowsInOgnp(string ognpName);
        public List<ProStudent> FindStudentFlowsByCourseName(string ognpName, string courseName);
        public List<ProStudent> FindStudentsWithoutOgnpByGroup(GroupWithLessons groupWithLessons);
    }
}