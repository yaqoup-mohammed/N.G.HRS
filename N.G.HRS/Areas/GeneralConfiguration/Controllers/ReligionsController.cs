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
    public class ReligionsController : Controller
    {
        private readonly AppDbContext _context;

        public ReligionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/Religions
        public async Task<IActionResult> Index()
        {
            return View(await _context.religion.ToListAsync());
        }

        // GET: GeneralConfiguration/Religions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var religion = await _context.religion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (religion == null)
            {
                return NotFound();
            }

            return View(religion);
        }

        // GET: GeneralConfiguration/Religions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Religions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Religion religion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(religion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(religion);
        }

        // GET: GeneralConfiguration/Religions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var religion = await _context.religion.FindAsync(id);
            if (religion == null)
            {
                return NotFound();
            }
            return View(religion);
        }

        // POST: GeneralConfiguration/Religions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Religion religion)
        {
            if (id != religion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(religion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReligionExists(religion.Id))
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
            return View(religion);
        }

        // GET: GeneralConfiguration/Religions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var religion = await _context.religion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (religion == null)
            {
                return NotFound();
            }

            return View(religion);
        }

        // POST: GeneralConfiguration/Religions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var religion = await _context.religion.FindAsync(id);
            if (religion != null)
            {
                _context.religion.Remove(religion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReligionExists(int id)
        {
            return _context.religion.Any(e => e.Id == id);
        }
    }
}
