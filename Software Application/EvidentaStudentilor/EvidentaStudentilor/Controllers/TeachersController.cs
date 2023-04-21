using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.Models;
using NuGet.DependencyResolver;
using EvidentaStudentilor.Utilities;

namespace EvidentaStudentilor.Controllers
{
    public class TeachersController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public TeachersController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: Teachers
        [Authentication]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var evidentaStudentilorContext = _context.Teachers.Include(t => t.Department).Include(t => t.User);
            return View(await evidentaStudentilorContext.ToListAsync());
        }

        // GET: Teachers/Details/5
        [Authentication]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Department)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            var list2 = await _context.Departments.Where(y => y.Id == teacher.DepartmentId).ToListAsync();
            ViewBag.Department = list2.FirstOrDefault().Name;
            return View(teacher);
        }

        // GET: Teachers/Create
        [Authorize("Secretar")]
        [HttpGet]
        public IActionResult Create()
        {
            Teacher teacher = new Teacher();
            var list = _context.Users.Where(x => _context.Teachers.All(y => y.UserId != x.Id) && x.RoleId == 2).ToList(); //role=2 for teachers
            ViewBag.UserId = new SelectList(list, "Id", "Id", teacher.UserId);
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name", teacher.DepartmentId);
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,FirstName,DepartmentId,Title")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.Id = 0;
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,FirstName,DepartmentId,Title")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Department)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Teachers'  is null.");
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
          return (_context.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
