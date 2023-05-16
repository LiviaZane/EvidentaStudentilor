using NUnit.Framework;
using Moq;
using EvidentaStudentilor.Controllers;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.Tests.ControllerTests
{
    internal class TeacherUserControllerTests
    {
        private Mock<IExamService> mockExamService;                                                      // mocking IExamService, which return exams
        private Mock<IGradeService> mockGradeService;                                                    // mocking IGradeService, which return grades


        [OneTimeSetUp]
        public void Init()
        {
            mockExamService = new Mock<IExamService>();
            mockGradeService = new Mock<IGradeService>();
        }


        [Test]
        public void HavingTeacherUserController_WhenSelectedOpenExam_ThenRedirectToIndexActonFromGradesController()
        {

            //Arange
            TeacherUserController controller = new TeacherUserController(mockExamService.Object, mockGradeService.Object);

            //Act
            var result = (RedirectToActionResult)controller.Open(-1);


            //Result
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Grades"));
        }
    }
}
