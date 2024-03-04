using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class AdditionalAccountInformationsController : Controller
    {
        private readonly AppDbContext _context;

        public AdditionalAccountInformationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SalariesAndWages/AdditionalAccountInformations
        public async Task<IActionResult> Index()
        {
            return View(await _context.additionalAccountInformation.ToListAsync());
        }

        // GET: SalariesAndWages/AdditionalAccountInformations/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalariesAndWages/AdditionalAccountInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NormalCoefficient,WeekendLaboratories,OfficialHolidaysLab,NightPeriodParameter,LaboratoriesPerDay,FromTime,ToTime,FromDate,ToDate,Notes")] AdditionalAccountInformation additionalAccountInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(additionalAccountInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(additionalAccountInformation);
        }

        // GET: SalariesAndWages/AdditionalAccountInformations/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,NormalCoefficient,WeekendLaboratories,OfficialHolidaysLab,NightPeriodParameter,LaboratoriesPerDay,FromTime,ToTime,FromDate,ToDate,Notes")] AdditionalAccountInformation additionalAccountInformation)
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
    }
}
