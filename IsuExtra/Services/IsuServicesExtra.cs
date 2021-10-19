using System.Collections.Generic;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuServicesExtra : IIsuServicesExtra
    {
        private readonly List<MegaFaculty> _faculties = new List<MegaFaculty>();

        public MegaFaculty AddMegaFaculty(string name)
        {
            foreach (MegaFaculty faculty in _faculties)
            {
                if (_faculties.Count != 0 && name != faculty.Name) continue;
                throw new MegaFacultyAlreadyExistException();
            }

            var megaFaculty = new MegaFaculty(name);
            _faculties.Add(megaFaculty);
            return megaFaculty;
        }

        public ProStudent AddStudent(string name, GroupWithLessons group)
        {
            var student = new ProStudent(name);
            group.AddStudent(student);
            return student;
        }

        public void AddGroupWithLessonsToMegaFaculty(GroupWithLessons group, string megaFacultyName)
        {
            foreach (MegaFaculty faculty in _faculties)
            {
                if (faculty.Name != megaFacultyName) continue;
                faculty.AddGroupToFaculty(group);
            }

            throw new IncorrectMegaFacultyNameException();
        }

        public Ognp AddOgnp(string megaFacultyName, string ognpName, List<StudentFlow> courses)
        {
            foreach (MegaFaculty faculty in _faculties)
            {
                if (faculty.Name != megaFacultyName) continue;
                var ognp = new Ognp(ognpName, courses);
                faculty.SetOgnpToMegaFaculty(ognp);
                return ognp;
            }

            throw new IncorrectMegaFacultyNameException();
        }

        public void RegisterStudentOnOgnp(string ognpName, ProStudent student, string flowName)
        {
            foreach (MegaFaculty faculty in _faculties)
            {
                if (faculty.Ognp.Name != ognpName) continue;

                foreach (StudentFlow flow in faculty.Ognp.Courses)
                {
                    if (flow.Name != flowName) continue;
                    if (!CheckValidGroupAndOgnpTimetable(flow.Timetable, student.Timetable))
                        throw new CanNotRegisterStudentOnOgnpException();

                    flow.AddStudent(student);
                    return;
                }

                throw new IncorrectFlowNameException();
            }

            throw new IncorrectOgnpNameException();
        }

        public void DeleteRegistrationOnOgnp(string ognpName, ProStudent student, string flowName)
        {
            foreach (MegaFaculty faculty in _faculties)
            {
                if (faculty.Ognp.Name != ognpName) continue;

                foreach (StudentFlow flow in faculty.Ognp.Courses)
                {
                    if (flow.Name != flowName) continue;

                    flow.DeleteStudent(student);
                    return;
                }

                throw new IncorrectFlowNameException();
            }

            throw new IncorrectOgnpNameException();
        }

        public List<StudentFlow> FindFlowsInOgnp(string ognpName)
        {
            var ans = new List<StudentFlow>();
            foreach (MegaFaculty faculty in _faculties)
            {
                if (faculty.Ognp.Name != ognpName) continue;
                ans = faculty.Ognp.Courses;
            }

            return ans;
        }

        public List<ProStudent> FindStudentFlowsByCourseName(string ognpName, string courseName)
        {
            var ans = new List<ProStudent>();
            foreach (MegaFaculty faculty in _faculties)
            {
                if (faculty.Ognp.Name != ognpName) continue;
                foreach (StudentFlow course in faculty.Ognp.Courses)
                {
                    if (course.Name != courseName) continue;
                    ans = course.Students;
                }
            }

            return ans;
        }

        public List<ProStudent> FindStudentsWithoutOgnpByGroup(GroupWithLessons groupWithLessons)
        {
            var ans = new List<ProStudent>();
            foreach (ProStudent student in groupWithLessons.StudentsOfGroup)
            {
                if (student.HasOgnp) continue;
                ans.Add(student);
            }

            return ans;
        }

        private bool CheckValidGroupAndOgnpTimetable(Timetable timetable1, Timetable timetable2)
        {
            for (int i = 0; i < 8; ++i)
            {
                bool[] checkingTheIntersectionOfLessons = new bool[] { false, false, false, false, false, false, false, false };
                if (timetable1.GetStudyDay(i) == null && timetable2.GetStudyDay(i) == null) continue;
                if ((timetable1.GetStudyDay(i) == null && timetable2.GetStudyDay(i) != null) ||
                    (timetable1.GetStudyDay(i) != null && timetable2.GetStudyDay(i) == null)) continue;

                foreach (Lesson lesson in timetable1.GetStudyDay(i).Lessons)
                {
                    checkingTheIntersectionOfLessons[(int)lesson.LessonTime] = true;
                }

                foreach (Lesson lesson in timetable2.GetStudyDay(i).Lessons)
                {
                    if (checkingTheIntersectionOfLessons[(int)lesson.LessonTime])
                        return false;
                }

                return true;
            }

            return true;
        }
    }
}