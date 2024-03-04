using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class EmployeeAccountsController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeAccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SalariesAndWages/EmployeeAccounts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeeAccount.Include(e => e.FinanceAccount).Include(e => e.FinanceAccountType).Include(e => e.employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SalariesAndWages/EmployeeAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAccount = await _context.EmployeeAccount
                .Include(e => e.FinanceAccount)
                .Include(e => e.FinanceAccountType)
                .Include(e => e.employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            return View(employeeAccount);
        }

        // GET: SalariesAndWages/EmployeeAccounts/Create
        public IActionResult Create()
        {
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id");
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: SalariesAndWages/EmployeeAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Notes,EmployeeId,FinanceAccountTypeId,FinanceAccountId")] EmployeeAccount employeeAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id", employeeAccount.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id", employeeAccount.FinanceAccountTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAccount.EmployeeId);
            return View(employeeAccount);
        }

        // GET: SalariesAndWages/EmployeeAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAccount = await _context.EmployeeAccount.FindAsync(id);
            if (employeeAccount == null)
            {
                return NotFound();
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id", employeeAccount.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id", employeeAccount.FinanceAccountTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAccount.EmployeeId);
            return View(employeeAccount);
        }

        // POST: SalariesAndWages/EmployeeAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Notes,EmployeeId,FinanceAccountTypeId,FinanceAccountId")] EmployeeAccount employeeAccount)
        {
            if (id != employeeAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeAccountExists(employeeAccount.Id))
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
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id", employeeAccount.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id", employeeAccount.FinanceAccountTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAccount.EmployeeId);
            return View(employeeAccount);
        }

        // GET: SalariesAndWages/EmployeeAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAccount = await _context.EmployeeAccount
                .Include(e => e.FinanceAccount)
                .Include(e => e.FinanceAccountType)
                .Include(e => e.employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            return View(employeeAccount);
        }

        // POST: SalariesAndWages/EmployeeAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeAccount = await _context.EmployeeAccount.FindAsync(id);
            if (employeeAccount != null)
            {
                _context.EmployeeAccount.Remove(employeeAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAccountExists(int id)
        {
            return _context.EmployeeAccount.Any(e => e.Id == id);
        }
    }
}
