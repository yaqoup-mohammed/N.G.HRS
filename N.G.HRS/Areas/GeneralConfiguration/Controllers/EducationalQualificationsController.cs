using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class EducationalQualificationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<EducationalQualification> _educationalQualificationRepository;

        public EducationalQualificationsController(AppDbContext context, IRepository<EducationalQualification> educationalQualificationRepository)
        {
            _context = context;
            _educationalQualificationRepository = educationalQualificationRepository;
        }

        // GET: GeneralConfiguration/EducationalQualifications
        [Authorize (Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _educationalQualificationRepository.GetAllAsync());
        }

        // GET: GeneralConfiguration/EducationalQualifications/Details/5
        [Authorize(Policy = "DetailsPolicy")]
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

        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/EducationalQualifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] EducationalQualification educationalQualification)
        {
            if (ModelState.IsValid)
            {
                await _educationalQualificationRepository.AddAsync(educationalQualification);
                await Task.Delay(1000);

                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Index));
            }
            return View(educationalQualification);
        }

        // GET: GeneralConfiguration/EducationalQualifications/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalQualification = await _educationalQualificationRepository.GetByIdAsync(id);
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
        [Authorize(Policy = "EditPolicy")]
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
                   await _educationalQualificationRepository.UpdateAsync(educationalQualification);
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
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationalQualification = await _educationalQualificationRepository.GetByIdAsync(id);
            if (educationalQualification != null)
            {
                await _educationalQualificationRepository.DeleteAsync(id);
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
