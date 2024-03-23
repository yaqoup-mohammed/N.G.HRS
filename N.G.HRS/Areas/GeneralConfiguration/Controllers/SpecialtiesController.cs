using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class SpecialtiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Specialties> _specialtiesRepository;

        public SpecialtiesController(AppDbContext context, IRepository<Specialties> specialtiesRepository)
        {
            _context = context;
            _specialtiesRepository = specialtiesRepository;
        }

        // GET: GeneralConfiguration/Specialties
        public async Task<IActionResult> Index()
        {
            return View(await _context.specialties.ToListAsync());
        }

        // GET: GeneralConfiguration/Specialties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialties = await _context.specialties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialties == null)
            {
                return NotFound();
            }

            return View(specialties);
        }

        // GET: GeneralConfiguration/Specialties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Specialties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Specialties specialties)
        {
            if (ModelState.IsValid)
            {
               await _specialtiesRepository.AddAsync(specialties);
                TempData[("Success")] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Create));

                //return RedirectToAction(nameof(Index));

            }
            return View(specialties);
        }

        // GET: GeneralConfiguration/Specialties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialties = await _specialtiesRepository.GetByIdAsync(id);
            if (specialties == null)
            {
                return NotFound();
            }
            return View(specialties);
        }

        // POST: GeneralConfiguration/Specialties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Specialties specialties)
        {
            if (id != specialties.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _specialtiesRepository.UpdateAsync(specialties);
                    TempData [("Success")] = "تم التعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialtiesExists(specialties.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(specialties);
                //return RedirectToAction(nameof(Index));
            }
            return View(specialties);
        }

        // GET: GeneralConfiguration/Specialties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialties = await _context.specialties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialties == null)
            {
                return NotFound();
            }

            return View(specialties);
        }

        // POST: GeneralConfiguration/Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialties = await _specialtiesRepository.GetByIdAsync(id);
            if (specialties != null)
            {
                _context.specialties.Remove(specialties);
            }
            await _context.SaveChangesAsync();

            TempData[("Success")] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));
        }

        private bool SpecialtiesExists(int id)
        {
            return _context.specialties.Any(e => e.Id == id);
        }
    }
}
