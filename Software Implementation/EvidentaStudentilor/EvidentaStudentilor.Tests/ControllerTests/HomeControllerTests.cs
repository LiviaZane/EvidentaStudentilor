using NUnit.Framework;
using EvidentaStudentilor.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EvidentaStudentilor.Tests.ControllerTests
{
    public class HomeControllerTests
    {

        [Test]
        public void HavingHomeController_WhenApplicationStart_ThenIndexPageIsOpen()
        {
            //Arange
            HomeController controller = new HomeController(null);

            //Act
            ViewResult? result = controller.Index() as ViewResult;

            //Result
            Assert.IsNotNull(result);
        }

        [Test]
        public void HavingHomeController_WhenPrivacyActionIsAccesed_ThenPrivacyPageIsOpen()
        {
            //Arange
            HomeController controller = new HomeController(null);

            //Act
            ViewResult? result = controller.Privacy() as ViewResult;

            //Result
            Assert.IsNotNull(result);
        }
    }
}