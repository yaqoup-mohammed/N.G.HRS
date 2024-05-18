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
    public class FinanceAccountTypesController : Controller
    {
        private readonly AppDbContext _context;

        public FinanceAccountTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Finance/FinanceAccountTypes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.FinanceAccountType.Include(f => f.FinanceAccount);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Finance/FinanceAccountTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccountType = await _context.FinanceAccountType
                .Include(f => f.FinanceAccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financeAccountType == null)
            {
                return NotFound();
            }

            return View(financeAccountType);
        }

        // GET: Finance/FinanceAccountTypes/Create
        public IActionResult Create()
        {
            ViewData["FinanceAccountId"] = new SelectList(_context.FinanceAccount, "Id", "Name");
            return View();
        }

        // POST: Finance/FinanceAccountTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FinanceAccountId,AccountNumber,Note")] FinanceAccountType financeAccountType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financeAccountType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.FinanceAccount, "Id", "Name", financeAccountType.FinanceAccountId);
            return View(financeAccountType);
        }

        // GET: Finance/FinanceAccountTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccountType = await _context.FinanceAccountType.FindAsync(id);
            if (financeAccountType == null)
            {
                return NotFound();
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.FinanceAccount, "Id", "Name", financeAccountType.FinanceAccountId);
            return View(financeAccountType);
        }

        // POST: Finance/FinanceAccountTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FinanceAccountId,AccountNumber,Note")] FinanceAccountType financeAccountType)
        {
            if (id != financeAccountType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financeAccountType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinanceAccountTypeExists(financeAccountType.Id))
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
            ViewData["FinanceAccountId"] = new SelectList(_context.FinanceAccount, "Id", "Name", financeAccountType.FinanceAccountId);
            return View(financeAccountType);
        }

        // GET: Finance/FinanceAccountTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccountType = await _context.FinanceAccountType
                .Include(f => f.FinanceAccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financeAccountType == null)
            {
                return NotFound();
            }

            return View(financeAccountType);
        }

        // POST: Finance/FinanceAccountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financeAccountType = await _context.FinanceAccountType.FindAsync(id);
            if (financeAccountType != null)
            {
                _context.FinanceAccountType.Remove(financeAccountType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinanceAccountTypeExists(int id)
        {
            return _context.FinanceAccountType.Any(e => e.Id == id);
        }
    }
}
