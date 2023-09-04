using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;

namespace LibraryManagementSystem.Controllers
{
    public class SignUpsController : Controller
    {
        private readonly EntityFramework _context;

        public SignUpsController(EntityFramework context)
        {
            _context = context;
        }

        // GET: SignUps
        public async Task<IActionResult> Index()
        {
            //var model = new SignUp();
            //if (_context != null)
            //{
            //    var data = await _context.SignUp.ToListAsync();
            //    if (data.Count() > 0)
            //    {
            //        return View(data);
            //    }
            //    else
            //    {
            //        return View(model);

            //    }
            //}
            //else
            //{
            //    return View(model);
            //}

            return _context.SignUp != null ?
                        View(await _context.SignUp.ToListAsync()) :
                        Problem("Entity set 'EntityFramework.SignUp'  is null.");
        }

        // GET: SignUps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SignUp == null)
            {
                return NotFound();
            }

            var signUp = await _context.SignUp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signUp == null)
            {
                return NotFound();
            }

            return View(signUp);
        }

        // GET: SignUps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SignUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Gmail,Address,PhoneNo,Gender")] SignUp signUp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signUp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signUp);
        }

        // GET: SignUps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SignUp == null)
            {
                return NotFound();
            }

            var signUp = await _context.SignUp.FindAsync(id);
            if (signUp == null)
            {
                return NotFound();
            }
            return View(signUp);
        }

        // POST: SignUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Gmail,Address,PhoneNo,Gender")] SignUp signUp)
        {
            if (id != signUp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signUp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignUpExists(signUp.Id))
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
            return View(signUp);
        }

        // GET: SignUps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SignUp == null)
            {
                return NotFound();
            }

            var signUp = await _context.SignUp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signUp == null)
            {
                return NotFound();
            }

            return View(signUp);
        }

        // POST: SignUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SignUp == null)
            {
                return Problem("Entity set 'EntityFramework.SignUp'  is null.");
            }
            var signUp = await _context.SignUp.FindAsync(id);
            if (signUp != null)
            {
                _context.SignUp.Remove(signUp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignUpExists(int id)
        {
            return (_context.SignUp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
