using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StaffVacations.Include(s => s.Employee).Include(s => s.Period).Include(s => s.Sections).Include(s => s.SubstituteStaffMember).Include(s => s.Vacation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MaintenanceControl/StaffVacations/Details/5
        [Authorize(Policy = "DetailsPolicy")]

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
        [Authorize(Policy = "AddPolicy")]

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
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Date,VacationId,SectionId,EmployeeId,PeriodsId,IsConnected,FromDate,ToDate,PerDay,PerHour,PerMinute,Accepted,SubstituteStaffMemberId,DonorSide,Reason,Note")] StaffVacations staffVacations)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var balance = _context.VacationBalance.FirstOrDefault(x => x.EmployeeId == staffVacations.EmployeeId);
                    if (balance != null)
                    {
                        var rest = (balance.Editorial + balance.Transferred) - balance.Expendables;
                        //if (rest < staffVacations.PerMinute || rest == 0)
                        //{


                        //    TempData["Error"] = "الرصيد لا يكفي ";
                        //    return View(staffVacations);
                        //}
                        //else { 
                            if (staffVacations.VacationId == 1)
                            {
                                if (balance.Annual < staffVacations.PerMinute)
                                {
                                    balance.Annual -= balance.Annual;
                                    balance.Expendables += staffVacations.PerMinute;
                                    _context.VacationBalance.Update(balance);

                                }
                                else
                                {
                                    balance.Annual -= staffVacations.PerMinute;
                                    balance.Expendables += staffVacations.PerMinute;
                                    _context.VacationBalance.Update(balance);
                                }

                            }
                            else
                            {

                                balance.Expendables += staffVacations.PerMinute;
                                _context.VacationBalance.Update(balance);
                            }
                        }

                    //}
                    _context.Add(staffVacations);

                    await _context.SaveChangesAsync();
                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] =ex.Message;
                }
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", staffVacations.PeriodsId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", staffVacations.SectionId);
            ViewData["SubstituteStaffMemberId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffVacations.SubstituteStaffMemberId);
            ViewData["VacationId"] = new SelectList(_context.publicHolidays, "Id", "HolidayName", staffVacations.VacationId);
            return View(staffVacations);
        }

        // GET: MaintenanceControl/StaffVacations/Edit/5
        [ Authorize(Policy = "EditPolicy")]
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
        [Authorize(Policy = "EditPolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
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
        public IActionResult GetSection(int? id)
        {
            if (id == 0)
            {
                return Json(new { error = "لم يتم العثور على أي قسم" });
            }
            else
            {
                var sect =  _context.employee.Where(x => x.Id == id).Select(x=> new {id= x.SectionsId}).FirstOrDefault();

                return Json(new { sect });
            
            }
        }
        //=======================================================
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

                        var employeePeriod = _context.periods.Where(x => employeePeriodIds.Contains(x.Id)).Select(x => new { id = x.Id, name = x.PeriodsName }).ToList();

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
                    var vacationsbalance = _context.openingBalancesForVacations.Include(x=>x.PublicHolidays).Where(x => x.EmployeeId == id && x.BalanceYear==DateTime.Now.Year).Select(x => new {id=x.PublicHolidaysId,name=x.PublicHolidays.HolidayName,balance=x.Balance,year=x.BalanceYear}).ToList();

                    if (vacationsbalance != null)
                    {
                       
                        return Json(new { vacationsbalance });
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
        public IActionResult VocationList(int? empid, int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Where(x => x.Id == id).FirstOrDefault();
                if (employee != null)
                {
                    var vacationsbalance = _context.openingBalancesForVacations.Include(x=>x.PublicHolidays).Where(x => x.EmployeeId == empid && x.PublicHolidaysId==id).Select(x => new {id=x.PublicHolidaysId,name=x.PublicHolidays.HolidayName,balance=x.Balance,year=x.BalanceYear}).ToList();
                    if (vacationsbalance != null)
                    {
                       
                        return Json(new { vacationsbalance });
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
                        var vacationBalanceList= vacationBalance.Select(x => new {
                            annul = x.Annual,
                            editorial = x.Editorial,
                            transferred = x.Transferred,
                            residual = x.Residual,
                            expendables = x.Expendables,
                            shiftHour = x.ShiftHour,
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
        public IActionResult CheackVocationBalance(int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.FirstOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    var vacationBalance = _context.VacationBalance.Where(x => x.EmployeeId == id).Select(x => new{
                        annul = x.Annual,
                        editorial = x.Editorial,
                        transferred = x.Transferred,
                        residual = x.Residual,
                        expendables = x.Expendables,
                        shiftHour = x.ShiftHour,
                    }).FirstOrDefault();
                    if (vacationBalance == null)
                    {
                        TempData["Error"] = "الموظف المحدد لا يمتلك اي أجازات ☹";
                        return Ok();


                    }
                    else
                    {
                        //var vacationBalanceList= vacationBalance.Select(x => new {
                        //    annul = x.Annual,
                        //    editorial = x.Editorial,
                        //    transferred = x.Transferred,
                        //    residual = x.Residual,
                        //    expendables = x.Expendables,
                        //    shiftHour = x.ShiftHour,
                        // });
                        return Json(new { vacationBalance } );
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
        public IActionResult ShiftHour(int? id)
        {
            if (id != 0)
            {
                var employee = _context.employee.FirstOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    var vacationBalance = _context.VacationBalance.Where(x => x.EmployeeId == id).Select(x=>new { shiftHour = x.ShiftHour}).FirstOrDefault();
                    if (vacationBalance == null)
                    {
                        TempData["Error"] = "الموظف المحدد لا يمتلك اي أجازات ☹";
                        return Ok();


                    }
                    else
                    {
                        //return Json(vacationBalance);
                        return Json(new { vacationBalance });
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
        public IActionResult OpeningBalancesForVacations(int? id)
        {
            if (id != 0)
            {
                
                    var vacationBalance = _context.openingBalancesForVacations.Include(x => x.PublicHolidays).Where(x => x.PublicHolidaysId == id).Select(x => new { id = x.PublicHolidaysId, name = x.PublicHolidays.HolidayName, balance = x.Balance}).FirstOrDefault();
                    if (vacationBalance == null)
                    {
                        TempData["Error"] = "الموظف المحدد لا يمتلك اي أجازات ☹";
                        return Ok();
                    }
                    else
                    {
                        //return Json(vacationBalance);
                        return Json(new { vacationBalance });
                    }

                
            }

            else
            {
                return BadRequest("Invalid id");
            }
        }
    }
}
