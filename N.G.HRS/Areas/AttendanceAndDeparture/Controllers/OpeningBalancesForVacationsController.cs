using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]
    public class OpeningBalancesForVacationsController : Controller
    {
        private readonly AppDbContext _context;

        public OpeningBalancesForVacationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.openingBalancesForVacations.Include(o => o.Employee).Include(o => o.PublicHolidays);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openingBalancesForVacations = await _context.openingBalancesForVacations
                .Include(o => o.Employee)
                .Include(o => o.PublicHolidays)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (openingBalancesForVacations == null)
            {
                return NotFound();
            }

            return View(openingBalancesForVacations);
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["PublicHolidaysId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName");
            return View();
        }

        // POST: AttendanceAndDeparture/OpeningBalancesForVacations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BalanceYear,Balance,Date,Notes,EmployeeId,PublicHolidaysId")] OpeningBalancesForVacations openingBalancesForVacations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(openingBalancesForVacations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", openingBalancesForVacations.EmployeeId);
            ViewData["PublicHolidaysId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName", openingBalancesForVacations.PublicHolidaysId);
            return View(openingBalancesForVacations);
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openingBalancesForVacations = await _context.openingBalancesForVacations.FindAsync(id);
            if (openingBalancesForVacations == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", openingBalancesForVacations.EmployeeId);
            ViewData["PublicHolidaysId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName", openingBalancesForVacations.PublicHolidaysId);
            return View(openingBalancesForVacations);
        }

        // POST: AttendanceAndDeparture/OpeningBalancesForVacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BalanceYear,Balance,Date,Notes,EmployeeId,PublicHolidaysId")] OpeningBalancesForVacations openingBalancesForVacations)
        {
            if (id != openingBalancesForVacations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(openingBalancesForVacations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpeningBalancesForVacationsExists(openingBalancesForVacations.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", openingBalancesForVacations.EmployeeId);
            ViewData["PublicHolidaysId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName", openingBalancesForVacations.PublicHolidaysId);
            return View(openingBalancesForVacations);
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openingBalancesForVacations = await _context.openingBalancesForVacations
                .Include(o => o.Employee)
                .Include(o => o.PublicHolidays)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (openingBalancesForVacations == null)
            {
                return NotFound();
            }

            return View(openingBalancesForVacations);
        }

        // POST: AttendanceAndDeparture/OpeningBalancesForVacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var openingBalancesForVacations = await _context.openingBalancesForVacations.FindAsync(id);
            if (openingBalancesForVacations != null)
            {
                _context.openingBalancesForVacations.Remove(openingBalancesForVacations);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpeningBalancesForVacationsExists(int id)
        {
            return _context.openingBalancesForVacations.Any(e => e.Id == id);
        }
    }
}
