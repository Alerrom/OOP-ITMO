using System;
using Isu.Entities;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Assert.Catch<IsuException>(() => 
            {
                Group group1 = _isuService.AddGroup("M3415");
                Group group2 = _isuService.AddGroup("M3415");
                Student s = _isuService.AddStudent(group1, "Alex");
                s = _isuService.AddStudent(group2, "Alex");
            });
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3201");
                for (int i = 0; i < 26; i++)
                {
                    _isuService.AddStudent(group, "Alex" + Convert.ToString(i));
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M320441");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group1 = _isuService.AddGroup("M3415");
                Group group2 = _isuService.AddGroup("M3415");
                Student s = _isuService.AddStudent(group1, "Alex");
                _isuService.ChangeStudentGroup(s, group1);
            });
        }
    }
}