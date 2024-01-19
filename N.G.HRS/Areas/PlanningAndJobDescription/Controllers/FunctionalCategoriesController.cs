using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Controllers
{
    [Area("PlanningAndJobDescription")]
    public class FunctionalCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public FunctionalCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PlanningAndJobDescription/FunctionalCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.functionalCategories.ToListAsync());
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalCategories = await _context.functionalCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalCategories == null)
            {
                return NotFound();
            }

            return View(functionalCategories);
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanningAndJobDescription/FunctionalCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriesName,Notes")] FunctionalCategories functionalCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(functionalCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(functionalCategories);
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalCategories = await _context.functionalCategories.FindAsync(id);
            if (functionalCategories == null)
            {
                return NotFound();
            }
            return View(functionalCategories);
        }

        // POST: PlanningAndJobDescription/FunctionalCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriesName,Notes")] FunctionalCategories functionalCategories)
        {
            if (id != functionalCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(functionalCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionalCategoriesExists(functionalCategories.Id))
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
            return View(functionalCategories);
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalCategories = await _context.functionalCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalCategories == null)
            {
                return NotFound();
            }

            return View(functionalCategories);
        }

        // POST: PlanningAndJobDescription/FunctionalCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var functionalCategories = await _context.functionalCategories.FindAsync(id);
            if (functionalCategories != null)
            {
                _context.functionalCategories.Remove(functionalCategories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunctionalCategoriesExists(int id)
        {
            return _context.functionalCategories.Any(e => e.Id == id);
        }
    }
}
