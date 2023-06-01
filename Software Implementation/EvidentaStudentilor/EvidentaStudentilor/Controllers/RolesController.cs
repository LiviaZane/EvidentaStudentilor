using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService roles;

        public RolesController(IRoleService roleService)
        {
            roles = roleService;
        }

        // GET: Roles
        [Authorize("Administrator", "Secretar")]                                           // control acces
        [HttpGet]                                                                          // http method
        [Route("Roles")]                                                                   // atribute routing
        [Route("Roles/Index")]
        public IActionResult Index()
        {
              return roles.GetRole() != null ? 
                          View(roles.GetRole().ToList()) :
                          Problem("Entity set 'EvidentaStudentilorContext.Roles'  is null.");
        }

        // GET: Roles/Details/5
        [Authorize("Administrator")]
        [HttpGet]
        [Route("Roles/Details")]
        public IActionResult Details(int id)
        {
            if (roles.GetRole() == null)
            {
                return NotFound();
            }

            Role role = roles.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        [Authorize("Administrator")]
        [HttpGet]
        [Route("Roles/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                roles.createRole(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        [Authorize("Administrator")]
        [HttpGet]
        [Route("Roles/Edit")]
        public IActionResult Edit(int id)
        {
            if (roles.GetRole() == null)
            {
                return NotFound();
            }

            Role role = roles.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Role role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    roles.updateRole(role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Id))
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
            return View(role);
        }

        // GET: Roles/Delete/5
        [Authorize("Administrator")]
        [HttpGet]
        [Route("Roles/Delete")]
        public IActionResult Delete(int id)
        {
            if (roles.GetRole() == null)
            {
                return NotFound();
            }

            Role role = roles.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [Authorize("Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (roles.GetRole() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Roles'  is null.");
            }
            Role role = roles.GetRole(id);
            if (role != null)
            {
                roles.deleteRole(role);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return (roles.GetRole(id) == null) ? false : true;
        }
    }
}
