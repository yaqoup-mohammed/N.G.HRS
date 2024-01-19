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
    public class SectionsController : Controller
    {
        private readonly AppDbContext _context;

        public SectionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/Sections
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Sections.Include(s => s.Departments);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections
                .Include(s => s.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sections == null)
            {
                return NotFound();
            }

            return View(sections);
        }

        // GET: OrganizationalChart/Sections/Create
        public IActionResult Create()
        {
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration");
            return View();
        }

        // POST: OrganizationalChart/Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SectionsName,Notes,DepartmentsId")] Sections sections)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sections);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", sections.DepartmentsId);
            return View(sections);
        }

        // GET: OrganizationalChart/Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections.FindAsync(id);
            if (sections == null)
            {
                return NotFound();
            }
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", sections.DepartmentsId);
            return View(sections);
        }

        // POST: OrganizationalChart/Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionsName,Notes,DepartmentsId")] Sections sections)
        {
            if (id != sections.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sections);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionsExists(sections.Id))
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
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", sections.DepartmentsId);
            return View(sections);
        }

        // GET: OrganizationalChart/Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections
                .Include(s => s.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sections == null)
            {
                return NotFound();
            }

            return View(sections);
        }

        // POST: OrganizationalChart/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sections = await _context.Sections.FindAsync(id);
            if (sections != null)
            {
                _context.Sections.Remove(sections);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionsExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
