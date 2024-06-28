using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Date;
using N.G.HRS.HRSelectList;
using N.G.HRS.Repository;

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
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdministrativeDecisions.Include(a => a.Currency).Include(a => a.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/AdministrativeDecisions/Details/5
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
        public async Task<IActionResult> Create(int? id,AdministrativeDecisions administrativeDecisions)
        {
            await PopulateDropdownListsAsync();
            if (id == null)
            {


                if (ModelState.IsValid)
                {
                    try
                    {
                        await _AdministrativeDecisionsrepository.AddAsync(administrativeDecisions);
                        TempData["Success"] = "تمت العملية بنجاح";
                    }
                    catch (Exception ex)
                    {
                        TempData["SystemError"] = ex.Message;
                        return View(administrativeDecisions);
                    }
                    return RedirectToAction(nameof(Index));
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
                        await _AdministrativeDecisionsrepository.UpdateAsync(administrativeDecisions);
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
                    return RedirectToAction(nameof(Index));
                }

                return View(administrativeDecisions);
            }
        }

        // GET: EmployeesAffsirs/AdministrativeDecisions/Delete/5
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
    }
}
