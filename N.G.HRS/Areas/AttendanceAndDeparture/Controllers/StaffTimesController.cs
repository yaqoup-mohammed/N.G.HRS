using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
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
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.staffTimes.Include(s => s.Employee).Include(s => s.Periods).Include(s => s.PermanenceModels).Include(s => s.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/StaffTimes/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffTime = await _context.staffTimes
                .Include(s => s.Employee)
                .Include(s => s.Periods)
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
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();

            return View();
        }

        // POST: AttendanceAndDeparture/StaffTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,WorksFullTimeFromDate,EmployeeId,PermanenceModelsId,SectionsId,PeriodId")] StaffTime staffTime)
        {
            if (ModelState.IsValid)
            {
                if (staffTime.EmployeeId == 0)
                {
                    var employeeLisr = _context.employee.ToList();
                    foreach (var item in employeeLisr)
                    {
                        var staffime = _context.staffTimes.FirstOrDefault(x => x.EmployeeId == item.Id);
                        if (staffime == null)
                        {
                            StaffTime staff = new StaffTime
                            {
                                EmployeeId = item.Id,
                                WorksFullTimeFromDate = staffTime.WorksFullTimeFromDate,
                                SectionsId = item.SectionsId,
                                PermanenceModelsId = staffTime.PermanenceModelsId,
                                PeriodId = staffTime.PeriodId,

                            };

                            
                                var period = _context.Periods.FirstOrDefault(x => x.Id == staffTime.PeriodId);
                                if (period != null)
                                {
                                    var balance = _context.VacationBalance.FirstOrDefault(x => x.EmployeeId == item.Id);
                                    if (balance != null)
                                    {
                                        balance.ShiftHour += period.Hours.Value;
                                        _context.VacationBalance.Update(balance);
                                    }
                                    else
                                    {
                                        VacationBalance vacation = new VacationBalance
                                        {
                                            EmployeeId = item.Id,
                                            ShiftHour = period.Hours.Value
                                        };
                                        _context.VacationBalance.Add(vacation);
                                    }
                                }
                            
                            _context.staffTimes.Add(staff);
                            await _context.SaveChangesAsync();

                        }
                        else
                        {
                            continue;
                        }
                    }
                    return RedirectToAction(nameof(Index));

                }
                else
                {


                    var employee = _context.employee.FirstOrDefault(x => x.Id == staffTime.EmployeeId);
                    if (employee != null)
                    {
                        var period = _context.Periods.FirstOrDefault(x => x.Id == staffTime.PeriodId);
                        if (period != null)
                        {
                            var balance = _context.VacationBalance.FirstOrDefault(x => x.EmployeeId == staffTime.EmployeeId);
                            if (balance != null)
                            {
                                balance.ShiftHour += period.Hours.Value;
                                _context.VacationBalance.Update(balance);
                            }
                            else
                            {
                                VacationBalance vacation = new VacationBalance
                                {
                                    EmployeeId = staffTime.EmployeeId.Value,
                                    ShiftHour = period.Hours.Value
                                };
                                _context.VacationBalance.Add(vacation);
                            }
                        }
                    }
                }


                _context.Add(staffTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", staffTime.EmployeeId);

            return View(staffTime);
        }

        // GET: AttendanceAndDeparture/StaffTimes/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var staffTime = await _context.staffTimes.FindAsync(id);
            if (staffTime == null)
            {
                return NotFound();
            }


            return View(staffTime);
        }

        // POST: AttendanceAndDeparture/StaffTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorksFullTimeFromDate,EmployeeId,PermanenceModelsId,SectionsId,PeriodId")] StaffTime staffTime)
        {
            if (id != staffTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                try
                {
                    var period = _context.Periods.FirstOrDefault(x => x.Id == staffTime.PeriodId);
                    if (period != null)
                    {
                        var balance = _context.VacationBalance.FirstOrDefault(x => x.EmployeeId == staffTime.EmployeeId);
                        if (balance != null)
                        {

                            balance.ShiftHour -= period.Hours.Value;
                            _context.VacationBalance.Update(balance);
                            await _context.SaveChangesAsync();

                            balance.ShiftHour += period.Hours.Value;
                            _context.VacationBalance.Update(balance);
                            _context.Update(staffTime);
                            await _context.SaveChangesAsync();

                        }

                    }

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


            return View(staffTime);
        }


        // GET: AttendanceAndDeparture/StaffTimes/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffTime = await _context.staffTimes
                .Include(s => s.Employee)
                .Include(s => s.Periods)
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffTime = await _context.staffTimes.FindAsync(id);
            if (staffTime != null)
            {
                var result = _context.VacationBalance.FirstOrDefault(x => x.EmployeeId == staffTime.EmployeeId);
                var period = _context.Periods.FirstOrDefault(x => x.Id == staffTime.PeriodId);
                if (result != null && period != null)
                {
                    result.ShiftHour -= period.Hours.Value;
                    _context.VacationBalance.Update(result);
                }

                _context.staffTimes.Remove(staffTime);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffTimeExists(int id)
        {
            return _context.staffTimes.Any(e => e.Id == id);
        }

        private async Task PopulateDropdownListsAsync()
        {
            //var department = await _context.Departments.ToListAsync();
            //ViewData["DepartmentsId"] = new SelectList(department, "Id", "SubAdministration");
            //====================================================
            var section = await _context.Sections.ToListAsync();
            ViewData["SectionsId"] = new SelectList(section, "Id", "SectionsName");
            //=========================================================
            var employee = await _context.employee.ToListAsync();
            ViewData["EmployeeId"] = new SelectList(employee, "Id", "EmployeeName");
            //============================================================
            var permanance = await _context.permanenceModels.ToListAsync();
            ViewData["PermanenceModelsId"] = new SelectList(permanance, "Id", "PermanenceName");
            //============================================================
            var period = await _context.periods.ToListAsync();
            ViewData["PeriodsId"] = new SelectList(period, "Id", "PeriodsName");


        }
        public IActionResult GetEmployee(int? id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            else
            {
                var employee = _context.employee.FirstOrDefault(x => x.Id == id);
                return Json(employee);
            }
        }
        public IActionResult GetPermanenceModels(int? id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            else
            {
                var permanance = _context.periods.Where(x => x.PermanenceModelsId == id).ToList().Select(x => new { id = x.Id, name = x.PeriodsName, from = x.FromTime, to = x.ToTime, hours = x.Hours });
                return Json(permanance);
            }
        }
        public IActionResult CheackGetPermanenceModels(int? id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            else
            {
                var permanance = _context.staffTimes.Include(x => x.PermanenceModels).Where(x => x.EmployeeId == id).Select(x => new { id = x.PermanenceModelsId, perId = x.PeriodId, name = x.PermanenceModels.PermanenceName });
                if (permanance != null)
                {
                    return Json(permanance);
                }
            }
            return NotFound();
        }
        public IActionResult CheackPeriod(int? id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            else
            {
                var permanance = _context.staffTimes.Where(x => x.EmployeeId == id).Select(x => new { id = x.PeriodId });
                if (permanance != null)
                {
                    return Json(permanance);
                }
            }
            return NotFound();
        }
    }
}
