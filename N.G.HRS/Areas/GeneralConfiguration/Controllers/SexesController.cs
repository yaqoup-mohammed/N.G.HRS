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
    public class SexesController : Controller
    {
        private readonly AppDbContext _context;

        public SexesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/Sexes
        public async Task<IActionResult> Index()
        {
            return View(await _context.sex.ToListAsync());
        }

        // GET: GeneralConfiguration/Sexes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sex = await _context.sex
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sex == null)
            {
                return NotFound();
            }

            return View(sex);
        }

        // GET: GeneralConfiguration/Sexes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Sexes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Sex sex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sex);
        }

        // GET: GeneralConfiguration/Sexes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sex = await _context.sex.FindAsync(id);
            if (sex == null)
            {
                return NotFound();
            }
            return View(sex);
        }

        // POST: GeneralConfiguration/Sexes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Sex sex)
        {
            if (id != sex.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SexExists(sex.Id))
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
            return View(sex);
        }

        // GET: GeneralConfiguration/Sexes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sex = await _context.sex
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sex == null)
            {
                return NotFound();
            }

            return View(sex);
        }

        // POST: GeneralConfiguration/Sexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sex = await _context.sex.FindAsync(id);
            if (sex != null)
            {
                _context.sex.Remove(sex);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SexExists(int id)
        {
            return _context.sex.Any(e => e.Id == id);
        }
    }
}
