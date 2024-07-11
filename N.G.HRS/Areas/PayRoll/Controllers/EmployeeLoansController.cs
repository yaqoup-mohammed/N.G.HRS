using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class EmployeeLoansController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeLoansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/EmployeeLoans
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeeLoans.Include(e => e.Currency).Include(e => e.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/EmployeeLoans/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeLoans = await _context.EmployeeLoans
                .Include(e => e.Currency)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeLoans == null)
            {
                return NotFound();
            }

            return View(employeeLoans);
        }

        // GET: PayRoll/EmployeeLoans/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: PayRoll/EmployeeLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Date,InstallmentStartDate,CurrencyId,Arrest,Amount,InstallmentAmount,NumberOfInstallmentMonths,Notes")] EmployeeLoans employeeLoans)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeLoans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeLoans.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeLoans.EmployeeId);
            return View(employeeLoans);
        }

        // GET: PayRoll/EmployeeLoans/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeLoans = await _context.EmployeeLoans.FindAsync(id);
            if (employeeLoans == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeLoans.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeLoans.EmployeeId);
            return View(employeeLoans);
        }

        // POST: PayRoll/EmployeeLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Date,InstallmentStartDate,CurrencyId,Arrest,Amount,InstallmentAmount,NumberOfInstallmentMonths,Notes")] EmployeeLoans employeeLoans)
        {
            if (id != employeeLoans.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeLoans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeLoansExists(employeeLoans.Id))
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
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeLoans.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeLoans.EmployeeId);
            return View(employeeLoans);
        }

        // GET: PayRoll/EmployeeLoans/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeLoans = await _context.EmployeeLoans
                .Include(e => e.Currency)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeLoans == null)
            {
                return NotFound();
            }

            return View(employeeLoans);
        }

        // POST: PayRoll/EmployeeLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeLoans = await _context.EmployeeLoans.FindAsync(id);
            if (employeeLoans != null)
            {
                _context.EmployeeLoans.Remove(employeeLoans);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeLoansExists(int id)
        {
            return _context.EmployeeLoans.Any(e => e.Id == id);
        }
    }
}
