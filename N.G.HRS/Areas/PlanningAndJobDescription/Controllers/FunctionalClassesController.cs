using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Controllers
{
    [Area("PlanningAndJobDescription")]
    public class FunctionalClassesController : Controller
    {
        private readonly AppDbContext _context;

        public FunctionalClassesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PlanningAndJobDescription/FunctionalClasses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.functionalClasses.Include(f => f.Currency);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalClass = await _context.functionalClasses
                .Include(f => f.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalClass == null)
            {
                return NotFound();
            }

            return View(functionalClass);
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Create
        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode");
            return View();
        }

        // POST: PlanningAndJobDescription/FunctionalClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BasicSalary,Notes,CurrencyId")] FunctionalClass functionalClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(functionalClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode", functionalClass.CurrencyId);
            return View(functionalClass);
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalClass = await _context.functionalClasses.FindAsync(id);
            if (functionalClass == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode", functionalClass.CurrencyId);
            return View(functionalClass);
        }

        // POST: PlanningAndJobDescription/FunctionalClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BasicSalary,Notes,CurrencyId")] FunctionalClass functionalClass)
        {
            if (id != functionalClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(functionalClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionalClassExists(functionalClass.Id))
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
            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode", functionalClass.CurrencyId);
            return View(functionalClass);
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalClass = await _context.functionalClasses
                .Include(f => f.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalClass == null)
            {
                return NotFound();
            }

            return View(functionalClass);
        }

        // POST: PlanningAndJobDescription/FunctionalClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var functionalClass = await _context.functionalClasses.FindAsync(id);
            if (functionalClass != null)
            {
                _context.functionalClasses.Remove(functionalClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunctionalClassExists(int id)
        {
            return _context.functionalClasses.Any(e => e.Id == id);
        }
    }
}
