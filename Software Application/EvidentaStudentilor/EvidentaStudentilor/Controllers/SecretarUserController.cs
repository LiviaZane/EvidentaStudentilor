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
    public class SecretarUserController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public SecretarUserController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: SecretarUser
        [Authorize("Secretar")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var evidentaStudentilorContext = _context.Grades.Include(g => g.Curricula).Include(g => g.Exam).Include(g => g.Student).Include(g => g.Subject).
                Include(g => g.Teacher).Where(x => x.Reexamination == true);
            return View(await evidentaStudentilorContext.ToListAsync());
        }



        // GET: SecretarUser/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["CurriculaId"] = new SelectList(_context.Curricula, "Id", "Id", grade.CurriculaId);
            ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Id", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", grade.TeacherId);
            return View(grade);
        }

        // POST: SecretarUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ExamId,SubjectId,TeacherId,CurriculaId,Year,Semester,FormerGrade,ActualGrade,Reexamination,ApprovedReexam")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Exam nextExam = await _context.Exams.Where(x => x.Closed == false && x.ProfileId == grade.ProfileId && x.SubjectId == grade.SubjectId)
                        .FirstOrDefaultAsync();                                              // try to find a future exam for reexamination

                    if (nextExam != null)                                                    // if exist one exam in the future
                    {
                        grade.ExamId = nextExam.Id;                                          // modify the grade for next exam
                        grade.SubjectId = nextExam.SubjectId;
                        grade.TeacherId = nextExam.TeacherId;
                        grade.CurriculaId = nextExam.CurriculaId;
                        grade.FormerGrade = grade.ActualGrade;
                        grade.ActualGrade = 0;
                        grade.ApprovedReexam = true; 
                        grade.Reexamination = false;

                        _context.Update(grade);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
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
            ViewData["CurriculaId"] = new SelectList(_context.Curricula, "Id", "Id", grade.CurriculaId);
            ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Id", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", grade.TeacherId);
            return View(grade);
        }

        private bool GradeExists(int id)
        {
          return (_context.Grades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
