using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class StudentUserController : Controller
    {
        private readonly IGradeService grades;

        public StudentUserController(IGradeService gradeService)
        {
            grades = gradeService;
        }

        // GET: StudentUser
        [Authorize("Student")]                                                         // control acces
        [HttpGet]                                                                      // http method
        [Route("StudentUser")]                                                         // atribute routing
        [Route("StudentUser/Index")]
        public IActionResult Index()
        {
            string studentId = HttpContext.Session.GetString("StudTeachID");
            var studId = Int32.Parse(studentId);
            var evidentaStudentilorContext = grades.GetGrade().Where(x => x.StudentId == studId).OrderBy(x => x.Year).ThenBy(x => x.Semester);
            return View(evidentaStudentilorContext.ToList());
        }


        // GET: StudentUser/Edit/5
        [Authorize("Student")]
        [HttpGet]
        [Route("StudentUser/Edit")]
        public async Task<IActionResult> Edit(int? id)
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

        // POST: StudentUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CurriculaId, ProfileId,ExamId,SubjectId,TeacherId,Year,Semester,ActualGrade,Reexamination,ApprovedReexam")] Grade grade)
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
