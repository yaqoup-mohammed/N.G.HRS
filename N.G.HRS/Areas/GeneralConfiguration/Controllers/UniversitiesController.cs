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
    public class UniversitiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Universities> _universitiesRepository;

        public UniversitiesController(AppDbContext context, IRepository<Universities> universitiesRepository)
        {
            _context = context;
            _universitiesRepository = universitiesRepository;
        }

        // GET: GeneralConfiguration/Universities
        [Authorize (Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Universities.ToListAsync());
        }

        // GET: GeneralConfiguration/Universities/Details/5
        [Authorize(Policy = "DetailsPolicy")]
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
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Universities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Universities universities)
        {
            if (ModelState.IsValid)
            {
                  await _universitiesRepository.AddAsync(universities);
                TempData ["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Create));

                //return RedirectToAction(nameof(Index));
            }
            return View(universities);
        }

        // GET: GeneralConfiguration/Universities/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universities = await _universitiesRepository.GetByIdAsync(id);
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
        [Authorize(Policy = "EditPolicy")]
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
                     await _universitiesRepository.UpdateAsync(universities);
                    TempData["Success"] = "تم التعديل بنجاح";
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
                //return RedirectToAction(nameof(Index));
                return View(universities);
            }
            return View(universities);
        }

        // GET: GeneralConfiguration/Universities/Delete/5
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var universities = await _universitiesRepository.GetByIdAsync(id);
            if (universities != null)
            {
                _context.Universities.Remove(universities);
            }
            TempData["Success"] = "تم الحذف بنجاح";

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        private bool UniversitiesExists(int id)
        {
            return _context.Universities.Any(e => e.Id == id);
        }
    }
}
