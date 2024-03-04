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
    public class BranchesController : Controller
    {
        private readonly AppDbContext _context;

        public BranchesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/Branches
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.branches.Include(b => b.Company).Include(b => b.Country).Include(b => b.Directorate).Include(b => b.Governorate);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.branches
                .Include(b => b.Company)
                .Include(b => b.Country)
                .Include(b => b.Directorate)
                .Include(b => b.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branches == null)
            {
                return NotFound();
            }

            return View(branches);
        }

        // GET: OrganizationalChart/Branches/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.company, "Id", "CompanyName");
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name");
            ViewData["DirectorateId"] = new SelectList(_context.directorates, "Id", "Name");
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name");
            return View();
        }

        // POST: OrganizationalChart/Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchesName,BranchesAdress,BranchesPhone,BranchesEmail,Notes,CompanyId,CountryId,GovernorateId,DirectorateId")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.company, "Id", "CompanyName", branches.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", branches.CountryId);
            ViewData["DirectorateId"] = new SelectList(_context.directorates, "Id", "Name", branches.DirectorateId);
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", branches.GovernorateId);
            return View(branches);
        }

        // GET: OrganizationalChart/Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.branches.FindAsync(id);
            if (branches == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.company, "Id", "CompanyName", branches.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", branches.CountryId);
            ViewData["DirectorateId"] = new SelectList(_context.directorates, "Id", "Name", branches.DirectorateId);
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", branches.GovernorateId);
            return View(branches);
        }

        // POST: OrganizationalChart/Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchesName,BranchesAdress,BranchesPhone,BranchesEmail,Notes,CompanyId,CountryId,GovernorateId,DirectorateId")] Branches branches)
        {
            if (id != branches.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchesExists(branches.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.company, "Id", "CompanyName", branches.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", branches.CountryId);
            ViewData["DirectorateId"] = new SelectList(_context.directorates, "Id", "Name", branches.DirectorateId);
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", branches.GovernorateId);
            return View(branches);
        }

        // GET: OrganizationalChart/Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.branches
                .Include(b => b.Company)
                .Include(b => b.Country)
                .Include(b => b.Directorate)
                .Include(b => b.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branches == null)
            {
                return NotFound();
            }

            return View(branches);
        }

        // POST: OrganizationalChart/Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branches = await _context.branches.FindAsync(id);
            if (branches != null)
            {
                _context.branches.Remove(branches);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchesExists(int id)
        {
            return _context.branches.Any(e => e.Id == id);
        }
    }
}
