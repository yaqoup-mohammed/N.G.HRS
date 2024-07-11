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
    public class NationalitiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Nationality> _nationalityRepository;

        public NationalitiesController(AppDbContext context, IRepository<Nationality> nationalityRepository )
        {
            _context = context;
            _nationalityRepository = nationalityRepository;
        }

        // GET: GeneralConfiguration/Nationalities
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.nationality.ToListAsync());
        }

        // GET: GeneralConfiguration/Nationalities/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationality = await _context.nationality
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nationality == null)
            {
                return NotFound();
            }

            return View(nationality);
        }

        // GET: GeneralConfiguration/Nationalities/Create
        [ Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Nationalities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,NationalityName,Notes")] Nationality nationality)
        {
            if (ModelState.IsValid)
            {
              await  _nationalityRepository.AddAsync(nationality);
                TempData["success"] = "تمت العملية بنجاح";

                return View(nationality); ;

            }
           
          
 
            return View(nationality);
        }

        // GET: GeneralConfiguration/Nationalities/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationality = await _nationalityRepository.GetByIdAsync(id);
            if (nationality == null)
            {
                return NotFound();
            }
            return View(nationality);
        }

        // POST: GeneralConfiguration/Nationalities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NationalityName,Notes")] Nationality nationality)
        {
            if (id != nationality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["Success"] = "تم التعديل بنجاح";

                try
                {
                    await _nationalityRepository.UpdateAsync(nationality);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!NationalityExists(nationality.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(nationality);
            }
            return View(nationality);
        }

        // GET: GeneralConfiguration/Nationalities/Delete/5
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationality = await _context.nationality
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nationality == null)
            {
                return NotFound();
            }

            return View(nationality);
        }

        // POST: GeneralConfiguration/Nationalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nationality = await _nationalityRepository.GetByIdAsync(id);
            if (nationality != null)
            {
                _context.nationality.Remove(nationality);
                TempData["Success"] = "تم الحذف بنجاح";

            }

            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Create));

        }

        private bool NationalityExists(int id)
        {
            return _context.nationality.Any(e => e.Id == id);
        }
    }
}
