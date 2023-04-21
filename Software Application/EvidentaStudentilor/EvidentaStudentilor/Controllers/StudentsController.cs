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
    public class StudentsController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public StudentsController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: Students
        [Authorize("Secretar", "Teacher")]
        [HttpGet]
        public async Task<IActionResult> Index(string? searchStringName, string? searchStringFirstName)
        {
            var list = await _context.Students.Include(x => x.Profile).Where(x => x.Profile.Id == x.ProfileId).
                Include(y => y.User).Where(y => y.User.Id == y.UserId).ToListAsync();

            if (_context.Students == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }

            if (!String.IsNullOrEmpty(searchStringName) && !String.IsNullOrEmpty(searchStringFirstName))
            {
                list = await _context.Students.Where(s => s.Name.Contains(searchStringName) && s.FirstName.Contains(searchStringFirstName))
                                              .ToListAsync();
            }
            else if (!String.IsNullOrEmpty(searchStringName))
            {
                list = await _context.Students.Where(s => s.Name.Contains(searchStringName))
                                             .ToListAsync();
            }
            else if (!String.IsNullOrEmpty(searchStringFirstName))
            {
                list = await _context.Students.Where(s => s.FirstName.Contains(searchStringFirstName))
                                             .ToListAsync();
            }

            return View(list);
        }

        
        // GET: Students/Details/5
        [Authorize("Secretar", "Teacher")]
        [HttpGet]
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
            var list2 = await _context.Profiles.Where(y => y.Id == student.ProfileId).ToListAsync();
            ViewBag.Profile = list2.FirstOrDefault().Name;
            return View(student);
        }


        // GET: Students/Create
        [Authorize("Secretar")]
        [HttpGet]
        public IActionResult Create()
        {
            Student student = new Student();
            var list = _context.Users.Where(x => _context.Students.All(y => y.UserId != x.Id) && x.RoleId == 1).ToList(); //role=1 for students
            ViewBag.UserId = new SelectList(list, "Id", "Id", student.UserId);
            ViewBag.ProfileId = new SelectList(_context.Profiles, "Id", "Name", student.ProfileId);
            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,FirstName,AdmisionYear,CurrentYear,ProfileId,Budget")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = 0;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", student.ProfileId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", student.UserId);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
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
            ViewBag.ProfileId = new SelectList(_context.Profiles, "Id", "Name", student.ProfileId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,FirstName,AdmisionYear,CurrentYear,ProfileId,Budget")] Student student)
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", student.ProfileId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", student.UserId);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Profile)
                .Include(s => s.User)
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
