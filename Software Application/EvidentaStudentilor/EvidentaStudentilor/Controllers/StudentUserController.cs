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
    public class StudentUserController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public StudentUserController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: StudentUser
        [Authorize("Student")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string studentId = HttpContext.Session.GetString("StudTeachID");
            int studId = Int32.Parse(studentId);
            var evidentaStudentilorContext = _context.Grades.Include(g => g.Exam).Include(g => g.Student).Include(g => g.Subject).Include(g => g.Teacher).
                Where(x => x.StudentId == studId).OrderBy(x => x.Year).ThenBy(x => x.Semester);
            return View(await evidentaStudentilorContext.ToListAsync());
        }


        // GET: StudentUser/Edit/5
        [Authorize("Student")]
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

        // POST: StudentUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ExamId,SubjectId,TeacherId,Year,Semester,ActualGrade,Reexamination,ApprovedReexam")] Grade grade)
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
                return RedirectToAction(nameof(Index));
            }
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
