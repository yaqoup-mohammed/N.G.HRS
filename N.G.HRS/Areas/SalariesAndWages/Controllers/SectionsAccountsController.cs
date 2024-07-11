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
    public class SectionsAccountsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<SectionsAccounts> _sectionsAccountsRepository;

        public SectionsAccountsController(AppDbContext context, IRepository<SectionsAccounts> sectionsAccountsRepository)
        {
            _context = context;
            _sectionsAccountsRepository = sectionsAccountsRepository;
        }

        // GET: SalariesAndWages/SectionsAccounts
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SectionsAccounts.Include(s => s.FinanceAccount).Include(s => s.FinanceAccountType).Include(s => s.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SalariesAndWages/SectionsAccounts/Details/5
        [Authorize(Policy = "DetailsPolicy")]

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
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name");
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name");
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            return View();
        }

        // POST: SalariesAndWages/SectionsAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("id,Notes,FinanceAccountTypeId,FinanceAccountId,SectionsId")] SectionsAccounts sectionsAccounts)
        {
            if (ModelState.IsValid)
            {
               await _sectionsAccountsRepository.AddAsync(sectionsAccounts);
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Create));

                //return RedirectToAction(nameof(Index));
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name", sectionsAccounts.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", sectionsAccounts.FinanceAccountTypeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", sectionsAccounts.SectionsId);
            return View(sectionsAccounts);
        }

        // GET: SalariesAndWages/SectionsAccounts/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectionsAccounts = await _sectionsAccountsRepository.GetByIdAsync(id);
            if (sectionsAccounts == null)
            {
                return NotFound();
            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name", sectionsAccounts.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", sectionsAccounts.FinanceAccountTypeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", sectionsAccounts.SectionsId);
            return View(sectionsAccounts);
        }

        // POST: SalariesAndWages/SectionsAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
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
                     await _sectionsAccountsRepository.UpdateAsync(sectionsAccounts);
                    TempData ["Success"] = "تمت التعديل بنجاح";
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
                return View(sectionsAccounts);
                //return RedirectToAction(nameof(Index));

            }
            ViewData["FinanceAccountId"] = new SelectList(_context.Set<FinanceAccount>(), "Id", "Name", sectionsAccounts.FinanceAccountId);
            ViewData["FinanceAccountTypeId"] = new SelectList(_context.FinanceAccountType, "Id", "Name", sectionsAccounts.FinanceAccountTypeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", sectionsAccounts.SectionsId);
            return View(sectionsAccounts);
        }

        // GET: SalariesAndWages/SectionsAccounts/Delete/5
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectionsAccounts = await _sectionsAccountsRepository.GetByIdAsync(id);
            if (sectionsAccounts != null)
            {
                _context.SectionsAccounts.Remove(sectionsAccounts);
            }

            await _context.SaveChangesAsync();
            TempData ["Success"] = "تمت الحذف بنجاح";
            return RedirectToAction(nameof(Create));

            //return RedirectToAction(nameof(Index));
        }

        private bool SectionsAccountsExists(int id)
        {
            return _context.SectionsAccounts.Any(e => e.id == id);
        }
    }
}
