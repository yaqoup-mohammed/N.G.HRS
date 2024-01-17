using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.Employees.Controllers
{
    [Area("Employees")]
    public class FamiliesController : Controller
    {
        private readonly AppDbContext _context;

        public FamiliesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees/Families
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Family.Include(f => f.RelativesType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Employees/Families/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family
                .Include(f => f.RelativesType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // GET: Employees/Families/Create
        public IActionResult Create()
        {
            ViewData["RelativesTypeId"] = new SelectList(_context.relativesTypes, "Id", "RelativeName");
            return View();
        }

        // POST: Employees/Families/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,RelativesTypeId")] Family family)
        {
            if (ModelState.IsValid)
            {
                _context.Add(family);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RelativesTypeId"] = new SelectList(_context.relativesTypes, "Id", "RelativeName", family.RelativesTypeId);
            return View(family);
        }

        // GET: Employees/Families/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }
            ViewData["RelativesTypeId"] = new SelectList(_context.relativesTypes, "Id", "RelativeName", family.RelativesTypeId);
            return View(family);
        }

        // POST: Employees/Families/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes,RelativesTypeId")] Family family)
        {
            if (id != family.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(family);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyExists(family.Id))
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
            ViewData["RelativesTypeId"] = new SelectList(_context.relativesTypes, "Id", "RelativeName", family.RelativesTypeId);
            return View(family);
        }

        // GET: Employees/Families/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family
                .Include(f => f.RelativesType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // POST: Employees/Families/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var family = await _context.Family.FindAsync(id);
            if (family != null)
            {
                _context.Family.Remove(family);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.Id == id);
        }
    }
}
