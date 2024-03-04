using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class BasicDataForWagesAndSalariesController : Controller
    {
        private readonly AppDbContext _context;

        public BasicDataForWagesAndSalariesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.basicDataForWagesAndSalaries.ToListAsync());
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForWagesAndSalaries = await _context.basicDataForWagesAndSalaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basicDataForWagesAndSalaries == null)
            {
                return NotFound();
            }

            return View(basicDataForWagesAndSalaries);
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalariesAndWages/BasicDataForWagesAndSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberOfMonthsDays,AbsencePerHour,DelayPerHour,OneFingerPrintPerHourDelay,FromDate,ToDate,TypeOfWage,Notes")] BasicDataForWagesAndSalaries basicDataForWagesAndSalaries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basicDataForWagesAndSalaries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(basicDataForWagesAndSalaries);
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForWagesAndSalaries = await _context.basicDataForWagesAndSalaries.FindAsync(id);
            if (basicDataForWagesAndSalaries == null)
            {
                return NotFound();
            }
            return View(basicDataForWagesAndSalaries);
        }

        // POST: SalariesAndWages/BasicDataForWagesAndSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberOfMonthsDays,AbsencePerHour,DelayPerHour,OneFingerPrintPerHourDelay,FromDate,ToDate,TypeOfWage,Notes")] BasicDataForWagesAndSalaries basicDataForWagesAndSalaries)
        {
            if (id != basicDataForWagesAndSalaries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basicDataForWagesAndSalaries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasicDataForWagesAndSalariesExists(basicDataForWagesAndSalaries.Id))
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
            return View(basicDataForWagesAndSalaries);
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForWagesAndSalaries = await _context.basicDataForWagesAndSalaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basicDataForWagesAndSalaries == null)
            {
                return NotFound();
            }

            return View(basicDataForWagesAndSalaries);
        }

        // POST: SalariesAndWages/BasicDataForWagesAndSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basicDataForWagesAndSalaries = await _context.basicDataForWagesAndSalaries.FindAsync(id);
            if (basicDataForWagesAndSalaries != null)
            {
                _context.basicDataForWagesAndSalaries.Remove(basicDataForWagesAndSalaries);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasicDataForWagesAndSalariesExists(int id)
        {
            return _context.basicDataForWagesAndSalaries.Any(e => e.Id == id);
        }
    }
}
