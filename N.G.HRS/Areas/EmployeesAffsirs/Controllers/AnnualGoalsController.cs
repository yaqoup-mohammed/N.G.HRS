using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class AnnualGoalsController : Controller
    {
        private readonly AppDbContext _context;

        public AnnualGoalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EmployeesAffsirs/AnnualGoals
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AnnualGoals.Include(a => a.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/AnnualGoals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualGoals = await _context.AnnualGoals
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annualGoals == null)
            {
                return NotFound();
            }

            return View(annualGoals);
        }

        // GET: EmployeesAffsirs/AnnualGoals/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: EmployeesAffsirs/AnnualGoals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,EmployeeId,Notes,Goals")] AnnualGoals annualGoals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(annualGoals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", annualGoals.EmployeeId);
            return View(annualGoals);
        }

        // GET: EmployeesAffsirs/AnnualGoals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualGoals = await _context.AnnualGoals.FindAsync(id);
            if (annualGoals == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", annualGoals.EmployeeId);
            return View(annualGoals);
        }

        // POST: EmployeesAffsirs/AnnualGoals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,EmployeeId,Notes,Goals")] AnnualGoals annualGoals)
        {
            if (id != annualGoals.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(annualGoals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnualGoalsExists(annualGoals.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", annualGoals.EmployeeId);
            return View(annualGoals);
        }

        // GET: EmployeesAffsirs/AnnualGoals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualGoals = await _context.AnnualGoals
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annualGoals == null)
            {
                return NotFound();
            }

            return View(annualGoals);
        }

        // POST: EmployeesAffsirs/AnnualGoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var annualGoals = await _context.AnnualGoals.FindAsync(id);
            if (annualGoals != null)
            {
                _context.AnnualGoals.Remove(annualGoals);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnualGoalsExists(int id)
        {
            return _context.AnnualGoals.Any(e => e.Id == id);
        }
    }
}
