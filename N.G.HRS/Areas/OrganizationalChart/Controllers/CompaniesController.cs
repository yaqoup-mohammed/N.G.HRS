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
    public class CompaniesController : Controller
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/Companies
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.company.Include(c => c.BoardOfDirectors);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.company
                .Include(c => c.BoardOfDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: OrganizationalChart/Companies/Create
        public IActionResult Create()
        {
            ViewData["BoardOfDirectorsId"] = new SelectList(_context.boardOfDirectors, "Id", "CouncilName");
            return View();
        }

        // POST: OrganizationalChart/Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,CompanyName,LicenseNumber,TypeOfBusinessActivity,ComponyLogo,ComponyAddress,Notes,BoardOfDirectorsId")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardOfDirectorsId"] = new SelectList(_context.boardOfDirectors, "Id", "CouncilName", company.BoardOfDirectorsId);
            return View(company);
        }

        // GET: OrganizationalChart/Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["BoardOfDirectorsId"] = new SelectList(_context.boardOfDirectors, "Id", "CouncilName", company.BoardOfDirectorsId);
            return View(company);
        }

        // POST: OrganizationalChart/Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CompanyName,LicenseNumber,TypeOfBusinessActivity,ComponyLogo,ComponyAddress,Notes,BoardOfDirectorsId")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            ViewData["BoardOfDirectorsId"] = new SelectList(_context.boardOfDirectors, "Id", "CouncilName", company.BoardOfDirectorsId);
            return View(company);
        }

        // GET: OrganizationalChart/Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.company
                .Include(c => c.BoardOfDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: OrganizationalChart/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.company.FindAsync(id);
            if (company != null)
            {
                _context.company.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.company.Any(e => e.Id == id);
        }
    }
}
