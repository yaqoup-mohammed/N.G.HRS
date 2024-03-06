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
    public class PermanenceModelsController : Controller
    {
        private readonly AppDbContext _context;

        public PermanenceModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceAndDeparture/PermanenceModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.permanenceModels.ToListAsync());
        }

        // GET: AttendanceAndDeparture/PermanenceModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permanenceModels = await _context.permanenceModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permanenceModels == null)
            {
                return NotFound();
            }

            return View(permanenceModels);
        }

        // GET: AttendanceAndDeparture/PermanenceModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AttendanceAndDeparture/PermanenceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PermanenceName,FromDate,ToDate,FlexibleWorkingHours,WorkBetweenTwoDays,FromTime,ToTime,HoursOfWorks,AddAttendanceAndDeparturePermission,AllowanceForLateAttendance,EarlyDeparturePermission,Notes")] PermanenceModels permanenceModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permanenceModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permanenceModels);
        }

        // GET: AttendanceAndDeparture/PermanenceModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permanenceModels = await _context.permanenceModels.FindAsync(id);
            if (permanenceModels == null)
            {
                return NotFound();
            }
            return View(permanenceModels);
        }

        // POST: AttendanceAndDeparture/PermanenceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PermanenceName,FromDate,ToDate,FlexibleWorkingHours,WorkBetweenTwoDays,FromTime,ToTime,HoursOfWorks,AddAttendanceAndDeparturePermission,AllowanceForLateAttendance,EarlyDeparturePermission,Notes")] PermanenceModels permanenceModels)
        {
            if (id != permanenceModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permanenceModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermanenceModelsExists(permanenceModels.Id))
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
            return View(permanenceModels);
        }

        // GET: AttendanceAndDeparture/PermanenceModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permanenceModels = await _context.permanenceModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permanenceModels == null)
            {
                return NotFound();
            }

            return View(permanenceModels);
        }

        // POST: AttendanceAndDeparture/PermanenceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permanenceModels = await _context.permanenceModels.FindAsync(id);
            if (permanenceModels != null)
            {
                _context.permanenceModels.Remove(permanenceModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermanenceModelsExists(int id)
        {
            return _context.permanenceModels.Any(e => e.Id == id);
        }
    }
}
