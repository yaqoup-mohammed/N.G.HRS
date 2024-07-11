using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PenaltiesAndViolations.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Controllers
{
    [Area("PenaltiesAndViolations")]
    public class ViolationsController : Controller
    {
        private readonly AppDbContext _context;

        public ViolationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PenaltiesAndViolations/Violations
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Violations.ToListAsync());
        }

        // GET: PenaltiesAndViolations/Violations/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violations = await _context.Violations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (violations == null)
            {
                return NotFound();
            }

            return View(violations);
        }

        // GET: PenaltiesAndViolations/Violations/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: PenaltiesAndViolations/Violations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,ViolationsName,Notes")] Violations violations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(violations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(violations);
        }

        // GET: PenaltiesAndViolations/Violations/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violations = await _context.Violations.FindAsync(id);
            if (violations == null)
            {
                return NotFound();
            }
            return View(violations);
        }

        // POST: PenaltiesAndViolations/Violations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ViolationsName,Notes")] Violations violations)
        {
            if (id != violations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violations);
                    await _context.SaveChangesAsync( );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationsExists(violations.Id))
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
            return View(violations);
        }

        // GET: PenaltiesAndViolations/Violations/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violations = await _context.Violations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (violations == null)
            {
                return NotFound();
            }

            return View(violations);
        }

        // POST: PenaltiesAndViolations/Violations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var violations = await _context.Violations.FindAsync(id);
            if (violations != null)
            {
                _context.Violations.Remove(violations);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViolationsExists(int id)
        {
            return _context.Violations.Any(e => e.Id == id);
        }
    }
}
