using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class TeacherUserController : Controller
    {
        private readonly IExamService exams;
        private readonly IGradeService grades;

        public TeacherUserController(IExamService examService, IGradeService gradeService)
        {
            exams = examService;
            grades = gradeService;
        }

        // GET: TeacherUser
        [Authorize("Teacher")]                                                         // control acces
        [HttpGet]                                                                      // http method
        [Route("TeacherUser")]                                                         // atribute routing
        [Route("TeacherUser/Index")]
        public IActionResult Index()
        {
            string teacherId = HttpContext.Session.GetString("StudTeachID");
            var teachId = Int32.Parse(teacherId);
            var evidentaStudentilorContext = exams.GetExam().Where(x => x.TeacherId == teachId && x.Closed == false);
            return View(evidentaStudentilorContext.ToList());
        }

        //[Authorize("Teacher")]
        //[HttpPost]
        [Route("TeacherUser/StorageExams")]
        public IActionResult StorageExams(bool myData)           // function for AJAX 
        {
            string teacherId = HttpContext.Session.GetString("StudTeachID");
            int teachId = Int32.Parse(teacherId);
            List<Exam> evidentaStudentilorContext;
            if (myData)
            {
                evidentaStudentilorContext = exams.GetExam().Where(x => x.TeacherId == teachId && x.Closed == false).ToList();
            }
            else
            {
                evidentaStudentilorContext = exams.GetExam().Where(x => x.TeacherId == teachId).ToList();
            }
            return Json(evidentaStudentilorContext);
        }

        // GET: TeacherUser/Details/5
        [Authorize("Teacher")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || exams.GetExam() == null)
            {
                return NotFound();
            }

            ViewBag.studentsNo = grades.GetGrade().Where(x => x.ExamId == id).Count();
           
            return View();
        }

        // GET: TeacherUser/Open
        [Authorize("Teacher")]
        [HttpGet]
        public IActionResult Open(int? id)
        {
            return RedirectToAction("Index", "Grades", new {ID = id});
        }


        // GET: TeacherUser/Edit/5
        [Authorize("Teacher")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || exams.GetExam() == null)
            {
                return NotFound();
            }

            var exam = exams.GetExam().FirstOrDefault(x => x.Id == id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        // POST: TeacherUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProfileId,CurriculaId,SubjectId,TeacherId,StudyYear,Semester,Data,HourIn,HourOut,Room,Closed")] Exam exam)
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

        private bool ExamExists(int id)
        {
          return (exams.GetExam().Any(e => e.Id == id));
        }
    }
}
