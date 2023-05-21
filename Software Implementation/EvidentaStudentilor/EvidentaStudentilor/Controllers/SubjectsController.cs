using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectService subjects;

        public SubjectsController(ISubjectService subjectService)
        {
            subjects = subjectService;
        }

        // GET: Subjects
        [Authentication]                                                               // control acces
        [HttpGet]                                                                      // http method
        [Route("Subjects")]                                                            // atribute routing
        [Route("Subjects/Index")]
        public IActionResult Index()
        {
              return subjects.GetSubject() != null ? 
                          View(subjects.GetSubject().ToList()) :
                          Problem("Entity set 'EvidentaStudentilorContext.Subjects'  is null.");
        }

        // GET: Subjects/Details/5
        [Authentication]
        [HttpGet]
        [Route("Subjects/Details")]
        public IActionResult Details(int id)
        {
            if (subjects.GetSubject() == null)
            {
                return NotFound();
            }

            Subject subject = subjects.GetSubject(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Subjects/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,CourseHours,SeminarHours,LaboratoryHours,ProjectHours,ECP,Credits")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                subjects.createSubject(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Subjects/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            if (subjects.GetSubject() == null)
            {
                return NotFound();
            }

            var subject = subjects.GetSubject(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,CourseHours,SeminarHours,LaboratoryHours,ProjectHours,ECP,Credits")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    subjects.updateSubject(subject);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
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
            return View(subject);
        }

        // GET: Subjects/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Subjects/Delete")]
        public IActionResult Delete(int id)
        {
            if (subjects.GetSubject() == null)
            {
                return NotFound();
            }

            Subject subject = subjects.GetSubject(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (subjects.GetSubject() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Subjects'  is null.");
            }
            Subject subject = subjects.GetSubject(id);
            if (subject != null)
            {
                subjects.deleteSubject(subject);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return (subjects.GetSubject(id) == null) ? false : true;
        }
    }
}
