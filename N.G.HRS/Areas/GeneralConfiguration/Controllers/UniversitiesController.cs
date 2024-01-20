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
    public class UniversitiesController : Controller
    {
        private readonly AppDbContext _context;

        public UniversitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/Universities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Universities.ToListAsync());
        }

        // GET: GeneralConfiguration/Universities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universities = await _context.Universities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (universities == null)
            {
                return NotFound();
            }

            return View(universities);
        }

        // GET: GeneralConfiguration/Universities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Universities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Universities universities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(universities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(universities);
        }

        // GET: GeneralConfiguration/Universities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universities = await _context.Universities.FindAsync(id);
            if (universities == null)
            {
                return NotFound();
            }
            return View(universities);
        }

        // POST: GeneralConfiguration/Universities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Universities universities)
        {
            if (id != universities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversitiesExists(universities.Id))
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
            return View(universities);
        }

        // GET: GeneralConfiguration/Universities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universities = await _context.Universities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (universities == null)
            {
                return NotFound();
            }

            return View(universities);
        }

        // POST: GeneralConfiguration/Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var universities = await _context.Universities.FindAsync(id);
            if (universities != null)
            {
                _context.Universities.Remove(universities);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversitiesExists(int id)
        {
            return _context.Universities.Any(e => e.Id == id);
        }
    }
}
