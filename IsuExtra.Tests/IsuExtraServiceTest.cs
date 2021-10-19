using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraServiceTest
    {
        private IIsuServicesExtra _service;
        [SetUp]
        public void Setup()
        {
            _service = new IsuServicesExtra();
        }

        [Test]
        public void AddOgnp_CreateMegaFacultyAndOgnp_OgnpAdded()
        {
            MegaFaculty itip = _service.AddMegaFaculty("ИТИП");
            var courses = new List<StudentFlow> { };
            Ognp ognp = _service.AddOgnp("ИТИП", "Матлогика", courses);
            Assert.AreEqual("Матлогика", ognp.Name);
        }

        [Test]
        public void RegisterStudentOnOgnp_StudentExistAndOgnpExist_StudentRegistered()
        {
            var aud403 = new Auditorium(403, Buildings.MainBuilding);
            var m3204 = new GroupWithLessons(new GroupName("M3204"));
            var vozianova = new Lector("Vozianova Anna");
            var mayatin = new Lector("Mayatin Aleksandr");

            var matan = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);
            var osi = new Lesson("Операционные системы", m3204, mayatin, LessonNumber.Third, aud403);

            var monday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Monday);
            var friday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Friday);

            var timetable = new Timetable(new List<StudyDay>() { monday, friday });
            m3204.SetTimetable(timetable);

            ProStudent student = _service.AddStudent("Alex", m3204);
            
            MegaFaculty itip = _service.AddMegaFaculty("ИТИП");
            var proMatan1 = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);

            var saturday = new StudyDay(new List<Lesson>() { proMatan1 }, WeekDay.Saturday);
            timetable = new Timetable(new List<StudyDay>() { saturday });
            
            var ml1 = new StudentFlow("МЛ-1", timetable);
            var courses = new List<StudentFlow> { ml1 };
            Ognp ognp = _service.AddOgnp("ИТИП", "Матлогика", courses);
            
            Assert.AreEqual(false, student.HasOgnp);
            _service.RegisterStudentOnOgnp("Матлогика", student, "МЛ-1");
            Assert.AreEqual(true, student.HasOgnp);
        }
        
        [Test]
        public void DeleteRegistrationOnOgnp_StudentExistAndOgnpExist_RegisteredDeleted()
        {
            var aud403 = new Auditorium(403, Buildings.MainBuilding);
            var m3204 = new GroupWithLessons(new GroupName("M3204"));
            var vozianova = new Lector("Vozianova Anna");
            var mayatin = new Lector("Mayatin Aleksandr");

            var matan = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);
            var osi = new Lesson("Операционные системы", m3204, mayatin, LessonNumber.Third, aud403);

            var monday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Monday);
            var friday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Friday);

            var timetable = new Timetable(new List<StudyDay>() { monday, friday });
            m3204.SetTimetable(timetable);

            ProStudent student = _service.AddStudent("Alex", m3204);
            
            MegaFaculty itip = _service.AddMegaFaculty("ИТИП");
            var proMatan1 = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);

            var saturday = new StudyDay(new List<Lesson>() { proMatan1 }, WeekDay.Saturday);
            timetable = new Timetable(new List<StudyDay>() { saturday });
            
            var ml1 = new StudentFlow("МЛ-1", timetable);
            var courses = new List<StudentFlow> { ml1 };
            Ognp ognp = _service.AddOgnp("ИТИП", "Матлогика", courses);
            
            Assert.AreEqual(false, student.HasOgnp);
            _service.RegisterStudentOnOgnp("Матлогика", student, "МЛ-1");
            Assert.AreEqual(true, student.HasOgnp);
            
            _service.DeleteRegistrationOnOgnp("Матлогика", student, "МЛ-1");
            Assert.AreEqual(false, student.HasOgnp);
        }

        [Test]
        public void FindFlowsInOgnp_FlowsExistsAndTheyAreCorrect_FlowsFound()
        {
            var aud403 = new Auditorium(403, Buildings.MainBuilding);
            var m3204 = new GroupWithLessons(new GroupName("M3204"));
            var vozianova = new Lector("Vozianova Anna");
            var mayatin = new Lector("Mayatin Aleksandr");

            var matan = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);
            var osi = new Lesson("Операционные системы", m3204, mayatin, LessonNumber.Third, aud403);

            var monday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Monday);
            var friday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Friday);

            var timetable = new Timetable(new List<StudyDay>() { monday, friday });
            m3204.SetTimetable(timetable);

            ProStudent student = _service.AddStudent("Alex", m3204);
            
            MegaFaculty itip = _service.AddMegaFaculty("ИТИП");
            var proMatan1 = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);

            var saturday = new StudyDay(new List<Lesson>() { proMatan1 }, WeekDay.Saturday);
            timetable = new Timetable(new List<StudyDay>() { saturday });
            
            var ml1 = new StudentFlow("МЛ-1", timetable);
            var ml2 = new StudentFlow("МЛ-2", timetable);
            var courses = new List<StudentFlow> { ml1, ml2 };
            Ognp ognp = _service.AddOgnp("ИТИП", "Матлогика", courses);
            
            _service.RegisterStudentOnOgnp("Матлогика", student, "МЛ-1");

            List<StudentFlow> result = _service.FindFlowsInOgnp("Матлогика");
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void FindStudentFlowsByCourseName_SituationValid_FoundStudents()
        {
            var aud403 = new Auditorium(403, Buildings.MainBuilding);
            var m3204 = new GroupWithLessons(new GroupName("M3204"));
            var vozianova = new Lector("Vozianova Anna");
            var mayatin = new Lector("Mayatin Aleksandr");

            var matan = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);
            var osi = new Lesson("Операционные системы", m3204, mayatin, LessonNumber.Third, aud403);

            var monday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Monday);
            var friday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Friday);

            var timetable = new Timetable(new List<StudyDay>() { monday, friday });
            m3204.SetTimetable(timetable);

            ProStudent student = _service.AddStudent("Alex", m3204);
            ProStudent student1 = _service.AddStudent("Alex", m3204);
            ProStudent student2 = _service.AddStudent("Alex", m3204);

            
            MegaFaculty itip = _service.AddMegaFaculty("ИТИП");
            var proMatan1 = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);

            var saturday = new StudyDay(new List<Lesson>() { proMatan1 }, WeekDay.Saturday);
            timetable = new Timetable(new List<StudyDay>() { saturday });
            
            var ml1 = new StudentFlow("МЛ-1", timetable);
            var ml2 = new StudentFlow("МЛ-2", timetable);
            var courses = new List<StudentFlow> { ml1, ml2 };
            Ognp ognp = _service.AddOgnp("ИТИП", "Матлогика", courses);
            
            _service.RegisterStudentOnOgnp("Матлогика", student, "МЛ-1");
            _service.RegisterStudentOnOgnp("Матлогика", student1, "МЛ-2");
            _service.RegisterStudentOnOgnp("Матлогика", student2, "МЛ-1");


            List<ProStudent> result = _service.FindStudentFlowsByCourseName("Матлогика", "МЛ-1");
            Assert.AreEqual(2, result.Count);
            result = _service.FindStudentFlowsByCourseName("Матлогика", "МЛ-2");
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public void FindStudentsWithoutOgnpByGroup_SituationValid_FoundStudents()
        {
            var aud403 = new Auditorium(403, Buildings.MainBuilding);
            var m3204 = new GroupWithLessons(new GroupName("M3204"));
            var vozianova = new Lector("Vozianova Anna");
            var mayatin = new Lector("Mayatin Aleksandr");

            var matan = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);
            var osi = new Lesson("Операционные системы", m3204, mayatin, LessonNumber.Third, aud403);

            var monday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Monday);
            var friday = new StudyDay(new List<Lesson>() { matan, osi }, WeekDay.Friday);

            var timetable = new Timetable(new List<StudyDay>() { monday, friday });
            m3204.SetTimetable(timetable);

            ProStudent student = _service.AddStudent("Alex", m3204);
            ProStudent student1 = _service.AddStudent("Alex", m3204);
            ProStudent student2 = _service.AddStudent("Alex", m3204);

            
            MegaFaculty itip = _service.AddMegaFaculty("ИТИП");
            var proMatan1 = new Lesson("Матанализ", m3204, vozianova, LessonNumber.Second, aud403);

            var saturday = new StudyDay(new List<Lesson>() { proMatan1 }, WeekDay.Saturday);
            timetable = new Timetable(new List<StudyDay>() { saturday });
            
            var ml1 = new StudentFlow("МЛ-1", timetable);
            var ml2 = new StudentFlow("МЛ-2", timetable);
            var courses = new List<StudentFlow> { ml1, ml2 };
            Ognp ognp = _service.AddOgnp("ИТИП", "Матлогика", courses);
            
            _service.RegisterStudentOnOgnp("Матлогика", student, "МЛ-1");
            _service.RegisterStudentOnOgnp("Матлогика", student2, "МЛ-1");


            List<ProStudent> result = _service.FindStudentsWithoutOgnpByGroup(m3204);
            Assert.AreEqual(1, result.Count);
            _service.RegisterStudentOnOgnp("Матлогика", student1, "МЛ-2");
            result = _service.FindStudentsWithoutOgnpByGroup(m3204);
            Assert.AreEqual(0, result.Count);
        }
    }
}