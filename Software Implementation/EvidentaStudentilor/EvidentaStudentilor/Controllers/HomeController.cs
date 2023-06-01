using EvidentaStudentilor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EvidentaStudentilor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]                                                       // atribute routing
        [HttpGet]                                                                   // http method
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Privacy")]
        [Route("Home/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("Error")]
        [Route("Home/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}