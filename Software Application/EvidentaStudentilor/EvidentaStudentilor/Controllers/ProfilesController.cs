using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.Utilities;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly IProfileService profiles;

        public ProfilesController(IProfileService profileService)
        {
            profiles = profileService;
        }

        // GET: Profiles
        [Authentication]                                                               // control acces
        [HttpGet]                                                                      // http method
        [Route("Profiles")]                                                            // atribute routing
        [Route("Profiles/Index")]
        public IActionResult Index()
        {
              return profiles.GetProfile() != null ? 
                          View(profiles.GetProfile().ToList()) :
                          Problem("Entity set 'EvidentaStudentilorContext.Profiles'  is null.");
        }

        // GET: Profiles/Details/5
        [Authentication]
        [HttpGet]
        [Route("Profiles/Details")]
        public IActionResult Details(int id)
        {
            if (profiles.GetProfile() == null)
            {
                return NotFound();
            }

            Profile profile = profiles.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Profiles/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                profiles.createProfile(profile);
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // GET: Profiles/Edit/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Profiles/Edit")]
        public IActionResult Edit(int id)
        {
            if (id == null || profiles.GetProfile() == null)
            {
                return NotFound();
            }

            Profile profile = profiles.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Profile profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    profiles.updateProfile(profile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
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
            return View(profile);
        }

        // GET: Profiles/Delete/5
        [Authorize("Secretar")]
        [HttpGet]
        [Route("Profiles/Delete")]
        public IActionResult Delete(int id)
        {
            if (id == null || profiles.GetProfile() == null)
            {
                return NotFound();
            }

            Profile profile = profiles.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [Authorize("Secretar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (profiles.GetProfile() == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Profiles'  is null.");
            }
            Profile profile = profiles.GetProfile(id);
            if (profile != null)
            {
                profiles.deleteProfile(profile);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return (profiles.GetProfile(id) == null) ? false : true;
        }
    }
}
