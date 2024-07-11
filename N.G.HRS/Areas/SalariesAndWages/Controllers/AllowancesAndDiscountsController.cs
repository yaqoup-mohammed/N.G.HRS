using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class AllowancesAndDiscountsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<AllowancesAndDiscounts> _allowancesAndDiscountsRepository;

        public AllowancesAndDiscountsController(AppDbContext context, IRepository<AllowancesAndDiscounts> allowancesAndDiscountsRepository)
        {
            _context = context;
            _allowancesAndDiscountsRepository = allowancesAndDiscountsRepository;
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.allowancesAndDiscounts.Include(a => a.Currency);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancesAndDiscounts = await _context.allowancesAndDiscounts
                .Include(a => a.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allowancesAndDiscounts == null)
            {
                return NotFound();
            }

            return View(allowancesAndDiscounts);
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();

            return View();
        }

        // POST: SalariesAndWages/AllowancesAndDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,Name,Type,Taxable,AddedToAllEmployees,CumulativeAllowance,SubjectToInsurance,Amount,Percentage,Notes,CurrencyId")] AllowancesAndDiscounts allowancesAndDiscounts)
        {
            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                try
                {
                    await _allowancesAndDiscountsRepository.AddAsync(allowancesAndDiscounts);
                    TempData["success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] =" حدثت خطأ اثناء الادخال برجاء التواصل مع مدير النظام  " + ex.Message;
                    return View(allowancesAndDiscounts);
                }
            }
            return View(allowancesAndDiscounts);
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var allowancesAndDiscounts = await _allowancesAndDiscountsRepository.GetByIdAsync(id);
            if (allowancesAndDiscounts == null)
            {
                return NotFound();
            }
            return View(allowancesAndDiscounts);
        }

        // POST: SalariesAndWages/AllowancesAndDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Taxable,AddedToAllEmployees,CumulativeAllowance,SubjectToInsurance,Amount,Percentage,Notes,CurrencyId")] AllowancesAndDiscounts allowancesAndDiscounts)
        {
            if (id != allowancesAndDiscounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();
                try
                {
                    await _allowancesAndDiscountsRepository.UpdateAsync(allowancesAndDiscounts);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllowancesAndDiscountsExists(allowancesAndDiscounts.Id))
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
            return View(allowancesAndDiscounts);
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts/Delete/5
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancesAndDiscounts = await _context.allowancesAndDiscounts
                .Include(a => a.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allowancesAndDiscounts == null)
            {
                return NotFound();
            }

            return View(allowancesAndDiscounts);
        }

        // POST: SalariesAndWages/AllowancesAndDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allowancesAndDiscounts = await _allowancesAndDiscountsRepository.GetByIdAsync(id);
            if (allowancesAndDiscounts != null)
            {
                await _allowancesAndDiscountsRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AllowancesAndDiscountsExists(int id)
        {
            return _context.allowancesAndDiscounts.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var CurrencyId = await _context.Currency.ToListAsync();
            ViewData["CurrencyId"] = new SelectList(CurrencyId, "Id", "CurrencyName");
            //====================================================
        }
    }
}
