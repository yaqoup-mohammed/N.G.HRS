using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class PublicHolidaysController : Controller
    {
        private readonly AppDbContext _context;

        public PublicHolidaysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/PublicHolidays
        public async Task<IActionResult> Index()
        {
            return View(await _context.publicHolidays.ToListAsync());
        }

        // GET: GeneralConfiguration/PublicHolidays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicHolidays = await _context.publicHolidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicHolidays == null)
            {
                return NotFound();
            }

            return View(publicHolidays);
        }

        // GET: GeneralConfiguration/PublicHolidays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/PublicHolidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HolidayName,Balance,Paid,DayCount,RotationDuration,Notes")] PublicHolidays publicHolidays)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicHolidays);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicHolidays);
        }

        // GET: GeneralConfiguration/PublicHolidays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicHolidays = await _context.publicHolidays.FindAsync(id);
            if (publicHolidays == null)
            {
                return NotFound();
            }
            return View(publicHolidays);
        }

        // POST: GeneralConfiguration/PublicHolidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HolidayName,Balance,Paid,DayCount,RotationDuration,Notes")] PublicHolidays publicHolidays)
        {
            if (id != publicHolidays.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicHolidays);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicHolidaysExists(publicHolidays.Id))
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
            return View(publicHolidays);
        }

        // GET: GeneralConfiguration/PublicHolidays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicHolidays = await _context.publicHolidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicHolidays == null)
            {
                return NotFound();
            }

            return View(publicHolidays);
        }

        // POST: GeneralConfiguration/PublicHolidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicHolidays = await _context.publicHolidays.FindAsync(id);
            if (publicHolidays != null)
            {
                _context.publicHolidays.Remove(publicHolidays);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicHolidaysExists(int id)
        {
            return _context.publicHolidays.Any(e => e.Id == id);
        }
    }
}
