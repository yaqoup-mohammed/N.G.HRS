using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Date;
using N.G.HRS.FingerPrintSetting;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]
    public class AttendanceRecordsController : Controller
    {
        private readonly AppDbContext _context;
        public AttendanceRecordsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: MaintenanceControl/AttendanceRecords
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AttendanceRecord.Include(a => a.Employee).Include(a => a.Period).Include(a => a.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MaintenanceControl/AttendanceRecords/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecord
                .Include(a => a.Employee)
                .Include(a => a.Period)
                .Include(a => a.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // GET: MaintenanceControl/AttendanceRecords/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "PeriodsName");
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            return View();
        }

        // POST: MaintenanceControl/AttendanceRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,SectionId,EmployeeId,PeriodsId,TimeOnlyRecord,Note")] AttendanceRecord attendanceRecord)
        {
            if (ModelState.IsValid)
            {
               var employee = _context.employee.FirstOrDefault(x => x.Id == attendanceRecord.EmployeeId);
                MachineInfo attLog = new MachineInfo();
                attLog.IndRegID = employee.EmployeeNumber;
                attLog.EmployeeName = employee.EmployeeName;
                attLog.DepartmentId = employee.DepartmentsId;
                attLog.SectionId = employee.SectionsId;
                attLog.DateTimeRecord = attendanceRecord.TimeOnlyRecord.ToString();
                attLog.TimeOnlyRecord = new DateTime(2000, 01, 01, attendanceRecord.TimeOnlyRecord.Hour, attendanceRecord.TimeOnlyRecord.Minute, attendanceRecord.TimeOnlyRecord.Second);
                attLog.DateOnlyRecord = new DateOnly(attendanceRecord.Date.Year, attendanceRecord.Date.Month, attendanceRecord.Date.Day);
                attLog.State = "حافظة دوام";
                attLog.MachineNumber = 999999990;
                _context.MachineInfo.Add(attLog);

                _context.Add(attendanceRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", attendanceRecord.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", attendanceRecord.PeriodsId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", attendanceRecord.SectionId);
            return View(attendanceRecord);
        }

        // GET: MaintenanceControl/AttendanceRecords/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecord.FindAsync(id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", attendanceRecord.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", attendanceRecord.PeriodsId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", attendanceRecord.SectionId);
            return View(attendanceRecord);
        }

        // POST: MaintenanceControl/AttendanceRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionId,EmployeeId,PeriodsId,TimeOnlyRecord,Note")] AttendanceRecord attendanceRecord)
        {
            if (id != attendanceRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceRecordExists(attendanceRecord.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", attendanceRecord.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", attendanceRecord.PeriodsId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", attendanceRecord.SectionId);
            return View(attendanceRecord);
        }

        // GET: MaintenanceControl/AttendanceRecords/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecord
                .Include(a => a.Employee)
                .Include(a => a.Period)
                .Include(a => a.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // POST: MaintenanceControl/AttendanceRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendanceRecord = await _context.AttendanceRecord.FindAsync(id);
            if (attendanceRecord != null)
            {
                _context.AttendanceRecord.Remove(attendanceRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceRecordExists(int id)
        {
            return _context.AttendanceRecord.Any(e => e.Id == id);
        }
        public IActionResult EmployeeInfo(int id)
        {
            if(id== 0)
            {
                return NotFound();
            }
            else
            {
                var employee=_context.employee.Where(e => e.Id == id).Select(x => new {id=x.SectionsId});
                if (employee != null)
                {
                    return Json(new { employee });
                }

                return NotFound();

            }

        }
        public IActionResult EmployeePeriod(int id)
        {
            if(id== 0)
            {
                return NotFound();
            }
            else
            {
                var period=_context.staffTimes.Include(x=>x.Periods).Where(e => e.EmployeeId == id).Select(x => new {id=x.PeriodId,name=x.Periods.PeriodsName,from=x.Periods.FromTime,to=x.Periods.ToTime});
                if (period != null)
                {
                    return Json(new { period });
                }

                return NotFound();

            }

        }
        public IActionResult PeriodInfo(int id)
        {
            if(id== 0)
            {
                return NotFound();
            }
            else
            {
                var periodInfo=_context.Periods.FirstOrDefault(e => e.Id == id);
                if (periodInfo != null)
                {
                    return Json(new { periodInfo });
                }

                return NotFound();

            }

        }
    }
}
