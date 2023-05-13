using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly IScheduleService schedules;

        public SchedulesController(IScheduleService scheduleServices)
        {
            schedules = scheduleServices;
        }

        // GET: Schedules
        [Authentication]                                                               // control acce
        [HttpGet]                                                                      // http method
        [Route("Schedules")]                                                           // atribute routing
        [Route("Schedules/Index")]
        public IActionResult Index()
        {
            return View(schedules.GetSchedule().ToList());
        }

        // GET: Schedules/Details/5
        [Authentication]
        [HttpGet]
        [Route("Schedules/Details")]
        public IActionResult Details(int id)
        {
            if (schedules.GetSchedule() == null)
            {
                return NotFound();
            }

            Schedule schedule = schedules.GetSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Schedules/Create")]
        public IActionResult Create()
        {
            Schedule schedule = new Schedule();
            ViewBag.ProfileId = new SelectList(schedules.GetProfile(), "Id", "Name", schedule.ProfileId);
            return View(schedule);
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProfileId,FileName,Year,Semester")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedules.createSchedule(schedule);
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Schedules/Edit")]
        public IActionResult Edit(int id)
        {
            if (schedules.GetSchedule() == null)
            {
                return NotFound();
            }

            var schedule = schedules.GetSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewBag.ProfileId = new SelectList(schedules.GetProfile(), "Id", "Name", schedule.ProfileId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProfileId,FileName,Year,Semester")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    schedules.updateSchedule(schedule);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Schedules/Delete")]
        public IActionResult Delete(int id)
        {
            if (schedules.GetSchedule() == null)
            {
                return NotFound();
            }

            var schedule = schedules.GetSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (schedules.GetSchedule() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Schedules'  is null.");
            }
            var schedule = schedules.GetSchedule(id);
            if (schedule != null)
            {
                schedules.deleteSchedule(schedule);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return (schedules.GetSchedule(id) == null) ? false : true;
        }
    }
}
