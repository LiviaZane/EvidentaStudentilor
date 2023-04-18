using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.Models;
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
        public async Task<IActionResult> Index()
        {
            var list = await _context.Teachers.Include(x => x.Department).Where(x => x.Department.Id == x.DepartmentId).
                Include(y => y.User).Where(y => y.User.Id == y.UserId).ToListAsync();

            if (_context.Students == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }

            return View(list);
        }

        // GET: Teachers/Details/5
        [Authentication]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            var list = await _context.Users.Where(y => y.Id == teacher.UserId).ToListAsync();
            ViewBag.TeacherName = list.FirstOrDefault().Name + " " + list.FirstOrDefault().FirstName;
            var list2 = await _context.Departments.Where(y => y.Id == teacher.DepartmentId).ToListAsync();
            ViewBag.Department = list2.FirstOrDefault().Name;
            return View(teacher);
        }

        // GET: Teachers/Create
        [Authorize("Secretar")]
        public async Task<IActionResult> Create()
        {
            var list = await _context.Users.Where(x => _context.Teachers.All(y => y.UserId != x.Id) && x.RoleId == 2).ToListAsync();  //role=2 for teacher
            return View(list);
        }

        [Authorize("Secretar")]
        public IActionResult Create2(int? id)
        {
            Teacher teacher = new Teacher();
            teacher.UserId = (int)id;
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name", teacher.DepartmentId);
            return View(teacher);
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("Id,UserId,DepartmentId,Title")] Teacher teacher)
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
            var list = await _context.Users.Where(y => y.Id == teacher.UserId).ToListAsync();
            ViewBag.TeacherName = list.FirstOrDefault().Name + " " + list.FirstOrDefault().FirstName;
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name", teacher.DepartmentId);   // used ViewBag to display Departments.Name in SelectList
            return View(teacher);                                                               // and save Department.Id (from SelectList) instead teacher.DepartmentId
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,DepartmentId,Title")] Teacher teacher)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
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
