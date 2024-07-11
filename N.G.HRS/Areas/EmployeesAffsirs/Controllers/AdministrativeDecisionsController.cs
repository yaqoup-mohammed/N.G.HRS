using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.ProjectServer.Client;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Date;
using N.G.HRS.HRSelectList;
using N.G.HRS.Repository;
using Task = System.Threading.Tasks.Task;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class AdministrativeDecisionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<AdministrativeDecisions> _AdministrativeDecisionsrepository;

        public AdministrativeDecisionsController(AppDbContext context, IRepository<AdministrativeDecisions> AdministrativeDecisionsrepository)
        {

            _context = context;
            _AdministrativeDecisionsrepository = AdministrativeDecisionsrepository;
        }

        // GET: EmployeesAffsirs/AdministrativeDecisions
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdministrativeDecisions.Include(a => a.Currency).Include(a => a.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/AdministrativeDecisions/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrativeDecisions = await _context.AdministrativeDecisions
                .Include(a => a.Currency)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrativeDecisions == null)
            {
                return NotFound();
            }

            return View(administrativeDecisions);
        }

        // GET: EmployeesAffsirs/AdministrativeDecisions/Create
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create(int? id)
        {
            await PopulateDropdownListsAsync();
            if (id != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var administrativeDecisions = await _AdministrativeDecisionsrepository.GetByIdAsync(id);
                if (administrativeDecisions == null)
                {
                    return NotFound();
                }

                return View(administrativeDecisions);
            }
            else
            {


                return View();
            }

        }

        // POST: EmployeesAffsirs/AdministrativeDecisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create(int? id,AdministrativeDecisions administrativeDecisions)
        {
            await PopulateDropdownListsAsync();
            if (id == null)
            {


                if (ModelState.IsValid)
                {
                    try
                    {
                        var employee = _context.employee.Find(administrativeDecisions.EmployeeId);
                        if (employee == null)
                        {
                            TempData["Error"] = "لم يتم العثور على بيانات الموظفين";
                            return View(administrativeDecisions);
                        }
                        else
                        {
                            var status = _context.financialStatements.FirstOrDefault(x => x.EmployeeId == employee.Id);
                            if (employee.EmploymentStatus == "3" || employee.EmploymentStatus == "6")
                            {
                                if (status == null)
                                {
                                    ViewData["Reload"] = "الموظف لايحتوي علي بيانات مالية جاري تحديث البيانات المالية";
                                    await Task.Delay(4000);
                                    employee.EmploymentStatus = administrativeDecisions.EmployeeStatus;
                                    FinancialStatements st = new FinancialStatements
                                    {
                                        NatureOfEmployment = administrativeDecisions.EmployeeStatus,
                                        SalaryEndDate = administrativeDecisions.SalaryEndtDate,
                                        SalaryStartDate = administrativeDecisions.SalaryStartDate,
                                        EmployeeId = administrativeDecisions.EmployeeId,
                                        CurrencyId = administrativeDecisions.CurrencyId,
                                        BasicSalary = administrativeDecisions.Salary
                                    };
                                    _context.employee.Update(employee);
                                    await _context.financialStatements.AddAsync(st);
                                    await _context.SaveChangesAsync();
                                    await _AdministrativeDecisionsrepository.AddAsync(administrativeDecisions);
                                    ViewData["Success"] = "تم تحديث البيانات بنجاح";

                                    return RedirectToAction(nameof(Index));
                                }

                            }
                            if (status == null)
                            {
                                TempData["Error"] = "الموظف لايحتوي علي بيانات مالية يرجى تعبئة البيانات المالية في قائمة موظفين!!";

                                return View(administrativeDecisions);
                            }

                            if (administrativeDecisions.EmployeeStatus == "3" && employee.EmploymentStatus == "3")
                            {
                                TempData["Error"] = "لا يمكن تقديم القرار للموظف لأن طبيعة التوظيف لم تتغير";
                                return View(administrativeDecisions);
                            }
                            else if (administrativeDecisions.EmployeeStatus == "6" && employee.EmploymentStatus == "6")
                            {
                                TempData["Error"] = "لا يمكن تقديم القرار للموظف لأن طبيعة التوظيف لم تتغير";
                                return View(administrativeDecisions);
                            }
                            else if (administrativeDecisions.EmployeeStatus == employee.EmploymentStatus
                                && administrativeDecisions.SalaryStartDate == status.SalaryStartDate
                                && administrativeDecisions.SalaryEndtDate == status.SalaryEndDate
                                && administrativeDecisions.Salary == status.BasicSalary)
                            {
                                TempData["Error"] = "بيانات الموظف هي نفسها لم تتغير يرجى تحديث البيانات!!";
                                return View(administrativeDecisions);
                            }
                            if (employee.EmploymentStatus == "3" && administrativeDecisions.EmployeeStatus != "3")
                            {
                                employee.EmploymentStatus = administrativeDecisions.EmployeeStatus;
                                status.NatureOfEmployment = administrativeDecisions.EmployeeStatus;
                                status.SalaryStartDate = administrativeDecisions.SalaryStartDate;
                                status.SalaryEndDate = administrativeDecisions.SalaryEndtDate;
                                status.BasicSalary = administrativeDecisions.Salary;
                                status.CurrencyId = administrativeDecisions.CurrencyId;
                                employee.PlacementDate = status.SalaryStartDate;

                                _context.employee.Update(employee);
                                _context.financialStatements.Update(status);
                                await _context.SaveChangesAsync();
                                await _AdministrativeDecisionsrepository.AddAsync(administrativeDecisions);
                                TempData["Success"] = "تم تحديث البيانات بنجاح";
                                return RedirectToAction(nameof(Index));
                            }
                            else if (employee.EmploymentStatus == "6" && administrativeDecisions.EmployeeStatus != "6")
                            {
                                employee.EmploymentStatus = administrativeDecisions.EmployeeStatus;
                                status.NatureOfEmployment = administrativeDecisions.EmployeeStatus;
                                status.SalaryStartDate = administrativeDecisions.SalaryStartDate;
                                status.SalaryEndDate = administrativeDecisions.SalaryEndtDate;
                                status.BasicSalary = administrativeDecisions.Salary;
                                status.CurrencyId = administrativeDecisions.CurrencyId;
                                employee.PlacementDate = status.SalaryStartDate;
                                _context.employee.Update(employee);
                                _context.financialStatements.Update(status);
                                await _context.SaveChangesAsync();
                                await _AdministrativeDecisionsrepository.AddAsync(administrativeDecisions);

                                TempData["Success"] = "تم تحديث البيانات بنجاح";
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                employee.EmploymentStatus = administrativeDecisions.EmployeeStatus;
                                status.NatureOfEmployment = administrativeDecisions.EmployeeStatus;
                                status.SalaryStartDate = administrativeDecisions.SalaryStartDate;
                                status.BasicSalary = administrativeDecisions.Salary;
                                status.CurrencyId = administrativeDecisions.CurrencyId;

                                _context.employee.Update(employee);
                                _context.financialStatements.Update(status);
                                await _context.SaveChangesAsync();
                                await _AdministrativeDecisionsrepository.AddAsync(administrativeDecisions);

                                TempData["Success"] = "تم تحديث البيانات بنجاح";
                                return RedirectToAction(nameof(Index));

                            }


                        }


                    }
                    catch (Exception ex)
                    {
                        TempData["SystemError"] = ex.Message;
                        return View(administrativeDecisions);
                    }
                }
                TempData["Error"] = " البيانات المدخلة غير صحيحة!!";
                ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", administrativeDecisions.CurrencyId);
                ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", administrativeDecisions.EmployeeId);
                return View(administrativeDecisions);

            }
            else
            {

                if (id != administrativeDecisions.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var employee = _context.employee.Find(administrativeDecisions.EmployeeId);
                        if (employee == null)
                        {
                            TempData["Error"] = "لم يتم العثور على بيانات الموظفين";
                            return View(administrativeDecisions);
                        }
                        else
                        {
                            var status = _context.financialStatements.FirstOrDefault(x => x.EmployeeId == employee.Id);
                            if (employee.EmploymentStatus == "3" || employee.EmploymentStatus == "6")
                            {
                                if (status == null)
                                {
                                    TempData["Reload"] = "الموظف لايحتوي علي بيانات مالية جاري تحديث البيانات المالية";
                                    Task.Delay(3000);
                                    FinancialStatements st = new FinancialStatements();
                                    employee.EmploymentStatus = administrativeDecisions.EmployeeStatus;
                                    st.SalaryEndDate = administrativeDecisions.SalaryEndtDate;
                                    st.SalaryStartDate = administrativeDecisions.SalaryStartDate;

                                    st.BasicSalary = administrativeDecisions.Salary;
                                    st.CurrencyId = administrativeDecisions.CurrencyId;

                                    _context.employee.Update(employee);
                                    _context.financialStatements.Update(st);
                                    await _context.SaveChangesAsync();
                                    await _AdministrativeDecisionsrepository.UpdateAsync(administrativeDecisions);
                                    TempData["Success"] = "تم تحديث البيانات بنجاح";
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                            if (status == null)
                            {
                                TempData["Reload"] = "الموظف لايحتوي علي بيانات مالية جاري تحديث البيانات المالية";
                                 Task.Delay(5000);
                                TempData["Error"] = "الموظف لايحتوي علي بيانات مالية يرجى تعبئة البيانات المالية في قائمة موظفين!!";

                                return View(administrativeDecisions);
                            }

                            if (administrativeDecisions.EmployeeStatus == "3" && employee.EmploymentStatus == "3")
                            {
                                TempData["Error"] = "لا يمكن تقديم القرار للموظف لأن طبيعة التوظيف لم تتغير";
                                return View(administrativeDecisions);
                            }
                            else if (administrativeDecisions.EmployeeStatus == "6" && employee.EmploymentStatus == "6")
                            {
                                TempData["Error"] = "لا يمكن تقديم القرار للموظف لأن طبيعة التوظيف لم تتغير";
                                return View(administrativeDecisions);
                            }
                            else if (administrativeDecisions.EmployeeStatus == employee.EmploymentStatus
                                && administrativeDecisions.SalaryStartDate == status.SalaryStartDate
                                && administrativeDecisions.SalaryEndtDate == status.SalaryEndDate
                                && administrativeDecisions.Salary == status.BasicSalary)
                            {
                                TempData["Error"] = "بيانات الموظف هي نفسها لم تتغير يرجى تحديث البيانات!!";
                                return View(administrativeDecisions);
                            }
                            if (employee.EmploymentStatus == "3" && administrativeDecisions.EmployeeStatus != "3")
                            {
                                employee.EmploymentStatus = administrativeDecisions.EmployeeStatus;
                                status.SalaryStartDate = administrativeDecisions.SalaryStartDate;
                                status.SalaryEndDate = administrativeDecisions.SalaryEndtDate;
                                status.BasicSalary = administrativeDecisions.Salary;
                                status.CurrencyId = administrativeDecisions.CurrencyId;

                                _context.employee.Update(employee);
                                _context.financialStatements.Update(status);
                                await _context.SaveChangesAsync();
                                await _AdministrativeDecisionsrepository.UpdateAsync(administrativeDecisions);
                                TempData["Success"] = "تم تحديث البيانات بنجاح";
                                return RedirectToAction(nameof(Index));
                            }
                            else if (employee.EmploymentStatus == "6" && administrativeDecisions.EmployeeStatus != "6")
                            {
                                employee.DateOfEmployment = status.SalaryStartDate;
                                _context.employee.Update(employee);
                                _context.financialStatements.Update(status);
                                await _context.SaveChangesAsync();
                                await _AdministrativeDecisionsrepository.UpdateAsync(administrativeDecisions);

                                TempData["Success"] = "تم تحديث البيانات بنجاح";
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                _context.employee.Update(employee);
                                _context.financialStatements.Update(status);
                                await _context.SaveChangesAsync();
                                await _AdministrativeDecisionsrepository.UpdateAsync(administrativeDecisions);

                                TempData["Success"] = "تم تحديث البيانات بنجاح";
                                return RedirectToAction(nameof(Index));

                            }


                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdministrativeDecisionsExists(administrativeDecisions.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return View(administrativeDecisions);
            }
        }

        // GET: EmployeesAffsirs/AdministrativeDecisions/Delete/5
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrativeDecisions = await _context.AdministrativeDecisions
                .Include(a => a.Currency)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrativeDecisions == null)
            {
                return NotFound();
            }

            return View(administrativeDecisions);
        }

        // POST: EmployeesAffsirs/AdministrativeDecisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrativeDecisions = await _AdministrativeDecisionsrepository.GetByIdAsync(id);
            if (administrativeDecisions != null)
            {
                await _AdministrativeDecisionsrepository.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrativeDecisionsExists(int id)
        {
            return _context.AdministrativeDecisions.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            //-----------------------Employee---------------------------
            var employee = await _context.employee.ToListAsync();
            ViewData["Employee"] = new SelectList(employee, "Id", "EmployeeName");
            //-----------------------Currency---------------------------
            var currency = await _context.Currency.ToListAsync();
            ViewData["Currency"] = new SelectList(currency, "Id", "CurrencyName");

            List<EmployeeStatusList> employeeStatus = new List<EmployeeStatusList>
            {
                new EmployeeStatusList () { id = 1, name = "مثبت" },
                new EmployeeStatusList () { id = 2, name = "متعاقد" },
                new EmployeeStatusList () { id = 3, name = "متدرب" },
                new EmployeeStatusList () { id = 4, name = "مستمر" },
                new EmployeeStatusList () { id = 5, name = "موقف" },
                new EmployeeStatusList () { id = 6, name = "تم إنهاء الخدمة" },
                new EmployeeStatusList () { id = 7, name = "حارس أمن" }
            };
            SelectList listItems = new SelectList(employeeStatus, "id", "name");
            ViewData["Status"] = listItems;
        }
        public IActionResult GetEmployee(int id)
        {
            if (id != 0)
            {
                if(id == 1)
                {
                    var employee = _context.employee.Where(x => x.EmploymentStatus == "3" || x.EmploymentStatus == "6").ToList();
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
                    var employee = _context.employee.Where(x => x.EmploymentStatus != "3" && x.EmploymentStatus != "6").ToList();
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

            return Json(new { error = "لم يتم العثور على أي موظفين" });
        }
        public IActionResult EmployeeData(int id)
        {
            if (id != 0)
            {

                    var employee = _context.employee.Find(id);
                    if (employee != null)
                    {
                        return Json( employee );
                    }
                    else
                    {
                        return Json(new { error = "لم يتم العثور على بيانات الموظفين" });
                    }

            }

            return Json(new { error = "لم يتم العثور على أي موظفين" });
        }
        public IActionResult JopData(int id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Find(id);
                if (employee != null)
                {
                    var jop = _context.JobDescription.Where(x => x.Id == employee.JobDescriptionId);
                    var jopList = jop.Select(x => new { id = x.Id, name = x.JopName });
                    return Json(new { jopList });
                }
                else
                {
                    return Json(new { error = "لم يتم العثور على بيانات الموظفين" });
                }

            }

            return Json(new { error = "لم يتم العثور على أي موظفين" });
        }
        public IActionResult SalaryData(int id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Find(id);
                if (employee != null)
                {
                    var statements = _context.financialStatements.Where(x => x.EmployeeId == employee.Id);
                    var statementsList = statements.Select(x => new { id = x.Id, name = x.BasicSalary,currency = x.CurrencyId ,date = x.SalaryStartDate });
                    return Json(new { statementsList });
                }
                else
                {
                    return Json(new { error = "لم يتم العثور على بيانات الموظفين" });
                }

            }

            return Json(new { error = "لم يتم العثور على أي موظفين" });
        }
        public IActionResult BranchData(int id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Find(id);
                
                if (employee != null)
                {
                    var departments = _context.Departments.FirstOrDefault(x => x.Id == employee.DepartmentsId);
                    if (departments != null)
                    {
                        var sector = _context.sectors.FirstOrDefault(x => x.Id == departments.SectorsId);
                        if (sector != null)
                        {
                            var branch = _context.branches.Where(x => x.Id == sector.BranchesId);
                            var branchList = branch.Select(x => new { id = x.Id, name = x.BranchesName });
                            return Json(new { branchList });
                        }
                        else
                        {
                            return Json(new { error = "لم يتم العثور على بيانات القطاع" });
                        }
                    }
                    else
                    {
                        return Json(new { error = "لم يتم العثور على بيانات الادارة" });
                    }
                }
                else
                {
                    return Json(new { error = "لم يتم العثور على بيانات الموظفين" });
                }

            }

            return Json(new { error = "لم يتم العثور على أي موظفين" });
        }
    }
}
