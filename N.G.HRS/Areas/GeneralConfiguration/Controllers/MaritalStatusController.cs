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
    public class MaritalStatusController : Controller
    {
        private readonly AppDbContext _context;

        public MaritalStatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/MaritalStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.maritalStatuses.ToListAsync());
        }

        // GET: GeneralConfiguration/MaritalStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatus = await _context.maritalStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatus == null)
            {
                return NotFound();
            }

            return View(maritalStatus);
        }

        // GET: GeneralConfiguration/MaritalStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/MaritalStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] MaritalStatus maritalStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maritalStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maritalStatus);
        }

        // GET: GeneralConfiguration/MaritalStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatus = await _context.maritalStatuses.FindAsync(id);
            if (maritalStatus == null)
            {
                return NotFound();
            }
            return View(maritalStatus);
        }

        // POST: GeneralConfiguration/MaritalStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] MaritalStatus maritalStatus)
        {
            if (id != maritalStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maritalStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaritalStatusExists(maritalStatus.Id))
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
            return View(maritalStatus);
        }

        // GET: GeneralConfiguration/MaritalStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatus = await _context.maritalStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatus == null)
            {
                return NotFound();
            }

            return View(maritalStatus);
        }

        // POST: GeneralConfiguration/MaritalStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maritalStatus = await _context.maritalStatuses.FindAsync(id);
            if (maritalStatus != null)
            {
                _context.maritalStatuses.Remove(maritalStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaritalStatusExists(int id)
        {
            return _context.maritalStatuses.Any(e => e.Id == id);
        }
    }
}
