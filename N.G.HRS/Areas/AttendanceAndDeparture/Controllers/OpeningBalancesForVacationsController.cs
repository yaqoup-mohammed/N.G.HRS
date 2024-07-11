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
using N.G.HRS.Repository;
using OfficeDevPnP.Core.Extensions;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]
    public class OpeningBalancesForVacationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<OpeningBalancesForVacations> _openingBalancesForVacationsRepository;

        public OpeningBalancesForVacationsController(AppDbContext context, IRepository<OpeningBalancesForVacations> openingBalancesForVacationsRepository)
        {
            _context = context;
            _openingBalancesForVacationsRepository = openingBalancesForVacationsRepository;
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.openingBalancesForVacations.Include(o => o.Employee).Include(o => o.PublicHolidays).OrderBy(x => x.EmployeeId);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openingBalancesForVacations = await _context.openingBalancesForVacations
                .Include(o => o.Employee)
                .Include(o => o.PublicHolidays)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (openingBalancesForVacations == null)
            {
                return NotFound();
            }

            return View(openingBalancesForVacations);
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
 
            return View();
        }

        // POST: AttendanceAndDeparture/OpeningBalancesForVacations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,BalanceYear,Balance,Date,Notes,EmployeeId,PublicHolidaysId")] OpeningBalancesForVacations openingBalancesForVacations)
        {
            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                if (openingBalancesForVacations.EmployeeId == 0)
                {
                    try
                    {



                        var employees = await _context.employee.ToListAsync();
                        foreach (var emp in employees)
                        {
                            var openBalance= await _context.openingBalancesForVacations.Where(x=>x.PublicHolidaysId==openingBalancesForVacations.PublicHolidaysId && x.EmployeeId==emp.Id && x.BalanceYear==openingBalancesForVacations.BalanceYear).FirstOrDefaultAsync();
                            if (openBalance == null )
                            {
                                    var voc = new OpeningBalancesForVacations
                                    {
                                        BalanceYear = openingBalancesForVacations.BalanceYear,
                                        Balance = openingBalancesForVacations.Balance,
                                        Date = openingBalancesForVacations.Date,
                                        Notes = openingBalancesForVacations.Notes,
                                        PublicHolidaysId = openingBalancesForVacations.PublicHolidaysId,
                                        EmployeeId = emp.Id

                                    };
                                var vacatiBalance=_context.VacationBalance.Where(x=>x.EmployeeId==emp.Id ).FirstOrDefault();
                                if (vacatiBalance != null)
                                {
                                    vacatiBalance.Editorial += (openingBalancesForVacations.Balance* vacatiBalance.ShiftHour) *60;
                                    if (openingBalancesForVacations.PublicHolidaysId == 1 && openingBalancesForVacations.BalanceYear == DateTime.Now.Year)
                                    {
                                        vacatiBalance.Annual = (openingBalancesForVacations.Balance * vacatiBalance.ShiftHour) * 60;

                                    }
                                     _context.VacationBalance.Update(vacatiBalance);

                                }
                                else
                                {
                                    TempData["Error"] = "هذا الموظف لا ينتمي الى دوام يرجى تسجيل الدوام للموظف";
                                    
                                }
                                await _openingBalancesForVacationsRepository.AddAsync(voc);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        await _context.SaveChangesAsync();

                        //await _context.SaveChangesAsync();
                        TempData["Success"] = "تمت الاضافة لكافة الموظفين";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        TempData["SystemError"]= ex.Message;
                        return View();
                    }
                }
                //=====================================
                var vacationBalance = _context.VacationBalance.Where(x => x.EmployeeId == openingBalancesForVacations.EmployeeId).FirstOrDefault();
                if (vacationBalance != null)
                {
                    vacationBalance.Editorial += (openingBalancesForVacations.Balance * vacationBalance.ShiftHour) * 60;
                    if (openingBalancesForVacations.PublicHolidaysId == 1 && openingBalancesForVacations.BalanceYear == DateTime.Now.Year)
                    {
                        vacationBalance.Annual = (openingBalancesForVacations.Balance * vacationBalance.ShiftHour) * 60;

                    }
                    _context.VacationBalance.Update(vacationBalance);

                }
                else
                {
                    //if (openingBalancesForVacations.PublicHolidaysId == 1 && openingBalancesForVacations.BalanceYear == DateTime.Now.Year)
                    //{
                    //    var vacaBalance = new VacationBalance
                    //    {
                    //        Editorial = (openingBalancesForVacations.Balance * vacationBalance.ShiftHour) * 60,
                    //        EmployeeId = openingBalancesForVacations.EmployeeId.Value,
                    //        Annual = (openingBalancesForVacations.Balance * vacationBalance.ShiftHour) * 60

                    //    };
                    //    _context.VacationBalance.Add(vacaBalance);

                    //}
                    //else
                    //{

                        TempData["Error"] = "هذا الموظف لا ينتمي الى دوام يرجى تسجيل الدوام للموظف";
                        //var vacaBalance = new VacationBalance
                        //{
                        //    Editorial = (openingBalancesForVacations.Balance * vacationBalance.ShiftHour) * 60,
                        //    EmployeeId = openingBalancesForVacations.EmployeeId.Value,
                        //    Annual = (openingBalancesForVacations.Balance * vacationBalance.ShiftHour) * 60

                        //};
                        //_context.VacationBalance.Add(vacaBalance);

                    

                }

                    //=====================================
                    await _openingBalancesForVacationsRepository.AddAsync(openingBalancesForVacations);
                return RedirectToAction(nameof(Index));
            }
            return View(openingBalancesForVacations);
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var openingBalancesForVacations = await _openingBalancesForVacationsRepository.GetByIdAsync(id);
            if (openingBalancesForVacations == null)
            {
                return NotFound();
            }
            return View(openingBalancesForVacations);
        }

        // POST: AttendanceAndDeparture/OpeningBalancesForVacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BalanceYear,Balance,Date,Notes,EmployeeId,PublicHolidaysId")] OpeningBalancesForVacations openingBalancesForVacations)
        {
            if (id != openingBalancesForVacations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();

                    await _openingBalancesForVacationsRepository.UpdateAsync(openingBalancesForVacations);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpeningBalancesForVacationsExists(openingBalancesForVacations.Id))
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
            return View(openingBalancesForVacations);
        }

        // GET: AttendanceAndDeparture/OpeningBalancesForVacations/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openingBalancesForVacations = await _context.openingBalancesForVacations
                .Include(o => o.Employee)
                .Include(o => o.PublicHolidays)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (openingBalancesForVacations == null)
            {
                return NotFound();
            }

            return View(openingBalancesForVacations);
        }

        // POST: AttendanceAndDeparture/OpeningBalancesForVacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var openingBalancesForVacations = await _openingBalancesForVacationsRepository.GetByIdAsync(id);
            if (openingBalancesForVacations != null)
            {
                var vacationBalance = _context.VacationBalance.FirstOrDefault(x => x.EmployeeId == openingBalancesForVacations.EmployeeId );

                if (vacationBalance != null)
                {
                    if (openingBalancesForVacations.PublicHolidaysId == 1)
                    {
                        vacationBalance.Annual -= openingBalancesForVacations.Balance;
                        _context.VacationBalance.Update(vacationBalance);

                    }
                    else
                    {
                        vacationBalance.Editorial -= openingBalancesForVacations.Balance;
                        _context.VacationBalance.Update(vacationBalance);

                    }
                }
                await _context.SaveChangesAsync();  
                await _openingBalancesForVacationsRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OpeningBalancesForVacationsExists(int id)
        {
            return _context.openingBalancesForVacations.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {


            //=========================================================
            var employee = await _context.employee.ToListAsync();
            ViewData["EmployeeId"] = new SelectList(employee, "Id", "EmployeeName");
            //============================================================
            var permanance = await _context.publicHolidays.ToListAsync();
            ViewData["publicHolidays"] = new SelectList(permanance, "Id", "HolidayName");
        }

       public IActionResult Vocation(int id)
        {
            var voc=_context.publicHolidays.FirstOrDefault(e => e.Id == id);
            if (voc != null)
            {
                return Json(voc);
            }
            TempData["Error"] = "لم يتم العثور على بيانات!!";
            return View();
        }

    }
}
