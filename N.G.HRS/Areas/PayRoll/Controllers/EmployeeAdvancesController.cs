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
            var appDbContext = _context.EmployeeAdvances.Include(e => e.Currency).Include(e => e.Employee).Include(e => e.EmployeeAccount);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/EmployeeAdva nces/Details/5
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAdvances == null)
            {
                return NotFound();
            }

            return View(employeeAdvances);
        }

        // GET: PayRoll/EmployeeAdvances/Create
        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration");
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName");

            return View();
        }

        // POST: PayRoll/EmployeeAdvances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,DepartmentId,SectionId,EmployeeAccountId,CurrencyId,Amount,Notes")] EmployeeAdvances employeeAdvances)
        {
            if (ModelState.IsValid)
            {
                EmployeeAdvances advances = new EmployeeAdvances
                {
                    EmployeeId = employeeAdvances.EmployeeId,
                    SectionId=employeeAdvances.SectionId,
                    DepartmentId = employeeAdvances.DepartmentId,
                    EmployeeAccountId = employeeAdvances.EmployeeAccountId,
                    CurrencyId = employeeAdvances.CurrencyId,
                    Amount = employeeAdvances.Amount,
                    Notes = employeeAdvances.Notes,


                };

                _context.Add(employeeAdvances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employeeAdvances.EmployeeId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", employeeAdvances.EmployeeId);
            ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
            return View(employeeAdvances);
        }

        // GET: PayRoll/EmployeeAdvances/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employeeAdvances.EmployeeId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", employeeAdvances.EmployeeId);
            ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
            return View(employeeAdvances);
        }

        // POST: PayRoll/EmployeeAdvances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,DepartmentId,SectionId,EmployeeAccountId,CurrencyId,Amount,Notes")] EmployeeAdvances employeeAdvances)
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
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employeeAdvances.EmployeeId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", employeeAdvances.EmployeeId);
            ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
            return View(employeeAdvances);
        }

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
        public IActionResult LoadEmployee(int id)
        {
            if (id != 0)
            {
                var sections = _context.employee.FirstOrDefault(x => x.Id == id);

                return Json(sections);
            }
            return NotFound();
        }    
        public IActionResult LoadEmployeeAccount(int id)
        {
            if (id != 0)
            {
                var account = _context.EmployeeAccount.FirstOrDefault(x => x.EmployeeId == id);
                if (account != null) { 
                    return Json(account);
                }
            }
            return NotFound();
        }
        public IActionResult EmployeeAccount(int id)
        {
            if (id != 0)
            {
                var account = _context.EmployeeAccount.Include(x=>x.FinanceAccount).Where(x => x.EmployeeId == id).Select(x => new {id=x.FinanceAccountId,name=x.FinanceAccount.Name});
                if (account != null) { 
                    return Json(new { account });
                }
            }
            return NotFound();
        }
    }
}
