using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.Finance.Controllers
{
    [Area("Finance")]
    public class FinanceAccountsController : Controller
    {
        private readonly AppDbContext _context;

        public FinanceAccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Finance/FinanceAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.FinanceAccount.ToListAsync());
        }

        // GET: Finance/FinanceAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccount = await _context.FinanceAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financeAccount == null)
            {
                return NotFound();
            }

            return View(financeAccount);
        }

        // GET: Finance/FinanceAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Finance/FinanceAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] FinanceAccount financeAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financeAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(financeAccount);
        }

        // GET: Finance/FinanceAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccount = await _context.FinanceAccount.FindAsync(id);
            if (financeAccount == null)
            {
                return NotFound();
            }
            return View(financeAccount);
        }

        // POST: Finance/FinanceAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] FinanceAccount financeAccount)
        {
            if (id != financeAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financeAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinanceAccountExists(financeAccount.Id))
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
            return View(financeAccount);
        }

        // GET: Finance/FinanceAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccount = await _context.FinanceAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financeAccount == null)
            {
                return NotFound();
            }

            return View(financeAccount);
        }

        // POST: Finance/FinanceAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financeAccount = await _context.FinanceAccount.FindAsync(id);
            if (financeAccount != null)
            {
                _context.FinanceAccount.Remove(financeAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinanceAccountExists(int id)
        {
            return _context.FinanceAccount.Any(e => e.Id == id);
        }
    }
}
