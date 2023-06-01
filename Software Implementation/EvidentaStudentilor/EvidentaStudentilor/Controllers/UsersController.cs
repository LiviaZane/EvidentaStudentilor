using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService users;

        public UsersController(IUserService userService)
        {
            users = userService;
        }

        // GET: Users
        [Route("Users")]
        [Route("Users/Index")]
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public IActionResult Index()
        {
            List<User> list = new List<User>();
            if (HttpContext.Session.GetString("UserRole") == "Administrator")
            {
                list = users.GetUsersRole("Secretar", "Administrator").ToList();
            }
            else
            {
                list = users.GetUsersRole("Student", "Teacher").ToList();
            }


            if (users.GetUser() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }

            return View(list);
        }

        // GET: Users/Details/5
        [Route("Users/Details")]
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (users.GetUser() == null)
            {
                return NotFound();
            }

            User user = users.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Route("Users/Create")]
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public IActionResult Create()
        {
            User user = new User();
            List<Role> list = new List<Role>();
            if (HttpContext.Session.GetString("UserRole") == "Administrator")
            {
                list = users.GetRole("Secretar", "Administrator").ToList();
            }
            else
            {
                list = users.GetRole("Student", "Teacher").ToList();
            } 
            ViewBag.RoleId = new SelectList(list, "Id", "Name", user.RoleId);
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar", "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,RoleId,Email,Paswword")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = 0;
                users.createUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Route("Users/Edit")]
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (users.GetUser() == null)
            {
                return NotFound();
            }

            var user = users.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            List<Role> list = new List<Role>();
            if (HttpContext.Session.GetString("UserRole") == "Administrator")
            {
                list = users.GetRole("Administrator", "Secretar").ToList();
            }
            else
            {
                list = users.GetRole("Student", "Teacher").ToList();
            }
            ViewBag.RoleId = new SelectList(list, "Id", "Name", user.RoleId);   // used ViewBag to display Roles.Name in SelectList
            return View(user);                                                            // and save Role.Id (from SelectList) instead User.Id
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar", "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,RoleId,Email,Paswword")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    users.updateUser(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Route("Users/Delete")]
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (users.GetUser() == null)
            {
                return NotFound();
            }

            var user = users.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [Authorize("Secretar", "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (users.GetUser() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Users'  is null.");
            }
            var user = users.GetUser(id);
            if (user != null)
            {
                users.deleteUser(user);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (users.GetUser(id) == null) ? false : true;
        }
    }
}
