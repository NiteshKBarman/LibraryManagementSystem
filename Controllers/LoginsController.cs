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
    public class LoginsController : Controller
    {
        private readonly EntityFramework _context;

        public LoginsController(EntityFramework context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            return View();
            //return _context.Login != null ? 
            //            View(await _context.Login.ToListAsync()) :
            //            Problem("Entity set 'EntityFramework.Login'  is null.");
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gmail,Passowrd")] Login login)
        {
            if (ModelState.IsValid)
            {
                var dataValid = await _context.SignUp.FirstOrDefaultAsync
                    (x => x.Gmail.ToLower() ==
                login.Gmail.ToLower() && x.Password.ToLower() ==
                login.Passowrd.ToLower());
                if (dataValid != null)
                {
                    return RedirectToAction( "Index", "Books");
                }
                else
                {
                    return RedirectToAction("Create","Signups");
                }
            }
            return View(login);
        }

    }
}
