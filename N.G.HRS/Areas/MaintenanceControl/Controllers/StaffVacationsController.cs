using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Employees.ViewModel;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Date;
using OfficeDevPnP.Core.Extensions;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]
    public class StaffVacationsController : Controller
    {
        private readonly AppDbContext _context;

        public StaffVacationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceControl/StaffVacations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StaffVacations.Include(s => s.Employee).Include(s => s.Period).Include(s => s.Sections).Include(s => s.SubstituteStaffMember).Include(s => s.Vacation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MaintenanceControl/StaffVacations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffVacations = await _context.StaffVacations
                .Include(s => s.Employee)
                .Include(s => s.Period)
                .Include(s => s.Sections)
                .Include(s => s.SubstituteStaffMember)
                .Include(s => s.Vacation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffVacations == null)
            {
                return NotFound();
            }

            return View(staffVacations);
        }

        // GET: MaintenanceControl/StaffVacations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "PeriodsName");
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            ViewData["SubstituteStaffMemberId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["VacationId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName");
            return View();
        }

        // POST: MaintenanceControl/StaffVacations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,VacationId,SectionId,EmployeeId,PeriodsId,IsConnected,FromDate,ToDate,PerDay,PerHour,PerMinute,Accepted,SubstituteStaffMemberId,DonorSide,Reason,Note")] StaffVacations staffVacations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffVacations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", staffVacations.PeriodsId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", staffVacations.SectionId);
            ViewData["SubstituteStaffMemberId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.SubstituteStaffMemberId);
            ViewData["VacationId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName", staffVacations.VacationId);
            return View(staffVacations);
        }

        // GET: MaintenanceControl/StaffVacations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffVacations = await _context.StaffVacations.FindAsync(id);
            if (staffVacations == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", staffVacations.PeriodsId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", staffVacations.SectionId);
            ViewData["SubstituteStaffMemberId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.SubstituteStaffMemberId);
            ViewData["VacationId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName", staffVacations.VacationId);
            return View(staffVacations);
        }

        // POST: MaintenanceControl/StaffVacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,VacationId,SectionId,EmployeeId,PeriodsId,IsConnected,FromDate,ToDate,PerDay,PerHour,PerMinute,Accepted,SubstituteStaffMemberId,DonorSide,Reason,Note")] StaffVacations staffVacations)
        {
            if (id != staffVacations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffVacations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffVacationsExists(staffVacations.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", staffVacations.PeriodsId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", staffVacations.SectionId);
            ViewData["SubstituteStaffMemberId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.SubstituteStaffMemberId);
            ViewData["VacationId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName", staffVacations.VacationId);
            return View(staffVacations);
        }

        // GET: MaintenanceControl/StaffVacations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffVacations = await _context.StaffVacations
                .Include(s => s.Employee)
                .Include(s => s.Period)
                .Include(s => s.Sections)
                .Include(s => s.SubstituteStaffMember)
                .Include(s => s.Vacation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffVacations == null)
            {
                return NotFound();
            }

            return View(staffVacations);
        }

        // POST: MaintenanceControl/StaffVacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffVacations = await _context.StaffVacations.FindAsync(id);
            if (staffVacations != null)
            {
                _context.StaffVacations.Remove(staffVacations);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffVacationsExists(int id)
        {
            return _context.StaffVacations.Any(e => e.Id == id);
        }

        //================================================
        public async Task<IActionResult> EmployeeOnSection(int? id)
        {
            if (id == 0)
            {
                var employee = _context.employee.ToList();
                if (employee != null)
                {
                    var employeeList = employee.Select(x => new { id = x.Id, name = x.EmployeeName });
                    return Json(new { employeeList });
                }
                else
                {
                    return Json(new { error = "لم يتم العثور على أي موظفين" });
                }
            }
            else
            {
                var sect = await _context.Sections.FirstOrDefaultAsync(x => x.Id == id);
                if (sect != null)
                {
                    var employee = _context.employee.Where(x => x.SectionsId == id && x.EmploymentStatus != "6").ToList();

                    if (employee != null)
                    {
                        var employeeList = employee.Select(x => new { id = x.Id, name = x.EmployeeName });
                        return Json(new { employeeList });
                    }
                    else
                    {
                        return Json(new { error = "لم يتم العثور على أي موظفين" });
                    }
                }
            }
            return NotFound();
        }


        public IActionResult GetEmployee(int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Where(x => x.SectionsId == id).ToList();
                if (employee != null)
                {
                    var employeeList = employee.Select(x => new { id = x.Id, name = x.EmployeeName });

                    return Json(employeeList);
                }
            }
            else
            {
                var employee = _context.employee.ToList();
                if (employee != null)
                {
                    var employeeList = employee.Select(x => new { id = x.Id, name = x.EmployeeName });
                    return Json(employeeList);
                }
            }
            return NotFound();

        }

        public IActionResult EmployeePeriod(int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Where(x => x.Id == id).FirstOrDefault();
                if (employee != null)
                {
                    var stafftimes = _context.staffTimes.Where(x => x.EmployeeId == employee.Id).ToList();

                    if (stafftimes != null)
                    {
                        int[] employeePeriodIds = new int[stafftimes.Count];
                        int index = 0;
                        foreach (var item in stafftimes)
                        {
                            if (item.PeriodId.HasValue)
                            {
                                employeePeriodIds[index] = item.PeriodId.Value;
                                index++;
                            }
                        }

                        var employeePeriod = _context.periods.Where(x => employeePeriodIds.Contains(x.Id)).ToList();

                        return Json(employeePeriod);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();

                }
            }

            else
            {
                return BadRequest("Invalid id");
            }
        }
        public IActionResult EmployeeVocationList(int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Where(x => x.Id == id).FirstOrDefault();
                if (employee != null)
                {
                    var vacationsbalance = _context.openingBalancesForVacations.Where(x => x.EmployeeId == id).ToList();

                    if (vacationsbalance != null)
                    {
                        int[] employeevacationIds = new int[vacationsbalance.Count];
                        int index = 0;
                        foreach (var item in vacationsbalance)
                        {
                            if (item.PublicHolidaysId.HasValue)
                            {
                                employeevacationIds[index] = item.PublicHolidaysId.Value;
                                index++;
                            }
                        }
                        var employeePeriod = _context.publicHolidays.Where(x => employeevacationIds.Contains(x.Id)).ToList();
                        var employeePeriodList = employeePeriod.Select(x => new { id = x.Id, name = x.HolidayName,isBalance = x.Balance , });
                        return Json(employeePeriodList);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();

                }
            }

            else
            {
                return BadRequest("Invalid id");
            }
        }
        public IActionResult SubEmployee(int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Where(x => x.Id != id).ToList();
                if (employee != null)
                {
                    var employeeList = employee.Select(x => new { id = x.Id, name = x.EmployeeName });
                    return Json(employeeList);
                }
                else
                {
                    return NotFound();

                }
            }

            else
            {
                TempData["Error"]="يجب تحديد الموظف 😤";
                return Ok();
            }
        }
        public IActionResult EmployeeShiftTime(int? id)
        {
            if (id != 0)
            {
                var period = _context.periods.Find( id);
                if (period != null)
                {
                    return Json(period);
                }
                else
                {
                    return NotFound();

                }
            }

            else
            {
                TempData["Error"]="يجب تحديد الفترة 😤";
                return Ok();
            }
        }
        public IActionResult EmployeeVocationBalance(int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Where(x => x.Id == id).FirstOrDefault();
                if (employee != null)
                {
                    var vacationBalance = _context.VacationBalance.Where(x => x.EmployeeId == id);
                    if (vacationBalance == null)
                    {
                        TempData["Error"] = "الموظف المحدد لا يمتلك اي أجازات ☹";
                        return Ok();


                    }
                    else
                    {
                        var vacationBalanceList
                         = vacationBalance.Select(x => new {
                            annul = x.Annual,
                            editorial = x.Editorial,
                            transferred = x.Transferred,
                            residual = x.Residual,
                            expendables = x.Expendables
                        });
                        return Json(new { vacationBalanceList });
                        //return Json(new { vacationBalanceList });
                    }

                }
                else
                {
                    return NotFound();

                }
            }

            else
            {
                return BadRequest("Invalid id");
            }
        }
    }
}
