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
                    if (role.Name == "Student")
                    {
                        Student student = _context.Students.Where(s => s.UserId == user.Id).FirstOrDefault();
                        HttpContext.Session.SetString("UserEntireName", student.Name + " " + student.FirstName);
                        HttpContext.Session.SetString("StudTeachID", student.Id.ToString());
                        Profile profile = _context.Profiles.Where(p => p.Id == student.ProfileId).FirstOrDefault();
                        HttpContext.Session.SetString("UserProfile", profile.Name);
                    }
                    else if (role.Name == "Teacher") 
                    {
                        Teacher teacher = _context.Teachers.Where(s => s.UserId == user.Id).FirstOrDefault();
                        HttpContext.Session.SetString("UserEntireName", teacher.Name + " " + teacher.FirstName);
                        HttpContext.Session.SetString("StudTeachID", teacher.Id.ToString());
                        Department department = _context.Departments.Where(p => p.Id == teacher.DepartmentId).FirstOrDefault();
                        HttpContext.Session.SetString("UserProfile", department.Name);
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
