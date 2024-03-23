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
    public class CountriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Country> _countryRepository;

        public CountriesController(AppDbContext context , IRepository<Country> countryRepository)
        {
            _context = context;
            _countryRepository = countryRepository;
        }

        // GET: GeneralConfiguration/Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.country.ToListAsync());
        }

        // GET: GeneralConfiguration/Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: GeneralConfiguration/Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {   

                    //var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(country.Data);
                    //foreach (var item in data)
                    //{
                        await _countryRepository.AddAsync(country);
                    //}
                    TempData["Success"] = "تم الحفظ بنجاح";

                //return RedirectToAction(nameof(Create));


                //if(country != null)
                //{
                    //var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(country.Data);
                    //foreach (var item in data)
                    //{
                        _context.AddAsync(country);
                        await _context.SaveChangesAsync();
                    //}
                    TempData["Success"] = "تم الحفظ بنجاح";
                //}
                //else
                //{
                //   TempData["Error"] = "لم تتم الإضافة، لم يتم إرسال بيانات الدول";
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: GeneralConfiguration/Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: GeneralConfiguration/Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _countryRepository.UpdateAsync(country);
                    TempData["Success"] = "تم التعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
                //return View(country);
            }
            return View(country);
        }

        // GET: GeneralConfiguration/Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: GeneralConfiguration/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country != null)
            {
                _context.country.Remove(country);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Index));
            //return RedirectToAction(nameof(Create));

        }

        private bool CountryExists(int id)
        {
            return _context.country.Any(e => e.Id == id);
        }
    }
}
