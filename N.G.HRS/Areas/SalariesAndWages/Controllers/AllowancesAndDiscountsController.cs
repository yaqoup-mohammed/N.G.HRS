using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class AllowancesAndDiscountsController : Controller
    {
        private readonly AppDbContext _context;

        public AllowancesAndDiscountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.allowancesAndDiscounts.ToListAsync());
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancesAndDiscounts = await _context.allowancesAndDiscounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allowancesAndDiscounts == null)
            {
                return NotFound();
            }

            return View(allowancesAndDiscounts);
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalariesAndWages/AllowancesAndDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Taxable,AddedToAllEmployees,CumulativeAllowance,SubjectToInsurance,Amount,Percentage,Notes")] AllowancesAndDiscounts allowancesAndDiscounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allowancesAndDiscounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allowancesAndDiscounts);
        }

        // GET: SalariesAndWages/AllowancesAndDiscounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancesAndDiscounts = await _context.allowancesAndDiscounts.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Taxable,AddedToAllEmployees,CumulativeAllowance,SubjectToInsurance,Amount,Percentage,Notes")] AllowancesAndDiscounts allowancesAndDiscounts)
        {
            if (id != allowancesAndDiscounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allowancesAndDiscounts);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancesAndDiscounts = await _context.allowancesAndDiscounts
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allowancesAndDiscounts = await _context.allowancesAndDiscounts.FindAsync(id);
            if (allowancesAndDiscounts != null)
            {
                _context.allowancesAndDiscounts.Remove(allowancesAndDiscounts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllowancesAndDiscountsExists(int id)
        {
            return _context.allowancesAndDiscounts.Any(e => e.Id == id);
        }
    }
}
