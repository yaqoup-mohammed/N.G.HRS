using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.Employees.Controllers
{
    [Area("Employees")]
    public class TrainingCoursesController : Controller
    {
        private readonly AppDbContext _context;

        public TrainingCoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees/TrainingCourses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.trainingCourses.Include(t => t.EmployeeOne);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Employees/TrainingCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingCourses = await _context.trainingCourses
                .Include(t => t.EmployeeOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingCourses == null)
            {
                return NotFound();
            }

            return View(trainingCourses);
        }

        // GET: Employees/TrainingCourses/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: Employees/TrainingCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameCourses,WhereToGetIt,FromDate,ToDate,EmployeeId")] TrainingCourses trainingCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingCourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", trainingCourses.EmployeeId);
            return View(trainingCourses);
        }

        // GET: Employees/TrainingCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingCourses = await _context.trainingCourses.FindAsync(id);
            if (trainingCourses == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", trainingCourses.EmployeeId);
            return View(trainingCourses);
        }

        // POST: Employees/TrainingCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameCourses,WhereToGetIt,FromDate,ToDate,EmployeeId")] TrainingCourses trainingCourses)
        {
            if (id != trainingCourses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingCourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingCoursesExists(trainingCourses.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", trainingCourses.EmployeeId);
            return View(trainingCourses);
        }

        // GET: Employees/TrainingCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingCourses = await _context.trainingCourses
                .Include(t => t.EmployeeOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingCourses == null)
            {
                return NotFound();
            }

            return View(trainingCourses);
        }

        // POST: Employees/TrainingCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingCourses = await _context.trainingCourses.FindAsync(id);
            if (trainingCourses != null)
            {
                _context.trainingCourses.Remove(trainingCourses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingCoursesExists(int id)
        {
            return _context.trainingCourses.Any(e => e.Id == id);
        }
    }
}
