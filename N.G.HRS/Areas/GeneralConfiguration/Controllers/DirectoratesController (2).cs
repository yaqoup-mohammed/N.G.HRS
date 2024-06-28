using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class DirectoratesController : Controller
    {
        private readonly AppDbContext _context;

        public DirectoratesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/Directorates
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.directorates.Include(d => d.Governorate);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GeneralConfiguration/Directorates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorate = await _context.directorates
                .Include(d => d.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorate == null)
            {
                return NotFound();
            }

            return View(directorate);
        }

        // GET: GeneralConfiguration/Directorates/Create
        public IActionResult Create()
        {
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name");
            return View();
        }

        // POST: GeneralConfiguration/Directorates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,GovernorateId")] Directorate directorate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(directorate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", directorate.GovernorateId);
            return View(directorate);
        }

        // GET: GeneralConfiguration/Directorates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorate = await _context.directorates.FindAsync(id);
            if (directorate == null)
            {
                return NotFound();
            }
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", directorate.GovernorateId);
            return View(directorate);
        }

        // POST: GeneralConfiguration/Directorates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes,GovernorateId")] Directorate directorate)
        {
            if (id != directorate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directorate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorateExists(directorate.Id))
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
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", directorate.GovernorateId);
            return View(directorate);
        }

        // GET: GeneralConfiguration/Directorates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorate = await _context.directorates
                .Include(d => d.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorate == null)
            {
                return NotFound();
            }

            return View(directorate);
        }

        // POST: GeneralConfiguration/Directorates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorate = await _context.directorates.FindAsync(id);
            if (directorate != null)
            {
                _context.directorates.Remove(directorate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorateExists(int id)
        {
            return _context.directorates.Any(e => e.Id == id);
        }
    }
}
