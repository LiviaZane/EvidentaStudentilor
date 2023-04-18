using EvidentaStudentilor.Models;
using EvidentaStudentilor.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvidentaStudentilor.Controllers
{
    public class LoginController : Controller
    {
        private readonly EvidentaStudentilorContext _context;
        public static HttpContext httpContext;
        public LoginController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // Get Action
        public IActionResult Login()
        {
            httpContext = HttpContext;
            if (httpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Post Action
        [HttpPost]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(a => a.Email.Equals(u.Email) && a.Paswword.Equals(u.Paswword)).FirstOrDefault();

                if (user != null)
                {
                    var role = _context.Roles.Where(r => r.Id == user.RoleId).FirstOrDefault();
                    HttpContext.Session.SetString("UserName", user.Email.ToString());
                    HttpContext.Session.SetString("UserRole", role.Name.ToString());
                    HttpContext.Session.SetString("UserEntireName", user.Name.ToString() + " " + user.FirstName.ToString());
                    Student student = _context.Students.Where(x => x.UserId == user.Id).FirstOrDefault();
                    Teacher teacher = _context.Teachers.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (student != null)
                    {
                        string profile = _context.Profiles.Where(x => x.Id == student.ProfileId).FirstOrDefault().Name.ToString();
                        HttpContext.Session.SetString("UserProfile", profile);
                    } 
                    else if (teacher != null) 
                    {
                        string department = _context.Departments.Where(x => x.Id == teacher.DepartmentId).FirstOrDefault().Name.ToString();
                        HttpContext.Session.SetString("UserProfile", department);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.WrongUser = "Wrong user name or password !!!";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
