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
    public class SectionsAccountsController : Controller
    {
        private readonly AppDbContext _context;

        public SectionsAccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SalariesAndWages/SectionsAccounts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SectionsAccounts.Include(s => s.FinanceAccount).Include(s => s.FinanceAccountType).Include(s => s.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SalariesAndWages/SectionsAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectionsAccounts = await _context.SectionsAccounts
                .Include(s => s.FinanceAccount)
                .Include(s => s.FinanceAccountType)
                .Include(s => s.Sections)
                .FirstOrDefaultAsync(m => m.id == id);
            if (sectionsAccounts == null)
            {
                return NotFound();
            }

            return View(sectionsAccounts);
        }

        // GET: SalariesAndWages/SectionsAccounts/Create
        public IActionResult Create()
        {
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id");
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id");
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            return View();
        }

        // POST: SalariesAndWages/SectionsAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Notes,FinanceAccountTypeId,FinanceAccountId,SectionsId")] SectionsAccounts sectionsAccounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectionsAccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id", sectionsAccounts.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id", sectionsAccounts.FinanceAccountTypeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", sectionsAccounts.SectionsId);
            return View(sectionsAccounts);
        }

        // GET: SalariesAndWages/SectionsAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectionsAccounts = await _context.SectionsAccounts.FindAsync(id);
            if (sectionsAccounts == null)
            {
                return NotFound();
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id", sectionsAccounts.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id", sectionsAccounts.FinanceAccountTypeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", sectionsAccounts.SectionsId);
            return View(sectionsAccounts);
        }

        // POST: SalariesAndWages/SectionsAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Notes,FinanceAccountTypeId,FinanceAccountId,SectionsId")] SectionsAccounts sectionsAccounts)
        {
            if (id != sectionsAccounts.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectionsAccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionsAccountsExists(sectionsAccounts.id))
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
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Id", sectionsAccounts.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Id", sectionsAccounts.FinanceAccountTypeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", sectionsAccounts.SectionsId);
            return View(sectionsAccounts);
        }

        // GET: SalariesAndWages/SectionsAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectionsAccounts = await _context.SectionsAccounts
                .Include(s => s.FinanceAccount)
                .Include(s => s.FinanceAccountType)
                .Include(s => s.Sections)
                .FirstOrDefaultAsync(m => m.id == id);
            if (sectionsAccounts == null)
            {
                return NotFound();
            }

            return View(sectionsAccounts);
        }

        // POST: SalariesAndWages/SectionsAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectionsAccounts = await _context.SectionsAccounts.FindAsync(id);
            if (sectionsAccounts != null)
            {
                _context.SectionsAccounts.Remove(sectionsAccounts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionsAccountsExists(int id)
        {
            return _context.SectionsAccounts.Any(e => e.id == id);
        }
    }
}
