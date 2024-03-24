using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]
    public class OpeningBalancesForVacationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<OpeningBalancesForVacations> _openingBalancesForVacationsRepository;

        public OpeningBalancesForVacationsController(AppDbContext context, IRepository<OpeningBalancesForVacations> openingBalancesForVacationsRepository)
        {
            _context = context;
            _openingBalancesForVacationsRepository = openingBalancesForVacationsRepository;
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
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
 
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
                await PopulateDropdownListsAsync();

                _openingBalancesForVacationsRepository.AddAsync(openingBalancesForVacations);
                return RedirectToAction(nameof(Index));
            }
            return View(openingBalancesForVacations);
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var openingBalancesForVacations = await _openingBalancesForVacationsRepository.GetByIdAsync(id);
            if (openingBalancesForVacations == null)
            {
                return NotFound();
            }
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
                    await PopulateDropdownListsAsync();

                    _openingBalancesForVacationsRepository.UpdateAsync(openingBalancesForVacations);
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
            var openingBalancesForVacations = await _openingBalancesForVacationsRepository.GetByIdAsync(id);
            if (openingBalancesForVacations != null)
            {
                _openingBalancesForVacationsRepository.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpeningBalancesForVacationsExists(int id)
        {
            return _context.openingBalancesForVacations.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {


            //=========================================================
            var employee = await _context.employee.ToListAsync();
            ViewData["EmployeeId"] = new SelectList(employee, "Id", "EmployeeName");
            //============================================================
            var permanance = await _context.publicHolidays.ToListAsync();
            ViewData["publicHolidays"] = new SelectList(permanance, "Id", "HolidayName");



        }
    }
}
