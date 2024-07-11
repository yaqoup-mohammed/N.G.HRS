using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class EntitlementsAndDeductionsController : Controller
    {
        private readonly AppDbContext _context;

        public EntitlementsAndDeductionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/EntitlementsAndDeductions
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EntitlementsAndDeductions.Include(e => e.Account).Include(e => e.Currency).Include(e => e.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/EntitlementsAndDeductions/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entitlementsAndDeductions = await _context.EntitlementsAndDeductions
                .Include(e => e.Account)
                .Include(e => e.Currency)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entitlementsAndDeductions == null)
            {
                return NotFound();
            }

            return View(entitlementsAndDeductions);
        }

        // GET: PayRoll/EntitlementsAndDeductions/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");


            return View();
        }

        // POST: PayRoll/EntitlementsAndDeductions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Date,Month,EmployeeId,Type,Taxable,FinanceAccountTypeId,Amount,Percentage,CurrencyId,Note")] EntitlementsAndDeductions entitlementsAndDeductions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entitlementsAndDeductions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", entitlementsAndDeductions.FinanceAccountTypeId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", entitlementsAndDeductions.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", entitlementsAndDeductions.EmployeeId);
            
            return View(entitlementsAndDeductions);
        }

        // GET: PayRoll/EntitlementsAndDeductions/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entitlementsAndDeductions = await _context.EntitlementsAndDeductions.FindAsync(id);
            if (entitlementsAndDeductions == null)
            {
                return NotFound();
            }
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", entitlementsAndDeductions.FinanceAccountTypeId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", entitlementsAndDeductions.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", entitlementsAndDeductions.EmployeeId);

            return View(entitlementsAndDeductions);
        }

        // POST: PayRoll/EntitlementsAndDeductions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Month,EmployeeId,Type,Taxable,FinanceAccountTypeId,Amount,Percentage,CurrencyId,Note")] EntitlementsAndDeductions entitlementsAndDeductions)
        {
            if (id != entitlementsAndDeductions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entitlementsAndDeductions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntitlementsAndDeductionsExists(entitlementsAndDeductions.Id))
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
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", entitlementsAndDeductions.FinanceAccountTypeId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", entitlementsAndDeductions.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", entitlementsAndDeductions.EmployeeId);
            return View(entitlementsAndDeductions);
        }

        // GET: PayRoll/EntitlementsAndDeductions/Delete/5
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entitlementsAndDeductions = await _context.EntitlementsAndDeductions
                .Include(e => e.Account)
                .Include(e => e.Currency)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entitlementsAndDeductions == null)
            {
                return NotFound();
            }

            return View(entitlementsAndDeductions);
        }

        // POST: PayRoll/EntitlementsAndDeductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entitlementsAndDeductions = await _context.EntitlementsAndDeductions.FindAsync(id);
            if (entitlementsAndDeductions != null)
            {
                _context.EntitlementsAndDeductions.Remove(entitlementsAndDeductions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntitlementsAndDeductionsExists(int id)
        {
            return _context.EntitlementsAndDeductions.Any(e => e.Id == id);
        }
    }
}
