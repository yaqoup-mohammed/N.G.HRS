using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.ProjectServer.Client;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Employees.ViewModel;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Date;
using N.G.HRS.FingerPrintSetting;
using N.G.HRS.HRSelectList;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]
    public class AdditionalExternalOfWorksController : Controller
    {
        private readonly AppDbContext _context;

        public AdditionalExternalOfWorksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceControl/AdditionalExternalOfWorks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdditionalExternalOfWork.Include(a => a.Employee).Include(a => a.SubstituteEmployee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MaintenanceControl/AdditionalExternalOfWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var additionalExternalOfWork = await _context.AdditionalExternalOfWork
                .Include(a => a.Employee)
                .Include(a => a.SubstituteEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalExternalOfWork == null)
            {
                return NotFound();
            }
            return View(additionalExternalOfWork);
        }

        // GET: MaintenanceControl/AdditionalExternalOfWorks/Create
        public IActionResult Create()
        {
           
            ViewData["Assignment"] = new SelectList(_context.Assignment, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["SubstituteEmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: MaintenanceControl/AdditionalExternalOfWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdditionalExternalOfWork additionalExternalOfWork)
        {
            if (ModelState.IsValid)
            {
                var from = new TimeSpan(additionalExternalOfWork.FromTime.Hour, additionalExternalOfWork.FromTime.Minute, 0);
                var to = new TimeSpan(additionalExternalOfWork.ToTime.Hour, additionalExternalOfWork.ToTime.Minute, 0);
                
                _context.Add(additionalExternalOfWork);
              
                if (additionalExternalOfWork.ToDate == null)
                {
                    var isExistWithOutToDate = await _context.AttendanceAndAbsenceProcessing.AnyAsync(x => x.EmployeeId == additionalExternalOfWork.EmployeeId && x.Date == additionalExternalOfWork.FromDate && x.FromTime == from && x.ToTime == to);
                    if (additionalExternalOfWork.AssignmentId == 1)
                    {
                        if (!isExistWithOutToDate)
                        {   
                            var permrnance = _context.staffTimes.Include(x => x.PermanenceModels).Include(x => x.Periods).FirstOrDefault(x => x.EmployeeId == additionalExternalOfWork.EmployeeId);
                            var WorkingTime = CalculateWorkDuration(from, to);
                            AttendanceAndAbsenceProcessing attendanceAndAbsenceProcessing = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = additionalExternalOfWork.EmployeeId,
                                DepartmentId = additionalExternalOfWork.Employee.DepartmentsId,
                                SectionId = additionalExternalOfWork.Employee.SectionsId,
                                AttendanceStatusId = 9,
                                Date = additionalExternalOfWork.Date,
                                FromTime = from,
                                ToTime = to,
                                TotalWorkMinutes = (int)WorkingTime,
                                MinutesOfLate = 0,
                                periodId = permrnance.PeriodId,
                                permenenceId = permrnance.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            await _context.AttendanceAndAbsenceProcessing.AddAsync(attendanceAndAbsenceProcessing);
                        }
                    }
                    else if (additionalExternalOfWork.AssignmentId == 2)
                    {
                        if (!isExistWithOutToDate)
                        {
                            var permrnance = _context.staffTimes.Include(x => x.PermanenceModels).Include(x => x.Periods).FirstOrDefault(x => x.EmployeeId == additionalExternalOfWork.EmployeeId);
                            var WorkingTime = CalculateWorkDuration(from, to);
                            AttendanceAndAbsenceProcessing attendanceAndAbsenceProcessing = new AttendanceAndAbsenceProcessing()
                            {

                                EmployeeId = additionalExternalOfWork.EmployeeId,
                                DepartmentId = additionalExternalOfWork.Employee.DepartmentsId,
                                SectionId = additionalExternalOfWork.Employee.SectionsId,
                                AttendanceStatusId = 15,
                                Date = additionalExternalOfWork.Date,
                                FromTime = from,
                                ToTime = to,
                                TotalWorkMinutes = (int)WorkingTime,
                                MinutesOfLate = 0,
                                periodId = permrnance.PeriodId,
                                permenenceId = permrnance.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false

                            };
                            await _context.AttendanceAndAbsenceProcessing.AddAsync(attendanceAndAbsenceProcessing);
                        }
                    }
                }
                else
                {

                    DateTime currentDate = new DateTime(additionalExternalOfWork.FromDate.Value.Year, additionalExternalOfWork.FromDate.Value.Month, additionalExternalOfWork.FromDate.Value.Day);
                    while (currentDate <= additionalExternalOfWork.ToDate)
                    {
                        var isExistWithToDate = await _context.AttendanceAndAbsenceProcessing.AnyAsync(x => x.EmployeeId == additionalExternalOfWork.EmployeeId && x.Date == currentDate && x.FromTime == from && x.ToTime == to);

                        if (additionalExternalOfWork.AssignmentId == 1)
                        {
                            if (!isExistWithToDate)
                            {
                                var permrnance = _context.staffTimes.Include(x => x.PermanenceModels).Include(x => x.Periods).FirstOrDefault(x => x.EmployeeId == additionalExternalOfWork.EmployeeId);
                                var WorkingTime = CalculateWorkDuration(from, to);
                                AttendanceAndAbsenceProcessing attendanceAndAbsenceProcessing = new AttendanceAndAbsenceProcessing()
                                {

                                    EmployeeId = additionalExternalOfWork.EmployeeId,
                                    DepartmentId = additionalExternalOfWork.Employee.DepartmentsId,
                                    SectionId = additionalExternalOfWork.Employee.SectionsId,
                                    AttendanceStatusId = 9,
                                    Date = additionalExternalOfWork.Date,
                                    FromTime = from,
                                    ToTime = to,
                                    TotalWorkMinutes = (int)WorkingTime,
                                    MinutesOfLate = 0,
                                    periodId = permrnance.PeriodId,
                                    permenenceId = permrnance.PermanenceModelsId,
                                    IsProcssessed = false,
                                    IsProcssessedBefore = false

                                };
                                await _context.AttendanceAndAbsenceProcessing.AddAsync(attendanceAndAbsenceProcessing);

                            }
                        }
                        else if (additionalExternalOfWork.AssignmentId == 2)
                        {
                            if (!isExistWithToDate)
                            {
                                var permrnance = _context.staffTimes.Include(x => x.PermanenceModels).Include(x => x.Periods).FirstOrDefault(x => x.EmployeeId == additionalExternalOfWork.EmployeeId);
                                var WorkingTime = CalculateWorkDuration(from, to);
                                AttendanceAndAbsenceProcessing attendanceAndAbsenceProcessing = new AttendanceAndAbsenceProcessing()
                                {
                                    EmployeeId = additionalExternalOfWork.EmployeeId,
                                    DepartmentId = additionalExternalOfWork.Employee.DepartmentsId,
                                    SectionId = additionalExternalOfWork.Employee.SectionsId,
                                    AttendanceStatusId = 15,
                                    Date = additionalExternalOfWork.Date,
                                    FromTime = from,
                                    ToTime = to,
                                    TotalWorkMinutes = (int)WorkingTime,
                                    MinutesOfLate = 0,
                                    periodId = permrnance.PeriodId,
                                    permenenceId = permrnance.PermanenceModelsId,
                                    IsProcssessed = false,
                                    IsProcssessedBefore = false

                                };
                                await _context.AttendanceAndAbsenceProcessing.AddAsync(attendanceAndAbsenceProcessing);

                            }
                        }

                        currentDate = currentDate.AddDays(1);
                    }

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            

            ViewData["Assignment"] = new SelectList(_context.Assignment, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", additionalExternalOfWork.EmployeeId);
            ViewData["SubstituteEmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", additionalExternalOfWork.SubstituteEmployeeId);
            return View(additionalExternalOfWork);
        }

        // GET: MaintenanceControl/AdditionalExternalOfWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var additionalExternalOfWork = await _context.AdditionalExternalOfWork.FindAsync(id);
            if (additionalExternalOfWork == null)
            {
                return NotFound();
            }
            ViewData["Assignment"] = new SelectList(_context.Assignment, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", additionalExternalOfWork.EmployeeId);
            ViewData["SubstituteEmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", additionalExternalOfWork.SubstituteEmployeeId);
            return View(additionalExternalOfWork);
        }

        // POST: MaintenanceControl/AdditionalExternalOfWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,SubstituteEmployeeId,Date,FromDate,ToDate,FromTime,ToTime,Hours,Minutes,Mission,TaskDestination,Note")] AdditionalExternalOfWork additionalExternalOfWork)
        {
            if (id != additionalExternalOfWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalExternalOfWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalExternalOfWorkExists(additionalExternalOfWork.Id))
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
            ViewData["Assignment"] = new SelectList(_context.Assignment, "Id", "Name");

            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", additionalExternalOfWork.EmployeeId);
            ViewData["SubstituteEmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", additionalExternalOfWork.SubstituteEmployeeId);
            return View(additionalExternalOfWork);
        }

        // GET: MaintenanceControl/AdditionalExternalOfWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalExternalOfWork = await _context.AdditionalExternalOfWork
                .Include(a => a.Employee)
                .Include(a => a.SubstituteEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalExternalOfWork == null)
            {
                return NotFound();
            }

            return View(additionalExternalOfWork);
        }

        // POST: MaintenanceControl/AdditionalExternalOfWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var additionalExternalOfWork = await _context.AdditionalExternalOfWork.FindAsync(id);
            if (additionalExternalOfWork != null)
            {
                _context.AdditionalExternalOfWork.Remove(additionalExternalOfWork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdditionalExternalOfWorkExists(int id)
        {
            return _context.AdditionalExternalOfWork.Any(e => e.Id == id);
        }

        public IActionResult GetEmployees(int id)
        {
            if (id != 0)
            {
                var employees = _context.employee.Where(e => e.Id != id).Select(x => new { id = x.Id, name = x.EmployeeName }).ToList();
                if (employees != null)
                {
                    return Json(new { employees });
                }
            }
            return NotFound();
        }
        private int CalculateWorkDuration(TimeSpan first, TimeSpan last)
        {
            if (first == null || last == null)
            {
                return 0; // Handle incomplete records
            }


            TimeSpan workDuration = last.Subtract(first);
            return (int)workDuration.TotalMinutes;
        }

    }

}
