using EvidentaStudentilor.DataModel;
using Microsoft.AspNetCore.Mvc;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.Utilities;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace EvidentaStudentilor.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDepartmentService departments;
        private readonly IProfileService profiles;
        private readonly IUserService users;
        private readonly IRoleService roles;
        private readonly IStudentService students;
        private readonly ITeacherService teachers;
        private readonly ILogger _logger;

        public LoginController(IRoleService roleService, IUserService userService, IStudentService studentService, 
            ITeacherService teacherService, IProfileService profileService, IDepartmentService departmentService, 
            ILogger<LoginController> logger)
        {
            departments = departmentService;
            profiles = profileService;
            users = userService;
            roles = roleService;
            students = studentService;
            teachers = teacherService;
            _logger = logger;
        }

        // Get Action
        [HttpGet]                                                              // http method
        [Route("Login")]                                                       // atribute routing
        [Route("Login/Login")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)              // if not a user in session
            {
                return View();                                                  // open the view
            }
            else                                                                // else  ... already the user has information saved in session
            {
                return RedirectToAction("Index", "Home");                      // open the Index view from Home controller
            }
        }

        //Post Action
        [HttpPost]
        public ActionResult Login(User u)                                               // after view submit, with normal login
        {
            if (ModelState.IsValid)
            {
                var user = users.GetUserByEmailPassword(u.Email, u.Paswword);           // seek in users a user with email/password inserted in view

                if (user != null)                                                       // if an user was founded
                {
                    SetSessionVariable(user);                                           // save in session, user information (function implemented bellow)

                    _logger.LogInformation("User " + user.Email + " logged in, at: " +
                        DateTime.Now, ConsoleColor.Red);                                           // message in console

                    return RedirectToAction("Index", "Home");
                }
                else                                                                   // if not exist a user with email/password
                {
                    ViewBag.WrongUser = "Wrong user name or password !!!";             // set message in ViewBag to transfer into view
                    return View();                                                     // return the view with ViewBag message
                }
            }
            else                                                                       // if Model is not valid (u doesn't respect the User model)
            {
                return RedirectToAction("Login");                                      // redirect to Login action
            }
        }

        public IActionResult LoginGoogle()                                            // executed after press Login with Google button
        {
            return new ChallengeResult(
                GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleFacebookResponse", "Login")         // Where google responds back (action bellow)
                });
        }

        public IActionResult LoginFacebook()                                           // after press Login with Facebook button
        {
            return new ChallengeResult(
                FacebookDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleFacebookResponse", "Login")         // Where facebook responds back (action bellow)
                });
        }

        public async Task<IActionResult> GoogleFacebookResponse()                       // after submit in view, with Google or Facebook login
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("External");  //Authentication response from google/facebook
            if (authenticateResult.Principal != null)     //check if principal value exists (google/facebook returned information)
            {
                var googleFacebookEmail = authenticateResult.Principal.FindFirst(ClaimTypes.Email).Value;
                var user = users.GetUserByEmail(googleFacebookEmail);
                if (user != null)
                {
                    SetSessionVariable(user);                                                // save in session, user information

                    _logger.LogInformation("User " + user.Email + " logged in, at: " +
                        DateTime.Now);                                                       // message in console for succes login

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.WrongUser = "Wrong user name or password !!!";
                    return View("Login");
                }
            }
            return RedirectToAction("LoginGoogle", "Login");
        }
         

        [HttpGet]                                                              // http method
        [Authentication]                                                       // control acces...just logged user can acces Logout
        [Route("Login/Logout")]                                                // atribute routing
        public ActionResult Logout()                                           // accesed when Logout button from navbar is pressed
        {
            _logger.LogInformation("User " + HttpContext.Session.GetString("UserName") + " logged out, at: " +
                DateTime.Now);                                                                                  // message in console for logout

            HttpContext.Session.Clear(); // Delete session variable (username, role, etc.)

            //Check for the cookies of application and delete them
            var siteCookies = HttpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
            foreach (var cookie in siteCookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }

            return RedirectToAction("Index", "Home");
        }


        private void SetSessionVariable(User user)            // method to save in session variables, the information regarding user (email, role, entire name and profile/department)
        {
            Role role = roles.GetRole(user.RoleId);                                               // get the role of the legged user (founded before in users with email/password)
            HttpContext.Session.SetString("UserRole", role.Name.ToString());                      // save in session the user role
            HttpContext.Session.SetString("UserName", user.Email.ToString());                     // save in session the user email
            HttpContext.Session.SetString("UserId", user.Id.ToString());                          // save in session the user id
            if (role.Name == "Student")
            {
                var student = students.GetStudentByUserId(user.Id);
                HttpContext.Session.SetString("UserEntireName", student.Name + " " + student.FirstName);
                HttpContext.Session.SetString("StudTeachID", student.Id.ToString());
                var profile = profiles.GetProfile(student.ProfileId);
                HttpContext.Session.SetString("UserProfile", profile.Name);
            }
            else if (role.Name == "Teacher")
            {
                var teacher = teachers.GetTeacherByUserId(user.Id);
                HttpContext.Session.SetString("UserEntireName", teacher.Name + " " + teacher.FirstName);
                HttpContext.Session.SetString("StudTeachID", teacher.Id.ToString());
                var department = departments.GetDepartment(teacher.DepartmentId);
                HttpContext.Session.SetString("UserProfile", department.Name);
            }
        }
    }
}
