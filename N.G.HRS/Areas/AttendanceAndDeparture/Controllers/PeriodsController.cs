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
    public class PeriodsController : Controller
    {
        private readonly AppDbContext _context;

        public PeriodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceAndDeparture/Periods
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.periods.Include(p => p.PermanenceModels);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periods = await _context.periods
                .Include(p => p.PermanenceModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periods == null)
            {
                return NotFound();
            }

            return View(periods);
        }

        // GET: AttendanceAndDeparture/Periods/Create
        public IActionResult Create()
        {
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName");
            return View();
        }

        // POST: AttendanceAndDeparture/Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Periods periods)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (periods.FromTime > periods.ToTime)
                    {
                        ViewData["Error"] = "وقت البدء يجب ان يكون اقل من وقت الانتهاء!!";
                        return View(periods);
                    }
                        periods.Hours = CalculateHourOfWork(periods.FromTime, periods.ToTime);
                        _context.Add(periods);
                        await _context.SaveChangesAsync();
                        ViewData["Message"] = "تمت العملية بنجاح";
                        return RedirectToAction(nameof(Index));
                    

                    
                }
                catch (Exception ex)
                {
                    ViewData["Error"] = ex.Message+"قيمة احد الحقول قد تكون خاطئة!!";

                    return View(periods);
                }

            }
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", periods.PermanenceModelsId);
            return View(periods);
        }

        // GET: AttendanceAndDeparture/Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periods = await _context.periods.FindAsync(id);
            if (periods == null)
            {
                return NotFound();
            }
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", periods.PermanenceModelsId);
            return View(periods);
        }

        // POST: AttendanceAndDeparture/Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PeriodsName,FromTime,ToTime,Saturday,SunDay,Monday,Tuesday,Wednesday,Thursday,Friday,Hours,PermanenceModelsId")] Periods periods)
        {
            if (id != periods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (periods.FromTime > periods.ToTime)
                    {
                        ViewData["Error"] = "وقت البدء يجب ان يكون اقل من وقت الانتهاء!!";
                        return View(periods);


                    }
                    periods.Hours = CalculateHourOfWork(periods.FromTime, periods.ToTime);
                    _context.Update(periods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodsExists(periods.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewData["Error"] = ("قيمة احد الحقول قد تكون خاطئة!!");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", periods.PermanenceModelsId);
            return View(periods);
        }

        // GET: AttendanceAndDeparture/Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periods = await _context.periods
                .Include(p => p.PermanenceModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periods == null)
            {
                return NotFound();
            }

            return View(periods);
        }

        // POST: AttendanceAndDeparture/Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var periods = await _context.periods.FindAsync(id);
            if (periods != null)
            {
                _context.periods.Remove(periods);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodsExists(int id)
        {
            return _context.periods.Any(e => e.Id == id);
        }
        private int CalculateHourOfWork( DateTime FromTime, DateTime ToTime)
        {


            //if (AddAttendanceAndDeparturePermission)
            //{
            //    TimeSpan withPermission = secondTime.AddMinutes(-EarlyDeparturePermission ?? 0) - firstTime.AddMinutes(AllowanceForLateAttendance ?? 0);

            //    return withPermission.TotalHours;
            //}
            TimeSpan timeSpan = ToTime - FromTime;

            int totalHours = timeSpan.Hours;

            return totalHours;
        }
        private bool IsBetween(DateTime fromTime,DateTime toTime,DateTime isBetweenFrom,DateTime isBetweenTo)
        {
                if (fromTime > toTime) {
                    if(fromTime >= isBetweenFrom && fromTime <= isBetweenTo)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                        throw new Exception("وقت  بداية ونهاية الفترة يجب ان تكون بين تاريخ البداية ونهاية الدوام");
                    }
                }
                else
                {
                    return false;
                    throw new Exception("وقت  البداية المحدد يجب ان يكون اقل من وقت الانتهاء");
                }

        }

    }
}
