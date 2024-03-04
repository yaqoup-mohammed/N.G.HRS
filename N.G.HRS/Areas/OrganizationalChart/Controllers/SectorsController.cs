using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.OrganizationalChart.Controllers
{
    [Area("OrganizationalChart")]
    public class SectorsController : Controller
    {
        private readonly AppDbContext _context;

        public SectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/Sectors
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.sectors.Include(s => s.Branches);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Sectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.sectors
                .Include(s => s.Branches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // GET: OrganizationalChart/Sectors/Create
        public IActionResult Create()
        {
            ViewData["BranchesId"] = new SelectList(_context.branches, "Id", "BranchesName");
            return View();
        }

        // POST: OrganizationalChart/Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SectorsName,Notes,BranchesId")] Sectors sectors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchesId"] = new SelectList(_context.branches, "Id", "BranchesName", sectors.BranchesId);
            return View(sectors);
        }

        // GET: OrganizationalChart/Sectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.sectors.FindAsync(id);
            if (sectors == null)
            {
                return NotFound();
            }
            ViewData["BranchesId"] = new SelectList(_context.branches, "Id", "BranchesName", sectors.BranchesId);
            return View(sectors);
        }

        // POST: OrganizationalChart/Sectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectorsName,Notes,BranchesId")] Sectors sectors)
        {
            if (id != sectors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorsExists(sectors.Id))
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
            ViewData["BranchesId"] = new SelectList(_context.branches, "Id", "BranchesName", sectors.BranchesId);
            return View(sectors);
        }

        // GET: OrganizationalChart/Sectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.sectors
                .Include(s => s.Branches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // POST: OrganizationalChart/Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectors = await _context.sectors.FindAsync(id);
            if (sectors != null)
            {
                _context.sectors.Remove(sectors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectorsExists(int id)
        {
            return _context.sectors.Any(e => e.Id == id);
        }
    }
}
