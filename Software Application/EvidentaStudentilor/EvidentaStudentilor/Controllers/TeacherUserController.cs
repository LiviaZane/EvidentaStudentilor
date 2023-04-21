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
    public class TeacherUserController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public TeacherUserController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: TeacherUser
        [Authorize("Teacher")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string teacherId = HttpContext.Session.GetString("StudTeachID");
            int teachId = Int32.Parse(teacherId);
            var evidentaStudentilorContext = _context.Exams.Include(e => e.Profile).Include(e => e.Subject).Include(e => e.Teacher).Where(x => x.TeacherId == teachId);
            return View(await evidentaStudentilorContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult StorageExams(bool isNumber) 
        {
            string teacherId = HttpContext.Session.GetString("StudTeachID");
            int teachId = Int32.Parse(teacherId);
            List<Exam> evidentaStudentilorContext;
            if (isNumber)
            {
                evidentaStudentilorContext = _context.Exams.Include(e => e.Profile).Include(e => e.Subject).Include(e => e.Teacher).Where(x => x.TeacherId == teachId && x.Closed == false).ToList();
            }
            else
            {
                evidentaStudentilorContext = _context.Exams.Include(e => e.Profile).Include(e => e.Subject).Include(e => e.Teacher).Where(x => x.TeacherId == teachId).ToList();
            }
            return Json(evidentaStudentilorContext);
        }

        // GET: TeacherUser/Details/5
        [Authorize("Teacher")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            ViewBag.studentsNo = _context.Grades.Where(x => x.ExamId == id).Count();
           
            return View();
        }

        // GET: TeacherUser/Open
        public IActionResult Open(int? id)
        {
            return RedirectToAction("Index", "Grades", new {ID = id});
        }


        // GET: TeacherUser/Edit/5
        [Authorize("Teacher")]
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

        // POST: TeacherUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfileId,SubjectId,TeacherId,StudyYear,Data,HourIn,HourOut,Room,Closed")] Exam exam)
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

        private bool ExamExists(int id)
        {
          return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
