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
    public class RelativesTypesController : Controller
    {
        private readonly AppDbContext _context;

        public RelativesTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/RelativesTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.relativesTypes.ToListAsync());
        }

        // GET: GeneralConfiguration/RelativesTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relativesType = await _context.relativesTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relativesType == null)
            {
                return NotFound();
            }

            return View(relativesType);
        }

        // GET: GeneralConfiguration/RelativesTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/RelativesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RelativeName,Notes")] RelativesType relativesType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relativesType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relativesType);
        }

        // GET: GeneralConfiguration/RelativesTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relativesType = await _context.relativesTypes.FindAsync(id);
            if (relativesType == null)
            {
                return NotFound();
            }
            return View(relativesType);
        }

        // POST: GeneralConfiguration/RelativesTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RelativeName,Notes")] RelativesType relativesType)
        {
            if (id != relativesType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relativesType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelativesTypeExists(relativesType.Id))
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
            return View(relativesType);
        }

        // GET: GeneralConfiguration/RelativesTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relativesType = await _context.relativesTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relativesType == null)
            {
                return NotFound();
            }

            return View(relativesType);
        }

        // POST: GeneralConfiguration/RelativesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relativesType = await _context.relativesTypes.FindAsync(id);
            if (relativesType != null)
            {
                _context.relativesTypes.Remove(relativesType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelativesTypeExists(int id)
        {
            return _context.relativesTypes.Any(e => e.Id == id);
        }
    }
}
