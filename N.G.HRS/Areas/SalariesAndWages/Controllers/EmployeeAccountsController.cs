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
using N.G.HRS.Repository;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class EmployeeAccountsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<EmployeeAccount> _employeeAccountRepository;

        public EmployeeAccountsController(AppDbContext context, IRepository<EmployeeAccount> employeeAccountRepository)
        {
            _context = context;
            _employeeAccountRepository = employeeAccountRepository;
        }

        // GET: SalariesAndWages/EmployeeAccounts
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeeAccount.Include(e => e.FinanceAccount).Include(e => e.FinanceAccountType).Include(e => e.employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SalariesAndWages/EmployeeAccounts/Details/5
        [Authorize(Policy = "DetailsPolicy")]

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
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name");
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: SalariesAndWages/EmployeeAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Notes,EmployeeId,FinanceAccountTypeId,FinanceAccountId")] EmployeeAccount employeeAccount)
        {
            if (ModelState.IsValid)
            {
               await _employeeAccountRepository.AddAsync(employeeAccount);
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Create));

                //return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "حدث خطأ ما";
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name", employeeAccount.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", employeeAccount.FinanceAccountTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAccount.EmployeeId);
            return View(employeeAccount);
        }

        // GET: SalariesAndWages/EmployeeAccounts/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAccount = await _employeeAccountRepository.GetByIdAsync(id);
            if (employeeAccount == null)
            {
                return NotFound();
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name", employeeAccount.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", employeeAccount.FinanceAccountTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAccount.EmployeeId);
            return View(employeeAccount);
        }

        // POST: SalariesAndWages/EmployeeAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
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
                    await _employeeAccountRepository.UpdateAsync(employeeAccount);
                    TempData["Success"] = "تم التعديل بنجاح";
                    
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
                return View (employeeAccount);
                //return RedirectToAction(nameof(Index));

            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name", employeeAccount.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", employeeAccount.FinanceAccountTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAccount.EmployeeId);
            return View(employeeAccount);
        }

        // GET: SalariesAndWages/EmployeeAccounts/Delete/5
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeAccount = await _employeeAccountRepository.GetByIdAsync(id);
            if (employeeAccount != null)
            {
                _context.EmployeeAccount.Remove(employeeAccount);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));
            //return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAccountExists(int id)
        {
            return _context.EmployeeAccount.Any(e => e.Id == id);
        }
    }
}
