using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService teachers;

        public TeachersController(ITeacherService teacherService)
        {
            teachers = teacherService;
        }

        // GET: Teachers
        [Authentication]                                                               // control acces
        [HttpGet]                                                                      // http method
        [Route("Teachers")]                                                            // atribute routing
        [Route("Teachers/Index")]
        public IActionResult Index()
        {
            var evidentaStudentilorContext = teachers.GetTeacher();
            return View(evidentaStudentilorContext);
        }

        // GET: Teachers/Details/5
        [Authentication]
        [HttpGet]
        [Route("Teachers/Delete")]
        public IActionResult Details(int id)
        {
            if (teachers.GetTeacher() == null)
            {
                return NotFound();
            }

            Teacher teacher = teachers.GetTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewBag.Department = teachers.GetDepartment(teacher.DepartmentId);
            return View(teacher);
        }

        // GET: Teachers/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Teachers/Create")]
        public IActionResult Create()
        {
            Teacher teacher = new Teacher();
            ViewBag.UserId = new SelectList(teachers.GetUsersWithoutTeacher(), "Id", "Email", teacher.UserId);
            ViewBag.DepartmentId = new SelectList(teachers.GetDepartment(), "Id", "Name", teacher.DepartmentId);
            return View(teacher);
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,Name,FirstName,DepartmentId,Title")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.Id = 0;
                teachers.createTeacher(teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (teachers.GetTeacher() == null)
            {
                return NotFound();
            }

            var teacher = teachers.GetTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentId = new SelectList(teachers.GetDepartment(), "Id", "Name", teacher.DepartmentId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserId,Name,FirstName,DepartmentId,Title")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    teachers.updateTeacher(teacher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (teachers.GetTeacher() == null)
            {
                return NotFound();
            }

            Teacher teacher = teachers.GetTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (teachers.GetTeacher() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Teachers'  is null.");
            }
            Teacher teacher = teachers.GetTeacher(id);
            if (teacher != null)
            {
                teachers.deleteTeacher(teacher);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
          return (teachers.GetTeacher(id) == null) ? false : true;
        }
    }
}
