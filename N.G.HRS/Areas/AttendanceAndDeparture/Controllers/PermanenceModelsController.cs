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

        public PermanenceModelsController(AppDbContext context, Periods periods)
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
        public async Task<IActionResult> Create([Bind("Id,PermanenceName,FromDate,ToDate,FlexibleWorkingHours,WorkBetweenTwoShifts,FromTime,ToTime,HoursOfWorks,AddAttendanceAndDeparturePermission,AllowanceForLateAttendance,EarlyDeparturePermission,Notes")] PermanenceModels permanenceModels)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    if (!permanenceModels.WorkBetweenTwoShifts)
                    {
                        if (permanenceModels.FromDate > permanenceModels.ToDate)
                        {
                            throw new Exception("يجب ان يكون تاريخ البدء اقل من تاريخ الانتهاء");
                        }
                        else if (permanenceModels.FromDate < DateOnly.FromDateTime(DateTime.Now))
                        {
                            throw new Exception("يجب ان لا يكون تاريخ البدء اصغر من التاريخ الحالي");
                        }
                        else if (permanenceModels.FromTime > permanenceModels.ToTime)
                        {
                            throw new Exception("يجب ان يكون وقت البدء اقل من وقت الانتهاء");
                        }
                        if (permanenceModels.FlexibleWorkingHours)
                        {
                            permanenceModels.HoursOfWorks = CalculateHourOfWork(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime/*, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance*/);
                            _context.Add(permanenceModels);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }

                        //if (permanenceModels.AddAttendanceAndDeparturePermission)
                        //{
                        //    permanenceModels.FromTime = permanenceModels.FromTime.AddMinutes((double)permanenceModels.AllowanceForLateAttendance);
                        //    permanenceModels.ToTime = permanenceModels.FromTime.AddMinutes(-(double)permanenceModels.EarlyDeparturePermission);
                        //    permanenceModels.HoursOfWorks = CalculateHourOfWorkBetweenDays(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance);
                        //    _context.Add(permanenceModels);
                        //    await _context.SaveChangesAsync();
                        //    return RedirectToAction(nameof(Index));
                        //}
                        else
                        {
                            permanenceModels.HoursOfWorks = CalculateHourOfWork(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime/*, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance*/);
                            _context.Add(permanenceModels);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else if (permanenceModels.WorkBetweenTwoShifts)
                    {
                        permanenceModels.HoursOfWorks = CalculateHourOfWorkBetweenDays(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime/*, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance*/);
                        _context.Add(permanenceModels);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    return View(permanenceModels);
                    throw new Exception("قيمة احد الحقول قد تكون خاطئة!!");

                }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,PermanenceName,FromDate,ToDate,FlexibleWorkingHours,WorkBetweenTwoShifts,FromTime,ToTime,HoursOfWorks,AddAttendanceAndDeparturePermission,AllowanceForLateAttendance,EarlyDeparturePermission,Notes")] PermanenceModels permanenceModels)
        {
            if (id != permanenceModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                 try
                {
                    if (!permanenceModels.WorkBetweenTwoShifts)
                    {
                        if (permanenceModels.FromDate > permanenceModels.ToDate)
                        {
                            throw new Exception("يجب ان يكون تاريخ البدء اقل من تاريخ الانتهاء");
                        }
                        else if (permanenceModels.FromDate < DateOnly.FromDateTime(DateTime.Now))
                        {
                            throw new Exception("يجب ان لا يكون تاريخ البدء اصغر من التاريخ الحالي");
                        }
                        if (permanenceModels.FlexibleWorkingHours)
                        {
                            permanenceModels.HoursOfWorks = CalculateHourOfWork(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime/*, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance*/);
                            _context.Update(permanenceModels);
                            await _context.SaveChangesAsync();
                        }

                        //if (permanenceModels.AddAttendanceAndDeparturePermission)
                        //{
                        //    permanenceModels.FromTime = permanenceModels.FromTime.AddMinutes((double)permanenceModels.AllowanceForLateAttendance);
                        //    permanenceModels.ToTime = permanenceModels.FromTime.AddMinutes(-(double)permanenceModels.EarlyDeparturePermission);
                        //    permanenceModels.HoursOfWorks = CalculateHourOfWorkBetweenDays(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance);
                        //    _context.Update(permanenceModels);
                        //    await _context.SaveChangesAsync();
                        //}
                        else
                        {
                            permanenceModels.HoursOfWorks = CalculateHourOfWork(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime/*, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance*/);
                            _context.Update(permanenceModels);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else if (permanenceModels.WorkBetweenTwoShifts)
                    {
                        permanenceModels.HoursOfWorks = CalculateHourOfWorkBetweenDays(permanenceModels.FromDate, permanenceModels.FromTime, permanenceModels.ToTime/*, permanenceModels.AddAttendanceAndDeparturePermission, permanenceModels.EarlyDeparturePermission, permanenceModels.AllowanceForLateAttendance*/);
                        _context.Update(permanenceModels);
                        await _context.SaveChangesAsync();
                    }
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
        public double CalculateHourOfWorkBetweenDays(DateOnly FromDate, DateTime FromTime, DateTime ToTime/*,bool AddAttendanceAndDeparturePermission,double? EarlyDeparturePermission,double? AllowanceForLateAttendance*/)
        {

            DateOnly nextDay = FromDate.AddDays(1);
            DateTime firstDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, FromTime.Hour, FromTime.Minute, FromTime.Second);
            DateTime secondDate = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, ToTime.Hour, ToTime.Minute, ToTime.Second);

            //if (AddAttendanceAndDeparturePermission)
            //{
            //    TimeSpan withPermission = secondDate.AddMinutes(-EarlyDeparturePermission ?? 0) - firstDate.AddMinutes(AllowanceForLateAttendance ?? 0);

            //    return withPermission.TotalHours;
            //}
            TimeSpan timeSpan = secondDate - firstDate;

            double totalHours = timeSpan.Hours;

            return totalHours;
        }
        public double CalculateHourOfWork(DateOnly FromDate, DateTime FromTime, DateTime ToTime/*, bool AddAttendanceAndDeparturePermission, double? EarlyDeparturePermission, double? AllowanceForLateAttendance*/)
        {
            DateTime firstTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, FromTime.Hour, FromTime.Minute, FromTime.Second);
            DateTime secondTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, ToTime.Hour, ToTime.Minute, ToTime.Second);

            //if (AddAttendanceAndDeparturePermission)
            //{
            //    TimeSpan withPermission = secondTime.AddMinutes(-EarlyDeparturePermission ?? 0) - firstTime.AddMinutes(AllowanceForLateAttendance ?? 0);

            //    return withPermission.TotalHours;
            //}
            TimeSpan timeSpan = secondTime - firstTime;

            int totalHours = timeSpan.Hours;

            return totalHours;
        }

    }
}
