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
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/Departments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Departments.Include(d => d.Sectors);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Include(d => d.Sectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: OrganizationalChart/Departments/Create
        public IActionResult Create()
        {
            ViewData["SectorsId"] = new SelectList(_context.sectors, "Id", "SectorsName");
            return View();
        }

        // POST: OrganizationalChart/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubAdministration,Notes,SectorsId")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectorsId"] = new SelectList(_context.sectors, "Id", "SectorsName", departments.SectorsId);
            return View(departments);
        }

        // GET: OrganizationalChart/Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments.FindAsync(id);
            if (departments == null)
            {
                return NotFound();
            }
            ViewData["SectorsId"] = new SelectList(_context.sectors, "Id", "SectorsName", departments.SectorsId);
            return View(departments);
        }

        // POST: OrganizationalChart/Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubAdministration,Notes,SectorsId")] Departments departments)
        {
            if (id != departments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(departments.Id))
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
            ViewData["SectorsId"] = new SelectList(_context.sectors, "Id", "SectorsName", departments.SectorsId);
            return View(departments);
        }

        // GET: OrganizationalChart/Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Include(d => d.Sectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // POST: OrganizationalChart/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departments = await _context.Departments.FindAsync(id);
            if (departments != null)
            {
                _context.Departments.Remove(departments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
