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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var evidentaStudentilorContext = _context.Exams.Include(e => e.Profile).Include(e => e.Subject).Include(e => e.Teacher);
            return View(await evidentaStudentilorContext.ToListAsync());
        }

        // GET: Exams/Details/5
        [Authentication]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Profile)
                .Include(e => e.Subject)
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
        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            Exam exam = new Exam();
            var curricula = await _context.Curricula.Where(c => c.Id == id).FirstOrDefaultAsync();
            if(curricula != null)
            {
                exam.CurriculaId = curricula.Id;
                exam.ProfileId = curricula.ProfileId;
                exam.SubjectId = curricula.SubjectId;
                exam.StudyYear = curricula.Year;
                exam.Semester = curricula.Semester;
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id");
            return View(exam);
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfileId,CurriculaId,SubjectId,TeacherId,StudyYear,Semester,Data,HourIn,HourOut,Room,Closed")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                exam.Id = 0;
                _context.Add(exam);
                await _context.SaveChangesAsync();
              
                var students = await _context.Students.Where(s => s.ProfileId == exam.ProfileId && s.CurrentYear == exam.StudyYear).ToListAsync();
                var grades = await _context.Grades.ToListAsync();
                foreach (Student stud in students)
                {
                    var foundedStudent = grades.Where(x => x.StudentId == stud.Id && x.SubjectId == exam.SubjectId).FirstOrDefault();
                    
                    if (foundedStudent != null && foundedStudent.ActualGrade < 5)                // for students which not graduate the exam in the present year
                    {
                        Grade grade = new Grade();
                        //grade.Id = foundedStudent.Id;
                        grade.StudentId = foundedStudent.StudentId;
                        grade.ExamId = exam.Id;
                        grade.SubjectId = exam.SubjectId;
                        grade.ProfileId = exam.ProfileId;
                        grade.CurriculaId = exam.CurriculaId;
                        grade.TeacherId = exam.TeacherId;
                        grade.Year = exam.StudyYear;
                        grade.Semester = exam.Semester;
                        grade.FormerGrade = foundedStudent.ActualGrade;
                        grade.ActualGrade = 0;
                        grade.Reexamination = false;
                        grade.ApprovedReexam = true;
                        //_context.Update(grade);
                        _context.Grades.Remove(foundedStudent);
                        //await _context.SaveChangesAsync();
                        _context.Grades.AddAsync(grade);
                        await _context.SaveChangesAsync();
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", exam.ProfileId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", exam.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", exam.TeacherId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", exam.ProfileId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", exam.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", exam.TeacherId);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfileId,SubjectId,TeacherId,StudyYear,Semester,Data,HourIn,HourOut,Room,Closed")] Exam exam)
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", exam.ProfileId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", exam.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", exam.TeacherId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Profile)
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }
            if (exam.Closed == true)
            {
                return RedirectToAction(nameof(Index));
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
                var grades = _context.Grades.Where(g => g.ExamId == id);
                foreach (Grade item in grades) 
                { 
                    _context.Grades.Remove(item);
                }

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
