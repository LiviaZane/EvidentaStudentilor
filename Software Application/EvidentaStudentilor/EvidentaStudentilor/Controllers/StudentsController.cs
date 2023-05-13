using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService students;

        public StudentsController(IStudentService studentService)
        {
            students = studentService;
        }

        // GET: Students
        [Authorize("Secretar", "Teacher")]                                             // control acces
        [HttpGet]                                                                      // http method
        [Route("Students")]                                                             // atribute routing
        [Route("Students/Index")]
        public IActionResult Index(string? searchStringName, string? searchStringFirstName)
        {
            var list = students.GetStudent().ToList();

            if (students.GetStudent() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }

            if (!String.IsNullOrEmpty(searchStringName) && !String.IsNullOrEmpty(searchStringFirstName))
            {
                list = students.GetStudent().Where(s => s.Name.Contains(searchStringName) && s.FirstName.Contains(searchStringFirstName))
                                              .ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringName))
            {
                list = students.GetStudent().Where(s => s.Name.Contains(searchStringName))
                                             .ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringFirstName))
            {
                list = students.GetStudent().Where(s => s.FirstName.Contains(searchStringFirstName))
                                             .ToList();
            }

            return View(list);
        }

        
        // GET: Students/Details/5
        [Authorize("Secretar", "Teacher")]
        [HttpGet]
        [Route("Students/Details")]
        public IActionResult Details(int id)
        {
            if (students.GetStudent() == null)
            {
                return NotFound();
            }

            Student student = students.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        // GET: Students/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Students/Create")]
        public IActionResult Create()
        {
            Student student = new Student();
            ViewBag.UserId = new SelectList(students.GetUsersWithoutStudent(), "Id", "Email", student.UserId);
            ViewBag.ProfileId = new SelectList(students.GetProfile(), "Id", "Name", student.ProfileId);
            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,Name,FirstName,AdmisionYear,CurrentYear,ProfileId,Budget")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = 0;
                students.createStudent(student);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(students.GetProfile(), "Id", "Id", student.ProfileId);
            ViewData["UserId"] = new SelectList(students.GetUser(), "Id", "Id", student.UserId);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Students/Edit")]
        public IActionResult Edit(int id)
        {
            if (id == null || students.GetStudent() == null)
            {
                return NotFound();
            }

            Student student = students.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.ProfileId = new SelectList(students.GetProfile(), "Id", "Name", student.ProfileId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserId,Name,FirstName,AdmisionYear,CurrentYear,ProfileId,Budget")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    students.updateStudent(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Students/Delete")]
        public IActionResult Delete(int id)
        {
            if (students.GetStudent() == null)
            {
                return NotFound();
            }

            Student student = students.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (students.GetStudent() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }
            Student student = students.GetStudent(id);
            if (student != null)
            {
                students.deleteStudent(student);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (students.GetStudent(id) == null) ? false : true;
        }
    }
}
