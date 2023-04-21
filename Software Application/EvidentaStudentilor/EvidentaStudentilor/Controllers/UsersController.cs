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
    public class UsersController : Controller
    {
        private readonly EvidentaStudentilorContext _context;

        public UsersController(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _context.Users.ToListAsync();
            if (HttpContext.Session.GetString("UserRole") == "Administrator")
            {
                list = await _context.Users.Include(x => x.Role).Where(x => x.Role.Id == x.RoleId && (x.Role.Name == "Secretar" || x.Role.Name == "Administrator")).ToListAsync();
                // join Roles table with Users with RoleId foreign key to display Role.Name instead Users.Id
            }
            else
            {
                list = await _context.Users.Include(x => x.Role).Where(x => x.Role.Id == x.RoleId &&
                        (x.Role.Name == "Student" || x.Role.Name == "Teacher")).ToListAsync();
            }


            if (_context.Students == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Students'  is null.");
            }

            return View(list);
        }

        // GET: Users/Details/5
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public IActionResult Create()
        {
            User user = new User();
            var list = _context.Roles.ToList();
            if (HttpContext.Session.GetString("UserRole") == "Administrator")
            {
                list = list.Where(x => x.Name == "Administrator" || x.Name == "Secretar").ToList();
            }
            else
            {
                list = list.Where(x => x.Name == "Student" || x.Name == "Teacher").ToList();
            }
            ViewBag.RoleId = new SelectList(list, "Id", "Name", user.RoleId);
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize("Secretar", "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,Email,Paswword")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = 0;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var list = _context.Roles.ToList();
            if (HttpContext.Session.GetString("UserRole") == "Administrator")
            {
                list = list.Where(x => x.Name == "Administrator" || x.Name == "Secretar").ToList();
            }
            else
            {
                list = list.Where(x => x.Name == "Student" || x.Name == "Teacher").ToList();
            }
            ViewBag.RoleId = new SelectList(list, "Id", "Name", user.RoleId);   // used ViewBag to display Roles.Name in SelectList
            return View(user);                                                            // and save Role.Id (from SelectList) instead User.Id
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,Email,Paswword")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize("Secretar", "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'EvidentaStudentilorContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
