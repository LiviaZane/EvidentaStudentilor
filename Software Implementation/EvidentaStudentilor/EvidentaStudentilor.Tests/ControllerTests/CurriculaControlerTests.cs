using NUnit.Framework;
using Moq;
using EvidentaStudentilor.Controllers;
using Microsoft.AspNetCore.Mvc;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Tests.ControllerTests
{
    public class CurriculaControlerTests
    {
        private Mock<ICurriculaService> mockCurriculaService;                                             // mocking ICurriculaService, which return curricula
        private Curricula curriculaTest;                                                                  // curricula for testing

        [OneTimeSetUp]
        public void Init()
        {
            mockCurriculaService = new Mock<ICurriculaService>();
            curriculaTest = new Curricula();
            curriculaTest.Id = 10;
            mockCurriculaService.Setup(x => x.GetCurricula(10)).Returns(curriculaTest);
        }

        [Test]
        public void HavingCurriculaController_WhenCurriculaSelectedExist_ThenReturnDataModelCurricula()
        {
            //Arange
            CurriculaController controller = new CurriculaController(mockCurriculaService.Object);

            //Act
            ViewResult result = controller.Details(mockCurriculaService.Object.GetCurricula(10).Id) as ViewResult;

            //Result
            Assert.That(result.Model.ToString(), Is.EqualTo("EvidentaStudentilor.DataModel.Curricula"));
        }

        [Test]
        public void HavingCurriculaController_WhenCurriculaSelectedNotExist_ThenThrowExpectedException()
        {
            //Arange
            CurriculaController controller = new CurriculaController(mockCurriculaService.Object);

            //Act
            NullReferenceException ex = Assert.Throws<NullReferenceException>(() => { controller.Details(mockCurriculaService.Object.GetCurricula(11).Id); });

            //Result
            StringAssert.Contains("Object reference not set to an instance of an object.", ex.Message.ToString());
        }
    }
}