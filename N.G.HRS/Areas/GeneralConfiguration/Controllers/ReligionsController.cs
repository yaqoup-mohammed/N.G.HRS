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
    public class ReligionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Religion> _religionsRepository;

        public ReligionsController(AppDbContext context, IRepository<Religion> religionsRepository)
        {
            _context = context;
            _religionsRepository = religionsRepository;
        }

        // GET: GeneralConfiguration/Religions
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.religion.ToListAsync());
        }

        // GET: GeneralConfiguration/Religions/Details/5
        [ Authorize(Policy = "DetailsPolicy")]
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
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Religions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Religion religion)
        {
            if (ModelState.IsValid)
            {
                await _religionsRepository.AddAsync(religion);
                TempData["Success"] = "تمت العملية بنجاح";

                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Create));
            }
            return View(religion);
        }

        // GET: GeneralConfiguration/Religions/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var religion = await _religionsRepository.GetByIdAsync(id);
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
        [Authorize(Policy = "EditPolicy")]
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
                     await _religionsRepository.UpdateAsync(religion);
                    TempData ["Success"] = "تمت تعديل  بنجاح";
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
                //return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(Create));
            }
            return View(religion);
        }

        // GET: GeneralConfiguration/Religions/Delete/5
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var religion = await _religionsRepository.GetByIdAsync(id);
            if (religion != null)
            {
                _context.religion.Remove(religion);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تمت الحذف بنجاح";
            return RedirectToAction(nameof(Create));
        }

        private bool ReligionExists(int id)
        {
            return _context.religion.Any(e => e.Id == id);
        }
    }
}
