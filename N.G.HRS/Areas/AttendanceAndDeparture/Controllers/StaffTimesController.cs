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
    public class StaffTimesController : Controller
    {
        private readonly AppDbContext _context;

        public StaffTimesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceAndDeparture/StaffTimes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.staffTimes.Include(s => s.Employee).Include(s => s.PermanenceModels).Include(s => s.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/StaffTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffTime = await _context.staffTimes
                .Include(s => s.Employee)
                .Include(s => s.PermanenceModels)
                .Include(s => s.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffTime == null)
            {
                return NotFound();
            }

            return View(staffTime);
        }

        // GET: AttendanceAndDeparture/StaffTimes/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName");
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName");
            return View();
        }

        // POST: AttendanceAndDeparture/StaffTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorksFullTimeFromDate,EmployeeId,PermanenceModelsId,SectionsId,PeriodId")] StaffTime staffTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffTime.EmployeeId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", staffTime.PermanenceModelsId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", staffTime.SectionsId);
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName", staffTime.PeriodId);
            return View(staffTime);
        }

        // GET: AttendanceAndDeparture/StaffTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffTime = await _context.staffTimes.FindAsync(id);
            if (staffTime == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffTime.EmployeeId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", staffTime.PermanenceModelsId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", staffTime.SectionsId);
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName", staffTime.PeriodId);

            return View(staffTime);
        }

        // POST: AttendanceAndDeparture/StaffTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorksFullTimeFromDate,EmployeeId,PermanenceModelsId,SectionsId,PeriodId")] StaffTime staffTime)
        {
            if (id != staffTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffTimeExists(staffTime.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffTime.EmployeeId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", staffTime.PermanenceModelsId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", staffTime.SectionsId);
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName", staffTime.PeriodId);

            return View(staffTime);
        }

        // GET: AttendanceAndDeparture/StaffTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffTime = await _context.staffTimes
                .Include(s => s.Employee)
                .Include(s => s.PermanenceModels)
                .Include(s => s.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffTime == null)
            {
                return NotFound();
            }

            return View(staffTime);
        }

        // POST: AttendanceAndDeparture/StaffTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffTime = await _context.staffTimes.FindAsync(id);
            if (staffTime != null)
            {
                _context.staffTimes.Remove(staffTime);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffTimeExists(int id)
        {
            return _context.staffTimes.Any(e => e.Id == id);
        }
    }
}
