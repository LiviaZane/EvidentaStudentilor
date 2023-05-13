using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvidentaStudentilor.Controllers
{
    public class ExamsController : Controller
    {
        private readonly IExamService exams;
        public ExamsController(IExamService examService)
        {
            exams = examService;
        }

        // GET: Exams
        [Authentication]                                                       // control acces
        [HttpGet]                                                              // http method
        [Route("Exams")]                                                       // atribute routing
        [Route("Exams/Index")]
        public IActionResult Index()
        {
            return View(exams.GetExam().ToList());
        }

        // GET: Exams/Details/5
        [Authentication]
        [HttpGet]
        [Route("Exams/Details")]
        public IActionResult Details(int id)
        {
            if (exams.GetExam() == null)
            {
                return NotFound();
            }

            var exam = exams.GetExam(id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Exams/Create")]
        public IActionResult Create(int id)
        {
            Exam exam = new Exam();
            Curricula curricula = exams.GetCurricula(id);
            if (curricula != null)
            {
                exam.CurriculaId = curricula.Id;
                exam.ProfileId = curricula.ProfileId;
                exam.SubjectId = curricula.SubjectId;
                exam.StudyYear = curricula.Year;
                exam.Semester = curricula.Semester;
                ViewBag.TeacherId = new SelectList(exams.GetTeacher(), "Id", "Name", exam.TeacherId);
            }
            return View(exam);
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProfileId,CurriculaId,SubjectId,TeacherId,StudyYear,Semester,Data,HourIn,HourOut,Room,Closed")] Exam exam, [FromServices]IGradeService grades)
        {
            if (ModelState.IsValid)
            {
                exam.Id = 0;
                exams.createExam(exam);
                var _students = exams.GetStudent().Where(s => s.ProfileId == exam.ProfileId && s.CurrentYear == exam.StudyYear).ToList();
                var _grades = grades.GetGrade().ToList();
                foreach (Student stud in _students)
                {
                    var foundedStudent = _grades.Where(x => x.StudentId == stud.Id && x.SubjectId == exam.SubjectId).FirstOrDefault();

                    Grade grade = new Grade();
                    grade.StudentId = stud.Id;
                    grade.ExamId = exam.Id;
                    grade.SubjectId = exam.SubjectId;
                    grade.ProfileId = exam.ProfileId;
                    grade.CurriculaId = exam.CurriculaId;
                    grade.TeacherId = exam.TeacherId;
                    grade.Year = exam.StudyYear;
                    grade.Semester = exam.Semester;
                    grade.FormerGrade = 0;
                    grade.ActualGrade = 0;
                    grade.Reexamination = false;
                    grade.ApprovedReexam = false;

                    if (foundedStudent != null && foundedStudent.ActualGrade < 5)                // for students which not graduate the exam in the present year
                    {
                        grade.FormerGrade = foundedStudent.ActualGrade;
                        grades.deleteGrade(foundedStudent);
                        grades.createGrade(grade);
                    }
                    if (foundedStudent == null) 
                    {
                        grades.createGrade(grade);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }

        // GET: Exams/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Exams/Edit")]
        public IActionResult Edit(int id)
        {
            if (exams.GetExam() == null)
            {
                return NotFound();
            }

            var exam = exams.GetExam(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProfileId,SubjectId,TeacherId,StudyYear,Semester,Data,HourIn,HourOut,Room,Closed")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    exams.updateExam(exam);
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
            return View(exam);
        }

        // GET: Exams/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Exams/Delete")]
        public IActionResult Delete(int id)
        {
            if (exams.GetExam() == null)
            {
                return NotFound();
            }

            var exam = exams.GetExam(id);
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
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, [FromServices] IGradeService grades)
        {
            if (exams.GetExam() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Exams'  is null.");
            }
            Exam exam = exams.GetExam(id);
            if (exam != null)
            {
                var _grades = grades.GetGrade().Where(g => g.ExamId == id);
                foreach (Grade item in _grades)
                {
                    grades.deleteGrade(item);
                }

                exams.deleteExam(exam);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return (exams.GetExam(id) == null) ? false : true;
        }
    }
}