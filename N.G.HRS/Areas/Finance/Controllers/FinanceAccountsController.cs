using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.Finance.Controllers
{
    [Area("Finance")]
    public class FinanceAccountsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<FinanceAccount> _financeAccountRepository;

        public FinanceAccountsController(AppDbContext context, IRepository<FinanceAccount> financeAccountRepository)
        {
            _context = context;
            _financeAccountRepository = financeAccountRepository;
        }

        // GET: Finance/FinanceAccounts
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.FinanceAccount.ToListAsync());
        }

        // GET: Finance/FinanceAccounts/Details/5
        [Authorize(Policy = "DetailsPolicy")]
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
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Finance/FinanceAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Notes")] FinanceAccount financeAccount)
        {
            if (ModelState.IsValid)
            {
              await _financeAccountRepository.AddAsync(financeAccount);
                TempData ["Success"] = "تم الحفظ بنجاح!";
                return RedirectToAction(nameof(Create));

                //return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "حدث خطأ ما!";
            }
            return View(financeAccount);
        }

        // GET: Finance/FinanceAccounts/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccount = await _financeAccountRepository.GetByIdAsync(id);
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
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Notes")] FinanceAccount financeAccount)
        {
            if (id != financeAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _financeAccountRepository.UpdateAsync(financeAccount);
                    TempData["Success"] = "تم التعديل بنجاح!";
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
                return View(financeAccount);
                //return RedirectToAction(nameof(Index));
            }
            return View(financeAccount);
        }

        // GET: Finance/FinanceAccounts/Delete/5
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financeAccount = await _financeAccountRepository.GetByIdAsync(id);
            if (financeAccount != null)
            {
                _context.FinanceAccount.Remove(financeAccount);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح!";
            return RedirectToAction(nameof(Create));

            //return RedirectToAction(nameof(Index));
        }

        private bool FinanceAccountExists(int id)
        {
            return _context.FinanceAccount.Any(e => e.Id == id);
        }
    }
}
