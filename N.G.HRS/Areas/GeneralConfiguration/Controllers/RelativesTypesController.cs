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
    public class RelativesTypesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<RelativesType> _RelativesTypeRepository;

        public RelativesTypesController(AppDbContext context, IRepository<RelativesType> RelativesTypeRepository)
        {
            _RelativesTypeRepository = RelativesTypeRepository;
            _context = context;
        }

        // GET: GeneralConfiguration/RelativesTypes
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.relativesTypes.ToListAsync());
        }

        // GET: GeneralConfiguration/RelativesTypes/Details/5
        [Authorize(Policy = "DetailsPolicy")]
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
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/RelativesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,RelativeName,Notes")] RelativesType relativesType)
        {
            if (ModelState.IsValid)
            {
                await _RelativesTypeRepository.AddAsync(relativesType);
                TempData ["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Create));
            }
            return View(relativesType);
        }

        // GET: GeneralConfiguration/RelativesTypes/Edit/5
        [ Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relativesType = await _RelativesTypeRepository.GetByIdAsync(id);
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
        [ Authorize(Policy = "EditPolicy")]
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
                    await _RelativesTypeRepository. UpdateAsync(relativesType);
                    await _context.SaveChangesAsync();
                    TempData ["Success"] = "تم التعديل بنجاح";
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
                //return RedirectToAction(nameof(Index));
                return View(relativesType);
            }
            return View(relativesType);
        }

        // GET: GeneralConfiguration/RelativesTypes/Delete/5
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relativesType = await _RelativesTypeRepository.GetByIdAsync(id);
            if (relativesType != null)
            {
                _context.relativesTypes.Remove(relativesType);
            }

            await _context.SaveChangesAsync();
            TempData ["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));
        }

        private bool RelativesTypeExists(int id)
        {
            return _context.relativesTypes.Any(e => e.Id == id);
        }
    }
}
