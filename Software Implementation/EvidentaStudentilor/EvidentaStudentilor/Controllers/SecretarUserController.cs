using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class SecretarUserController : Controller
    {
        private readonly IExamService exams;
        private readonly IGradeService grades;

        public SecretarUserController(IExamService examService, IGradeService gradeService)
        {
            exams = examService;
            grades = gradeService;
        }

        // GET: SecretarUser
        [Authorize("Secretar")]                                                        // control acces
        [HttpGet]                                                                      // http method
        [Route("SecretarUser")]                                                        // atribute routing
        [Route("SecretarUser/Index")]
        public IActionResult Index()
        {
            var evidentaStudentilorContext = grades.GetGrade().Where(x => x.Reexamination == true);
            return View(evidentaStudentilorContext.ToList());
        }



        // GET: SecretarUser/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("SecretarUser/Edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || grades.GetGrade() == null)
            {
                return NotFound();
            }

            var grade = grades.GetGrade().FirstOrDefault(x => x.Id == id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: SecretarUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,StudentId,ExamId,SubjectId,TeacherId,CurriculaId,ProfileId,Year,Semester,FormerGrade,ActualGrade,Reexamination,ApprovedReexam")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    grades.updateGrade(grade);

                    if (grade.ApprovedReexam) 
                    {
                        Exam nextExam = exams.GetExam().FirstOrDefault(x => x.Closed == false && x.ProfileId == grade.ProfileId && x.SubjectId == grade.SubjectId);                                              // try to find a future exam for reexamination
                        int e =0;
                        if (nextExam != null)                                                    // if exist one exam in the future
                        {
                            grade.ExamId = nextExam.Id;                                          // modify the examId, grade...etc, for the next exam
                            grade.SubjectId = nextExam.SubjectId;
                            grade.TeacherId = nextExam.TeacherId;
                            grade.CurriculaId = nextExam.CurriculaId;
                            grade.FormerGrade = grade.ActualGrade;
                            grade.ActualGrade = 0;
                            grade.ApprovedReexam = true;
                            grade.Reexamination = false;

                            grades.updateGrade(grade);
                        }
                        else 
                        {
                            grade.Reexamination = false;
                            grades.updateGrade(grade);
                        }
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
            return View(grade);
        }

        private bool GradeExists(int id)
        {
          return (grades.GetGrade().Any(e => e.Id == id));
        }
    }
}
