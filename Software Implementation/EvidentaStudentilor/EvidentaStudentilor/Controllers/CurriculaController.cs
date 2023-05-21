using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvidentaStudentilor.Controllers
{
    public class CurriculaController : Controller
    {
        private readonly ICurriculaService _curricula;

        public CurriculaController(ICurriculaService curriculaService)
        {
            _curricula = curriculaService;
        }

        [HttpGet]
        [Authorize("Secretar")]
        public IActionResult CreateExam(int? id)
        {
            return RedirectToAction("Create", "Exams", new { ID = id });
        }

        // GET: Curricula
        [HttpGet]                                                             // http method
        [Authentication]                                                      // control acces
        [Route("Curricula")]                                                  // atribute routing
        [Route("Curricula/Index")]
        public IActionResult Index()
        {
            return View(_curricula.GetCurricula().ToList());
        }

        // GET: Curricula/Details/5
        [HttpGet]
        [Authentication]
        [Route("Curricula/Details")]
        public IActionResult Details(int id)
        {
            if (_curricula.GetCurricula() == null)
            {
                return NotFound();
            }

            var curricula = _curricula.GetCurricula(id);
            if (curricula == null)
            {
                return NotFound();
            }

            return View(curricula);
        }

        // GET: Curricula/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Curricula/Create")]
        public IActionResult Create()
        {
            Curricula curricula = new Curricula();
            ViewBag.ProfileId = new SelectList(_curricula.GetProfile(), "Id", "Name", curricula.ProfileId);
            ViewBag.SubjectId = new SelectList(_curricula.GetSubject(), "Id", "Name", curricula.ProfileId);
            return View(curricula);
        }

        // POST: Curricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProfileId,SubjectId,Year,Semester,YearIn,YearOut")] Curricula curricula)
        {
            if (ModelState.IsValid)
            {
                _curricula.createCurricula(curricula);
                return RedirectToAction(nameof(Index));
            }
            return View(curricula);
        }

        // GET: Curricula/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Curricula/Edit")]
        public IActionResult Edit(int id)
        {
            if (_curricula.GetCurricula() == null)
            {
                return NotFound();
            }

            var curricula = _curricula.GetCurricula(id);
            if (curricula == null)
            {
                return NotFound();
            }
            ViewBag.ProfileId = new SelectList(_curricula.GetProfile(), "Id", "Name", curricula.ProfileId);
            ViewBag.SubjectId = new SelectList(_curricula.GetSubject(), "Id", "Name", curricula.ProfileId);
            return View(curricula);
        }

        // POST: Curricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProfileId,SubjectId,Year,Semester,YearIn,YearOut")] Curricula curricula)
        {
            if (id != curricula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _curricula.updateCurricula(curricula);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculaExists(curricula.Id))
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
            return View(curricula);
        }

        // GET: Curricula/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Curricula/Delete")]
        public IActionResult Delete(int id)
        {
            if (_curricula.GetCurricula() == null)
            {
                return NotFound();
            }

            Curricula curricula = _curricula.GetCurricula(id);
            if (curricula == null)
            {
                return NotFound();
            }

            return View(curricula);
        }

        // POST: Curricula/Delete/5
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_curricula.GetCurricula() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Curricula'  is null.");
            }
            Curricula curricula = _curricula.GetCurricula(id);
            if (curricula != null)
            {
                _curricula.deleteCurricula(curricula);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CurriculaExists(int id)
        {
            return (_curricula.GetCurricula(id) == null) ? false : true;
        }
    }
}
