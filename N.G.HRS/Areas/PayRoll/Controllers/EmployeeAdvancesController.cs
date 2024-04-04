using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;
namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class EmployeeAdvancesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeAdvancesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/EmployeeAdvances
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeeAdvances
                .Include(e => e.Currency)
                .Include(e => e.Employee)
                .Include(e => e.EmployeeAccount)
                .Include(e => e.Departments)
                .Include(e => e.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/EmployeeAdvances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAdvances = await _context.EmployeeAdvances
                .Include(e => e.Currency)
                .Include(e => e.Employee)
                .Include(e => e.EmployeeAccount)
                .Include(e => e.Departments)
                .Include(e => e.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAdvances == null)
            {
                return NotFound();
            }

            return View(employeeAdvances);
        }

        // GET: PayRoll/EmployeeAdvances/Create
        public async Task<IActionResult> Create(int? id)

        {
            if (id == null)
            {
                await PopulateDropdownListsAsync();
                return View();
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employeeAdvances = await _context.EmployeeAdvances.FindAsync(id);
                if (employeeAdvances == null)
                {   
                    return NotFound();
                }
                await PopulateDropdownListsAsync();

                return View(employeeAdvances);

            }
        }

        // POST: PayRoll/EmployeeAdvances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, EmployeeAdvances employeeAdvances)
        {
            if (id == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employeeAdvances);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                await PopulateDropdownListsAsync();

                return View(employeeAdvances);
            }
            else
            {
                if (id != employeeAdvances.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(employeeAdvances);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeAdvancesExists(employeeAdvances.Id))
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
                await PopulateDropdownListsAsync();

                return View(employeeAdvances);
            }
               
        }

        //// GET: PayRoll/EmployeeAdvances/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employeeAdvances = await _context.EmployeeAdvances.FindAsync(id);
        //    if (employeeAdvances == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
        //    ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
        //    ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
        //    return View(employeeAdvances);
        //}

        //// POST: PayRoll/EmployeeAdvances/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,DepartmentId,SectionId,EmployeeAccountId,CurrencyId,Amount,Notes")] EmployeeAdvances employeeAdvances)
        //{
        //    if (id != employeeAdvances.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(employeeAdvances);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EmployeeAdvancesExists(employeeAdvances.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
        //    ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
        //    ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
        //    return View(employeeAdvances);
        //}

        // GET: PayRoll/EmployeeAdvances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAdvances = await _context.EmployeeAdvances
                .Include(e => e.Currency)
                .Include(e => e.Employee)
                .Include(e => e.EmployeeAccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAdvances == null)
            {
                return NotFound();
            }

            return View(employeeAdvances);
        }

        // POST: PayRoll/EmployeeAdvances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeAdvances = await _context.EmployeeAdvances.FindAsync(id);
            if (employeeAdvances != null)
            {
                _context.EmployeeAdvances.Remove(employeeAdvances);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAdvancesExists(int id)
        {
            return _context.EmployeeAdvances.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            //-----------------------Sections---------------------------
            var Sections = await _context.Sections.ToListAsync();
            ViewData["Sections"] = new SelectList(Sections, "Id", "SectionsName");
            //--------------------------------------
            var Currency = await _context.Currency.ToListAsync();
            ViewData["CurrencyId"] = new SelectList(Currency, "Id", "CurrencyCode");
            var Employee = await _context.employee.ToListAsync();
            ViewData["EmployeeId"] = new SelectList(Employee, "Id", "EmployeeName");
            var Department = await _context.Departments.ToListAsync();
            ViewData["DepartmentId"] = new SelectList(Department, "Id", "SubAdministration");
            var EmployeeAccount = await _context.FinanceAccount.ToListAsync();
            ViewData["EmployeeAccountId"] = new SelectList(EmployeeAccount, "Id", "Name");
        }
            public IActionResult LoadData(int id)
        {
            if (id != 0)
            {
                var employee = _context.employee.Find(id);
                if (employee != null)
                {
                    return Ok(employee);
                }
                return NotFound();

            }

            return NotFound();

        }

        public IActionResult LoadAccount(int id)   // يوجد تعديل هنا  ============
        {
            if (id != 0)
            {
                var employee = _context.employee.Find(id);
                if (employee != null)
                {
                    var account = _context.EmployeeAccount.Include(e => e.EmployeeId).FirstOrDefault(e => e.EmployeeId == id);
                    var finance = _context.FinanceAccount.Include(e => e.EmployeeAccountsList).FirstOrDefault(e => e.Id == account.Id);
                    return Ok(finance);
                }
                return NotFound();

            }

            return NotFound();

        }
    }
}
