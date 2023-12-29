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
    public class GovernoratesController : Controller
    {
        private readonly AppDbContext _context;

        public GovernoratesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/Governorates
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.governorates.Include(g => g.CountryOne);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GeneralConfiguration/Governorates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governorate = await _context.governorates
                .Include(g => g.CountryOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (governorate == null)
            {
                return NotFound();
            }

            return View(governorate);
        }

        // GET: GeneralConfiguration/Governorates/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name");
            return View();
        }

        // POST: GeneralConfiguration/Governorates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,CountryId")] Governorate governorate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(governorate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", governorate.CountryId);
            return View(governorate);
        }

        // GET: GeneralConfiguration/Governorates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governorate = await _context.governorates.FindAsync(id);
            if (governorate == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", governorate.CountryId);
            return View(governorate);
        }

        // POST: GeneralConfiguration/Governorates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes,CountryId")] Governorate governorate)
        {
            if (id != governorate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(governorate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GovernorateExists(governorate.Id))
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
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", governorate.CountryId);
            return View(governorate);
        }

        // GET: GeneralConfiguration/Governorates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governorate = await _context.governorates
                .Include(g => g.CountryOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (governorate == null)
            {
                return NotFound();
            }

            return View(governorate);
        }

        // POST: GeneralConfiguration/Governorates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var governorate = await _context.governorates.FindAsync(id);
            if (governorate != null)
            {
                _context.governorates.Remove(governorate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GovernorateExists(int id)
        {
            return _context.governorates.Any(e => e.Id == id);
        }
    }
}
