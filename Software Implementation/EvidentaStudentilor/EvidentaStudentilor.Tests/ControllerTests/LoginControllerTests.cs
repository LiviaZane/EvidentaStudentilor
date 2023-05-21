using NUnit.Framework;
using Moq;
using EvidentaStudentilor.Controllers;
using Microsoft.AspNetCore.Mvc;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Tests.ControllerTests
{
    public class LoginControllerTests
    {
        private Mock<IUserService> mockUserService;                                                      // mocking IUserService, which return users
        private User userTest;                                                                           // user for testing

        [OneTimeSetUp]
        public void Init()
        {
            mockUserService = new Mock<IUserService>();
            userTest = new User();
            userTest.Id = 1; userTest.Email = "inexistent";
        }

        [Test]
        public void HavingLoginController_WhenUserEmailOrPasswordAreNotInDatabase_ThenDisplayExpectedMessage()
        {
            //Arange
            mockUserService.Setup(x => x.GetUserByEmailPassword(It.IsAny<string>(), It.IsAny<string>())).Returns((User)null);
            LoginController controller = new LoginController(null, mockUserService.Object, null, null, null, null, null);

            //Act
            ViewResult result = controller.Login(userTest) as ViewResult;

            //Result
            Assert.That(result.ViewData.First().Value, Is.EqualTo("Wrong user name or password !!!"));
        }
       
        [Test]
        public void HavingLoginController_WhenUserModelFromGoogleFacebookIsInvalid_ThenStatusModelIsFaulted()
        {
            //Arange
            mockUserService.Setup(x => x.GetUserByEmailPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(userTest);
            LoginController controller = new LoginController(null, mockUserService.Object, null, null, null, null, null);

            //Act
            var result = controller.GoogleFacebookResponse();

            //Result
            Assert.That(result.Status.ToString(), Is.EqualTo("Faulted"));
        }
    }
}