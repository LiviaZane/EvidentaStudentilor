using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.Models;
using System.Security.Permissions;
using NuGet.DependencyResolver;
using EvidentaStudentilor.Utilities;

namespace EvidentaStudentilor.Controllers
{
    public class StudentsController : Controller
    {
        private readonly EvidentaStudentilorContext _context;
        public StudentsController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: Students
        [Authorize("Secretar", "Teacher")]
        public async Task<IActionResult> Index(string searchStringName, string searchStringFirstName)
        {
            var list = await _context.Students.Include(x => x.Profile).Where(x => x.Profile.Id == x.ProfileId).
                Include(y => y.User).Where(y => y.User.Id == y.UserId).ToListAsync();

            if (_context.Students == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }

            if (!String.IsNullOrEmpty(searchStringName) && !String.IsNullOrEmpty(searchStringFirstName))
            {
                list = await _context.Students.Where(s => s.User.Name.Contains(searchStringName) && s.User.FirstName.Contains(searchStringFirstName))
                                              .ToListAsync();
            }
            else if (!String.IsNullOrEmpty(searchStringName))
            {
                list = await _context.Students.Where(s => s.User.Name.Contains(searchStringName))
                                             .ToListAsync();
            }
            else if (!String.IsNullOrEmpty(searchStringFirstName))
            {
                list = await _context.Students.Where(s => s.User.FirstName.Contains(searchStringFirstName))
                                             .ToListAsync();
            }

            return View(list); 
        }

        // GET: Students/Details/5
        [Authorize("Secretar", "Teacher")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            var list = await _context.Users.Where(y => y.Id == student.UserId).ToListAsync();
            ViewBag.StudentName = list.FirstOrDefault().Name + " " + list.FirstOrDefault().FirstName;
            var list2 = await _context.Profiles.Where(y => y.Id == student.ProfileId).ToListAsync();
            ViewBag.Profile = list2.FirstOrDefault().Name;
            return View(student);
        }

        // GET: Students/Create
        [Authorize("Secretar")]
        public async Task<IActionResult> Create()
        {
            var list = await _context.Users.Where(x => _context.Students.All(y => y.UserId != x.Id) && x.RoleId == 1).ToListAsync(); //role=1 for students
            return View(list);
        }

        [Authorize("Secretar")]
        public IActionResult Create2(int? id)
        {
            Student student = new Student();
            student.UserId = (int)id;
            ViewBag.ProfileId = new SelectList(_context.Profiles, "Id", "Name", student.ProfileId);
            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("Id,UserId,AdmisionYear,CurrentYear,ProfileId,Budget")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = 0;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize("Secretar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var list = await _context.Users.Where(y => y.Id == student.UserId).ToListAsync();
            ViewBag.StudentName = list.FirstOrDefault().Name + " " + list.FirstOrDefault().FirstName;
            ViewBag.ProfileId = new SelectList(_context.Profiles, "Id", "Name", student.ProfileId);   // used ViewBag to display Profiles.Name in SelectList
            return View(student);                                                                // and save Profile.Id (from SelectList) instead student.Id
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AdmisionYear,CurrentYear,ProfileId,Budget")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
