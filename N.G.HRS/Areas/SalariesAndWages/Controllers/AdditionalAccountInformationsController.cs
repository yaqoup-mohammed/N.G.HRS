using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class AdditionalAccountInformationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<AdditionalAccountInformation> _additionalAccountInformation;
        public AdditionalAccountInformationsController(AppDbContext context, IRepository<AdditionalAccountInformation> additionalAccountInformation)
        {
            _context = context;
            _additionalAccountInformation = additionalAccountInformation;
      }

        // GET: SalariesAndWages/AdditionalAccountInformations
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.additionalAccountInformation.ToListAsync());
        }

        // GET: SalariesAndWages/AdditionalAccountInformations/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalAccountInformation = await _context.additionalAccountInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalAccountInformation == null)
            {
                return NotFound();
            }

            return View(additionalAccountInformation);
        }

        // GET: SalariesAndWages/AdditionalAccountInformations/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: SalariesAndWages/AdditionalAccountInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,NormalCoefficient,WeekendLaboratories,OfficialHolidaysLab,NightPeriodParameter,LaboratoriesPerDay,FromTime,ToTime,Date,FromDate,ToDate,Notes")] AdditionalAccountInformation additionalAccountInformation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //           await _additionalAccountInformation.AddAsync(additionalAccountInformation);
        //            await _context.SaveChangesAsync();
        //            TempData["Success"] = "تم الحفظ بنجاح";
        //            return RedirectToAction(nameof(Index));


        //        }

        //        catch (Exception ex)
        //        {
        //            TempData["error"] = " حدثت خطأ اثناء الادخال برجاء التواصل مع مدير النظام  " + ex.Message;
        //            return View(additionalAccountInformation);
        //        }
        //    }
        //    return View(additionalAccountInformation);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("NormalCoefficient,WeekendLaboratories,OfficialHolidaysLab,NightPeriodParameter,LaboratoriesPerDay,FromTime,ToTime,Date,FromDate,ToDate,Notes")] AdditionalAccountInformation additionalAccountInformation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(additionalAccountInformation);
                    await _context.SaveChangesAsync();

                    // التحقق من قيمة Id بعد الحفظ
                    Console.WriteLine("Saved record ID: " + additionalAccountInformation.Id);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = "حدثت خطأ أثناء الإدخال، برجاء التواصل مع مدير النظام. " + ex.Message;
                    // سجل الاستثناء لمزيد من التحقيق
                    Console.WriteLine("Error: " + ex.ToString());
                    return View(additionalAccountInformation);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(additionalAccountInformation);
        }


        // GET: SalariesAndWages/AdditionalAccountInformations/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalAccountInformation = await _context.additionalAccountInformation.FindAsync(id);
            if (additionalAccountInformation == null)
            {
                return NotFound();
            }
            return View(additionalAccountInformation);
        }

        // POST: SalariesAndWages/AdditionalAccountInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NormalCoefficient,WeekendLaboratories,OfficialHolidaysLab,NightPeriodParameter,LaboratoriesPerDay,FromTime,ToTime,Date,FromDate,ToDate,Notes")] AdditionalAccountInformation additionalAccountInformation)
        {
            if (id != additionalAccountInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalAccountInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalAccountInformationExists(additionalAccountInformation.Id))
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
            return View(additionalAccountInformation);
        }

        // GET: SalariesAndWages/AdditionalAccountInformations/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalAccountInformation = await _context.additionalAccountInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalAccountInformation == null)
            {
                return NotFound();
            }

            return View(additionalAccountInformation);
        }

        // POST: SalariesAndWages/AdditionalAccountInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var additionalAccountInformation = await _context.additionalAccountInformation.FindAsync(id);
            if (additionalAccountInformation != null)
            {
                _context.additionalAccountInformation.Remove(additionalAccountInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdditionalAccountInformationExists(int id)
        {
            return _context.additionalAccountInformation.Any(e => e.Id == id);
        }


        public IActionResult Check1(DateOnly from, DateOnly to)
        {
            var date = _context.additionalAccountInformation.ToList();
            if (date != null)
            {
                foreach (var item in date)
                {
                   
                    if (item.FromDate == from && item.ToDate == to)
                    {
                        return Json(1);
                    }
                    else if (item.FromDate <= from && item.ToDate >= from)
                    {
                        return Json(2);
                    }
                    else if (item.FromDate <= to && item.ToDate >= to)
                    {
                        return Json(3);
                    }
                    else if ((from <= item.ToDate && to >= item.FromDate))
                    {
                        return Json(4);
                    }
                }

            }
            return Json(0);
        }
    }
}
