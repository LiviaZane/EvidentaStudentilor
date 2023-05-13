using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvidentaStudentilor.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: AdministratorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdministratorController
        public ActionResult Save()
        {
            return View("Index");
        }

        // GET: AdministratorController
        public ActionResult Restore()
        {
            return View("Index");
        }

    }
}
