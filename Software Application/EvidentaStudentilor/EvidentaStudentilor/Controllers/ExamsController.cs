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
    public class ExamsController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public ExamsController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: Exams
        [Authentication]
        public async Task<IActionResult> Index()
        {
            var evidentaStudentilorContext = _context.Exams.Include(e => e.Curricula).Include(e => e.Teacher);
            return View(await evidentaStudentilorContext.ToListAsync());
        }

        // GET: Exams/Details/5
        [Authentication]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Curricula)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        [Authorize("Secretar")]
        public IActionResult Create()
        {
            ViewData["CurriculaId"] = new SelectList(_context.Curricula, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CurriculaId,TeacherId,Data,HourIn,HourOut,Room,Closed")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurriculaId"] = new SelectList(_context.Curricula, "Id", "Id", exam.CurriculaId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", exam.TeacherId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        [Authorize("Secretar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id); 
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["CurriculaId"] = new SelectList(_context.Curricula, "Id", "Id", exam.CurriculaId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", exam.TeacherId);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CurriculaId,TeacherId,Data,HourIn,HourOut,Room,Closed")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
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
            ViewData["CurriculaId"] = new SelectList(_context.Curricula, "Id", "Id", exam.CurriculaId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", exam.TeacherId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        [Authorize("Secretar")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Curricula)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
          return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
