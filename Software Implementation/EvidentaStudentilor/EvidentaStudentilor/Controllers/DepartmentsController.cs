using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService departments;

        public DepartmentsController(IDepartmentService departmentService)
        {
            departments = departmentService;
        }

        // GET: Departments
        [HttpGet]                                                              // http method
        [Authentication]                                                       // control acces
        [Route("Departments")]                                                 // atribute routing
        [Route("Departments/Index")]
        public IActionResult Index()
        {
              return departments.GetDepartment() != null ? 
                          View(departments.GetDepartment().ToList()) :
                          Problem("Entity set 'EvidentaStudentilorContext.Departments'  is null.");
        }

        // GET: Departments/Details/5
        [Authentication]
        [HttpGet]
        [Route("Departments/Details")]
        public IActionResult Details(int id)
        {
            if (departments.GetDepartment() == null)
            {
                return NotFound();
            }

            Department department = departments.GetDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Departments/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                departments.createDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Departments/Edit")]
        public IActionResult Edit(int id)
        {
            if (departments.GetDepartment() == null)
            {
                return NotFound();
            }

            Department department = departments.GetDepartment(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    departments.updateDepartment(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Departments/Delete")]
        public IActionResult Delete(int id)
        {
            if (departments.GetDepartment() == null)
            {
                return NotFound();
            }

            Department department = departments.GetDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (departments.GetDepartment() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Departments'  is null.");
            }
            Department department = departments.GetDepartment(id);
            if (department != null)
            {
                departments.deleteDepartment(department);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return (departments.GetDepartment(id) == null) ? false : true;
        }
    }
}
