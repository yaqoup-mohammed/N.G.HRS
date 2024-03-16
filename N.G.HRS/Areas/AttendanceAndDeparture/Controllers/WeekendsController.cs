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
    public class WeekendsController : Controller
    {
        private readonly AppDbContext _context;

        public WeekendsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceAndDeparture/Weekends
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.weekends.Include(w => w.Periods).Include(w => w.PermanenceModels);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/Weekends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekends = await _context.weekends
                .Include(w => w.Periods)
                .Include(w => w.PermanenceModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weekends == null)
            {
                return NotFound();
            }

            return View(weekends);
        }

        // GET: AttendanceAndDeparture/Weekends/Create
        public IActionResult Create()
        {
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "PeriodsName");
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName");
            return View();
        }

        // POST: AttendanceAndDeparture/Weekends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SaturDay,SunDay,MonDay,Tuesday,Wednesday,Thursday,Friday,PermanenceModelsId,PeriodsId")] Weekends weekends)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weekends);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "PeriodsName", weekends.PeriodsId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", weekends.PermanenceModelsId);
            return View(weekends);
        }

        // GET: AttendanceAndDeparture/Weekends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekends = await _context.weekends.FindAsync(id);
            if (weekends == null)
            {
                return NotFound();
            }
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "PeriodsName", weekends.PeriodsId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", weekends.PermanenceModelsId);
            return View(weekends);
        }

        // POST: AttendanceAndDeparture/Weekends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SaturDay,SunDay,MonDay,Tuesday,Wednesday,Thursday,Friday,PermanenceModelsId,PeriodsId")] Weekends weekends)
        {
            if (id != weekends.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weekends);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekendsExists(weekends.Id))
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
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "PeriodsName", weekends.PeriodsId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", weekends.PermanenceModelsId);
            return View(weekends);
        }

        // GET: AttendanceAndDeparture/Weekends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekends = await _context.weekends
                .Include(w => w.Periods)
                .Include(w => w.PermanenceModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weekends == null)
            {
                return NotFound();
            }

            return View(weekends);
        }

        // POST: AttendanceAndDeparture/Weekends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weekends = await _context.weekends.FindAsync(id);
            if (weekends != null)
            {
                _context.weekends.Remove(weekends);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeekendsExists(int id)
        {
            return _context.weekends.Any(e => e.Id == id);
        }
    }
}
