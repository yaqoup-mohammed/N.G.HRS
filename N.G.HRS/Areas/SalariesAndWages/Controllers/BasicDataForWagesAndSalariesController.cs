using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class BasicDataForWagesAndSalariesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<BasicDataForWagesAndSalaries> _BasicDataForWagesAndSalariesRepo;
        public BasicDataForWagesAndSalariesController(AppDbContext context, IRepository<BasicDataForWagesAndSalaries> BasicDataForWagesAndSalariesRepo)
        {
            _context = context;
            _BasicDataForWagesAndSalariesRepo = BasicDataForWagesAndSalariesRepo;
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _BasicDataForWagesAndSalariesRepo.GetAllAsync());
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForWagesAndSalaries = await _context.basicDataForWagesAndSalaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basicDataForWagesAndSalaries == null)
            {
                return NotFound();
            }

            return View(basicDataForWagesAndSalaries);
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: SalariesAndWages/BasicDataForWagesAndSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,NumberOfMonthsDays,AbsencePerHour,DelayPerHour,OneFingerPrintPerHourDelay,FromDate,ToDate,TypeOfWage,Notes")] BasicDataForWagesAndSalaries basicDataForWagesAndSalaries)
        {
            if (ModelState.IsValid)
            {
                await _BasicDataForWagesAndSalariesRepo.AddAsync(basicDataForWagesAndSalaries);
                return RedirectToAction(nameof(Index));
            }

            return View(basicDataForWagesAndSalaries);
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForWagesAndSalaries = await _BasicDataForWagesAndSalariesRepo.GetByIdAsync(id);
            if (basicDataForWagesAndSalaries == null)
            {
                return NotFound();
            }
            return View(basicDataForWagesAndSalaries);
        }

        // POST: SalariesAndWages/BasicDataForWagesAndSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberOfMonthsDays,AbsencePerHour,DelayPerHour,OneFingerPrintPerHourDelay,FromDate,ToDate,TypeOfWage,Notes")] BasicDataForWagesAndSalaries basicDataForWagesAndSalaries)
        {
            if (id != basicDataForWagesAndSalaries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _BasicDataForWagesAndSalariesRepo.UpdateAsync(basicDataForWagesAndSalaries);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasicDataForWagesAndSalariesExists(basicDataForWagesAndSalaries.Id))
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
            return View(basicDataForWagesAndSalaries);
        }

        // GET: SalariesAndWages/BasicDataForWagesAndSalaries/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForWagesAndSalaries = await _context.basicDataForWagesAndSalaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basicDataForWagesAndSalaries == null)
            {
                return NotFound();
            }

            return View(basicDataForWagesAndSalaries);
        }

        // POST: SalariesAndWages/BasicDataForWagesAndSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basicDataForWagesAndSalaries = await _BasicDataForWagesAndSalariesRepo.GetByIdAsync(id);
            if (basicDataForWagesAndSalaries != null)
            {
                await _BasicDataForWagesAndSalariesRepo.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BasicDataForWagesAndSalariesExists(int id)
        {
            return _context.basicDataForWagesAndSalaries.Any(e => e.Id == id);
        }

        public IActionResult Check(DateOnly from, DateOnly to, int month, int apcent, int late, int fapcent)
        {
            var date = _context.basicDataForWagesAndSalaries.ToList();
            if (date != null)
            {
                foreach (var item in date)
                {
                    if (item.FromDate == from && item.ToDate == to && item.NumberOfMonthsDays == month && item.AbsencePerHour == apcent && item.DelayPerHour == late && item.OneFingerPrintPerHourDelay == fapcent)
                    {
                        return Json(1);
                    }
                    else if (item.FromDate == from && item.ToDate == to)
                    {
                        return Json(2);
                    }
                    else if (item.FromDate <= from && item.ToDate >= from)
                    {
                        return Json(3);
                    }
                    else if (item.FromDate <= to && item.ToDate >= to)
                    {
                        return Json(4);
                    }
                    else if ((from <= item.ToDate && to >= item.FromDate))
                    {
                        return Json(5);
                    }
                }

            }
            return Json(0);
        }


    }
}
