using NUnit.Framework;
using Moq;
using EvidentaStudentilor.Controllers;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;


namespace EvidentaStudentilor.Tests.ControllerTests
{
    internal class GradeControllerTests
    {
        private Mock<IGradeService> mockGradeService;                                                    // mocking IGradeService, which return grades
        private List<Grade> gradesTest;                                                                  // fake list of grades for testing

        [OneTimeSetUp]
        public void Init()
        {
            mockGradeService = new Mock<IGradeService>();
            gradesTest = new List<Grade>();
            gradesTest.AddRange(new Grade[]
            {
                new Grade(),
                new Grade(),
                new Grade()
            });
            gradesTest[0].ExamId = 1;
            gradesTest[1].ExamId = 1;
            gradesTest[2].ExamId = 2;
            gradesTest[2].ActualGrade = 10;
        }


        [Test]
        public void HavingGradesController_WhenSelectedExamId_ThenReturnGradesForExamId()
        {
            //Arange
            mockGradeService.Setup(x => x.GetGradeOfExam(It.IsAny<int>())).Returns(gradesTest.Where(x => x.ExamId == 1));
            GradesController controller = new GradesController(mockGradeService.Object);

            //Act
            ViewResult? result = controller.Index(1) as ViewResult;
            List<Grade>? grades = result.ViewData.Model as List<Grade>;


            //Result
            Assert.That(grades.Count, Is.EqualTo(2));
        }


        [Test]
        public void HavingGradesController_WhenAskToEditExamId_ThenReturnGradeForExamId()
        {
            //Arange
            _ = mockGradeService.Setup(x => x.GetGrade(It.IsAny<int>())).Returns(gradesTest.FirstOrDefault(x => x.ExamId == 2));
            GradesController controller = new GradesController(mockGradeService.Object);

            //Act
            ViewResult? result = controller.Edit(2) as ViewResult;
            Grade? grade = result.ViewData.Model as Grade;


            //Result
            Assert.That(grade.ActualGrade, Is.EqualTo(10));
        }
    }
}
