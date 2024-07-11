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
    public class OfficialVacationsController : Controller
    {
        private readonly AppDbContext _context;
        private  readonly IRepository<OfficialVacations> _officialVacationsRepository; 

        public OfficialVacationsController(AppDbContext context , IRepository<OfficialVacations> officialVacationsRepository)
        {
            _context = context;
            _officialVacationsRepository = officialVacationsRepository;
        }

        // GET: GeneralConfiguration/OfficialVacations
        [Authorize (Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.officialVacations.ToListAsync());
        }

        // GET: GeneralConfiguration/OfficialVacations/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialVacations = await _context.officialVacations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officialVacations == null)
            {
                return NotFound();
            }

            return View(officialVacations);
        }

        // GET: GeneralConfiguration/OfficialVacations/Create
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/OfficialVacations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,VacationsName,FromDate,ToDate")] OfficialVacations officialVacations)
        {
            //if (ModelState.IsValid)
            //{
            //    await _officialVacationsRepository.AddAsync(officialVacations);

            //    await _context.SaveChangesAsync();
            //    TempData["Success"] = "تمت العملية بنجاح";

            //}
            if (ModelState.IsValid)
            {
                try
                {


                    if (officialVacations != null)
                    {
                        if (officialVacations.FromDate > officialVacations.ToDate)
                        {
                            TempData["Error"] = "يجب ان يكون تاريخ الانتهاء اكبر من تاريخ البدء";
                            return View(officialVacations);
                        }
                        await _officialVacationsRepository.AddAsync(officialVacations);
                        //================================================
                        TempData["Success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(Index));
                        //return RedirectToAction(nameof(Index));


                    }
                    else
                    {
                        TempData["Error"] = "لم تتم الإضافة، هناك خطأ";

                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it accordingly
                    TempData["Error"] = ex.Message;
                    TempData["Error"] = "حدث خطأ أثناء محاولة إضافة الموظف";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: GeneralConfiguration/OfficialVacations/Edit/5
        [ Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialVacations = await _officialVacationsRepository.GetByIdAsync(id);
            if (officialVacations == null)
            {
                return NotFound();
            }
            return View(officialVacations);
        }

        // POST: GeneralConfiguration/OfficialVacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VacationsName,FromDate,ToDate")] OfficialVacations officialVacations)
        {
            if (id != officialVacations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["Success"] = "تم التعديل بنجاح";

                try
                {
                    await    _officialVacationsRepository.UpdateAsync(officialVacations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficialVacationsExists(officialVacations.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(officialVacations)  ;
            }
            return View(officialVacations);
        }

        // GET: GeneralConfiguration/OfficialVacations/Delete/5
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officialVacations = await _context.officialVacations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officialVacations == null)
            {
                return NotFound();
            }

            return View(officialVacations);
        }

        // POST: GeneralConfiguration/OfficialVacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officialVacations = await _officialVacationsRepository.GetByIdAsync(id);
            if (officialVacations != null)
            {
                _context.officialVacations.Remove(officialVacations);
                TempData["Success"] = "تم الحذف بنجاح";

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        private bool OfficialVacationsExists(int id)
        {
            return _context.officialVacations.Any(e => e.Id == id);
        }
    }
}
