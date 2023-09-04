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
    public class StudentBookEnrollmentsController : Controller
    {
        private readonly EntityFramework _context;

        public StudentBookEnrollmentsController(EntityFramework context)
        {
            _context = context;
        }

        // GET: StudentBookEnrollments
        public async Task<IActionResult> Index()
        {
            var studentData = await _context.Student.ToListAsync();
            var bookData = await _context.Book.ToListAsync();
            var studentBookEnrollment = await _context.StudentBookEnrollment.ToListAsync();
            ViewData["studentData"] = studentData;
            ViewData["bookData"] = bookData;
            ViewData["studentBookEnrollment"] = studentBookEnrollment;
            return View();
        }

        // GET: StudentBookEnrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentBookEnrollment == null)
            {
                return NotFound();
            }

            var studentBookEnrollment = await _context.StudentBookEnrollment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentBookEnrollment == null)
            {
                return NotFound();
            }

            return View(studentBookEnrollment);
        }

        // GET: StudentBookEnrollments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentBookEnrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,BookId,Date")] StudentBookEnrollment studentBookEnrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentBookEnrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentBookEnrollment);
        }

        // GET: StudentBookEnrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentBookEnrollment == null)
            {
                return NotFound();
            }

            var studentBookEnrollment = await _context.StudentBookEnrollment.FindAsync(id);
            if (studentBookEnrollment == null)
            {
                return NotFound();
            }
            return View(studentBookEnrollment);
        }

        // POST: StudentBookEnrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,BookId,Date")] StudentBookEnrollment studentBookEnrollment)
        {
            if (id != studentBookEnrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentBookEnrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentBookEnrollmentExists(studentBookEnrollment.Id))
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
            return View(studentBookEnrollment);
        }

        // GET: StudentBookEnrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentBookEnrollment == null)
            {
                return NotFound();
            }

            var studentBookEnrollment = await _context.StudentBookEnrollment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentBookEnrollment == null)
            {
                return NotFound();
            }

            return View(studentBookEnrollment);
        }

        // POST: StudentBookEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentBookEnrollment == null)
            {
                return Problem("Entity set 'EntityFramework.StudentBookEnrollment'  is null.");
            }
            var studentBookEnrollment = await _context.StudentBookEnrollment.FindAsync(id);
            if (studentBookEnrollment != null)
            {
                _context.StudentBookEnrollment.Remove(studentBookEnrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentBookEnrollmentExists(int id)
        {
            return (_context.StudentBookEnrollment?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost]
        public async Task<IActionResult> saveStudentBookData([FromBody] List<StudentBookEnrollmentViewModel> obtData)
        {
            string message = "Error";
            try
            {
                foreach (var da in obtData)
                {
                    if (!await _context.StudentBookEnrollment.AnyAsync(x => x.StudentId 
                    == da.StudentId &&  x.BookId == da.BookId))
                    {
                        var model = new StudentBookEnrollment();
                        model.BookId = da.BookId;
                        model.StudentId = da.StudentId;
                        model.Date = DateTime.Now;
                        _context.Add(model);
                        await _context.SaveChangesAsync();
                    }

                }
                message = "Added Successfully";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(message);
        }
    }
}
