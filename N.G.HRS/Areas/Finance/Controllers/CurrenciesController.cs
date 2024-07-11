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
    public class CurrenciesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Currency> _repository;

        public CurrenciesController(AppDbContext context, IRepository<Currency> repository)
        {
            _context = context;
            _repository = repository;
        }


        // GET: Finance/Currencies
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Currency.ToListAsync());
        }

        // GET: Finance/Currencies/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Finance/Currencies/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Finance/Currencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,CurrencyName,CurrencyCode,CurrencyNotes,CurrentPriceOfCurrency,PreviousPriceOfCurrency")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(currency);
                TempData["Success"] = "تمت العملية بنجاح";
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Finance/Currencies/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Finance/Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,CurrencyName,CurrencyCode,CurrencyNotes,CurrentPriceOfCurrency,PreviousPriceOfCurrency")] Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _repository.UpdateAsync(currency);
                    TempData ["Success"] = "تمت تعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.Id))
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
            return View(currency);
        }

        // GET: Finance/Currencies/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Finance/Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]


        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _repository.GetByIdAsync(id);
            if (currency != null)
            {
                _context.Currency.Remove(currency);
                TempData["Success"] = "تمت الحذف بنجاح";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
