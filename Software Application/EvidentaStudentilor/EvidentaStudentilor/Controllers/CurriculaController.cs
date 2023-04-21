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
    public class CurriculaController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public CurriculaController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        [Authorize("Secretar")]
        public IActionResult CreateExam(int? id) 
        {
            return RedirectToAction("Create", "Exams", new { ID = id });
        }

        // GET: Curricula
        [HttpGet]
        [Authentication]
        public async Task<IActionResult> Index()
        {
            var evidentaStudentilorContext = _context.Curricula.Include(c => c.Profile).Include(c => c.Subject);
            return View(await evidentaStudentilorContext.ToListAsync());
        }

        // GET: Curricula/Details/5
        [HttpGet]
        [Authentication]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Curricula == null)
            {
                return NotFound();
            }

            var curricula = await _context.Curricula
                .Include(c => c.Profile)
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curricula == null)
            {
                return NotFound();
            }

            return View(curricula);
        }

        // GET: Curricula/Create
        [Authorize("Secretar")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id");
            return View();
        }

        // POST: Curricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfileId,SubjectId,Year,Semester,YearIn,YearOut")] Curricula curricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", curricula.ProfileId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", curricula.SubjectId);
            return View(curricula);
        }

        // GET: Curricula/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Curricula == null)
            {
                return NotFound();
            }

            var curricula = await _context.Curricula.FindAsync(id);
            if (curricula == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", curricula.ProfileId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", curricula.SubjectId);
            return View(curricula);
        }

        // POST: Curricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfileId,SubjectId,Year,Semester,YearIn,YearOut")] Curricula curricula)
        {
            if (id != curricula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculaExists(curricula.Id))
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", curricula.ProfileId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", curricula.SubjectId);
            return View(curricula);
        }

        // GET: Curricula/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Curricula == null)
            {
                return NotFound();
            }

            var curricula = await _context.Curricula
                .Include(c => c.Profile)
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curricula == null)
            {
                return NotFound();
            }

            return View(curricula);
        }

        // POST: Curricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Curricula == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Curricula'  is null.");
            }
            var curricula = await _context.Curricula.FindAsync(id);
            if (curricula != null)
            {
                _context.Curricula.Remove(curricula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurriculaExists(int id)
        {
          return (_context.Curricula?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
