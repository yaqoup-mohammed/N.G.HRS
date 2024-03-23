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
    public class MonthsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Months> _monthsRepository;

        public MonthsController(AppDbContext context, IRepository<Months> monthsRepository)
        {
            _context = context;
            _monthsRepository = monthsRepository;
        }

        // GET: AttendanceAndDeparture/Months
        public async Task<IActionResult> Index()
        {
            return View(await _monthsRepository.GetAllAsync());
        }

        // GET: AttendanceAndDeparture/Months/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var months = await _context.months
                .FirstOrDefaultAsync(m => m.Id == id);
            if (months == null)
            {
                return NotFound();
            }

            return View(months);
        }

        // GET: AttendanceAndDeparture/Months/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AttendanceAndDeparture/Months/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Month,StartDate,EndDate,Closed")] Months months)
        {
            if (ModelState.IsValid)
            {
                await _monthsRepository.AddAsync(months);
                return RedirectToAction(nameof(Index));
            }
            return View(months);
        }

        // GET: AttendanceAndDeparture/Months/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var months = await _monthsRepository.GetByIdAsync(id);
            if (months == null)
            {
                return NotFound();
            }
            return View(months);
        }

        // POST: AttendanceAndDeparture/Months/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Month,StartDate,EndDate,Closed")] Months months)
        {
            if (id != months.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _monthsRepository.UpdateAsync(months);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonthsExists(months.Id))
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
            return View(months);
        }

        // GET: AttendanceAndDeparture/Months/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var months = await _context.months
                .FirstOrDefaultAsync(m => m.Id == id);
            if (months == null)
            {
                return NotFound();
            }

            return View(months);
        }

        // POST: AttendanceAndDeparture/Months/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var months = await _monthsRepository.GetByIdAsync(id);
            if (months != null)
            {
                await _monthsRepository.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonthsExists(int id)
        {
            return _context.months.Any(e => e.Id == id);
        }
    }
}
