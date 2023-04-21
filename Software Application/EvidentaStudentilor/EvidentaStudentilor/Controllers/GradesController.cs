using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.Models;
using System.Diagnostics;
using EvidentaStudentilor.Utilities;

namespace EvidentaStudentilor.Controllers
{
    public class GradesController : Controller
    {
        private readonly EvidentaStudentilorContext _context;
        private int? idExam;

        public GradesController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: Grades
        [Authorize("Teacher")]
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            idExam = id;
            var evidentaStudentilorContext = _context.Grades.Include(g => g.Exam).Include(g => g.Student).Include(g => g.Subject).Include(g => g.Teacher).
                Where(g => g.ExamId == id);
            return View(await evidentaStudentilorContext.ToListAsync());
        }


        // GET: Grades/Edit/5
        [Authorize("Teacher")]
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
            ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Id", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", grade.TeacherId);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ExamId,SubjectId,TeacherId,ActualGrade,Year,Semester,Reexamination,ApprovedReexam")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction("Index", new {ID = grade.ExamId });
            }
            ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Id", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", grade.TeacherId);
            return View(grade);
        }


        public IActionResult BackToExams() 
        {
            return RedirectToAction("Index", "TeacherUser");
        }

        private bool GradeExists(int id)
        {
          return (_context.Grades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
