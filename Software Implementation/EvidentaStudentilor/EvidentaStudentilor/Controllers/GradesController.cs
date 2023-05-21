using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class GradesController : Controller
    {
        private readonly IGradeService grades;

        public GradesController(IGradeService gradeService)
        {
            grades = gradeService;
        }

        // GET: Grades
        [Authorize("Teacher")]                                                       // control acces
        [HttpGet]                                                                    // http method
        [Route("Grades")]                                                            // atribute routing
        [Route("Grades/Index")]
        public IActionResult Index(int id)
        {
            return View(grades.GetGradeOfExam(id).ToList());
        }


        // GET: Grades/Edit/5
        [Authorize("Teacher")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (grades.GetGrade() == null)
            {
                return NotFound();
            }

            Grade grade = grades.GetGrade(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,StudentId,ProfileId,CurriculaId,ExamId,SubjectId,TeacherId,FormerGrade,ActualGrade,Year,Semester,Reexamination,ApprovedReexam")] Grade grade)
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
            return View(grade);
        }


        public IActionResult BackToExams() 
        {
            return RedirectToAction("Index", "TeacherUser");
        }

        private bool GradeExists(int id)
        {
            return (grades.GetGrade(id) == null) ? false : true;
        }
    }
}
