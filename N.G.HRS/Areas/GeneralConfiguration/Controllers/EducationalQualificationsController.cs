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
    public class EducationalQualificationsController : Controller
    {
        private readonly AppDbContext _context;

        public EducationalQualificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/EducationalQualifications
        public async Task<IActionResult> Index()
        {
            return View(await _context.educationalQualifications.ToListAsync());
        }

        // GET: GeneralConfiguration/EducationalQualifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalQualification = await _context.educationalQualifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalQualification == null)
            {
                return NotFound();
            }

            return View(educationalQualification);
        }

        // GET: GeneralConfiguration/EducationalQualifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/EducationalQualifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] EducationalQualification educationalQualification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationalQualification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationalQualification);
        }

        // GET: GeneralConfiguration/EducationalQualifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalQualification = await _context.educationalQualifications.FindAsync(id);
            if (educationalQualification == null)
            {
                return NotFound();
            }
            return View(educationalQualification);
        }

        // POST: GeneralConfiguration/EducationalQualifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] EducationalQualification educationalQualification)
        {
            if (id != educationalQualification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationalQualification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationalQualificationExists(educationalQualification.Id))
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
            return View(educationalQualification);
        }

        // GET: GeneralConfiguration/EducationalQualifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalQualification = await _context.educationalQualifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalQualification == null)
            {
                return NotFound();
            }

            return View(educationalQualification);
        }

        // POST: GeneralConfiguration/EducationalQualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationalQualification = await _context.educationalQualifications.FindAsync(id);
            if (educationalQualification != null)
            {
                _context.educationalQualifications.Remove(educationalQualification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationalQualificationExists(int id)
        {
            return _context.educationalQualifications.Any(e => e.Id == id);
        }
    }
}
