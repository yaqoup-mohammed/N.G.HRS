using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class VacationAllowancesController : Controller
    {
        private readonly AppDbContext _context;

        public VacationAllowancesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/VacationAllowances
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.VacationAllowances.ToListAsync());
        }

        // GET: PayRoll/VacationAllowances/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationAllowances = await _context.VacationAllowances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacationAllowances == null)
            {
                return NotFound();
            }

            return View(vacationAllowances);
        }

        // GET: PayRoll/VacationAllowances/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: PayRoll/VacationAllowances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,Date,EmplyeeId,VacationBalance,Amount,CarryoverBalance,Notes")] VacationAllowances vacationAllowances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacationAllowances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacationAllowances);
        }

        // GET: PayRoll/VacationAllowances/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationAllowances = await _context.VacationAllowances.FindAsync(id);
            if (vacationAllowances == null)
            {
                return NotFound();
            }
            return View(vacationAllowances);
        }

        // POST: PayRoll/VacationAllowances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,EmplyeeId,VacationBalance,Amount,CarryoverBalance,Notes")] VacationAllowances vacationAllowances)
        {
            if (id != vacationAllowances.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationAllowances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationAllowancesExists(vacationAllowances.Id))
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
            return View(vacationAllowances);
        }

        // GET: PayRoll/VacationAllowances/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationAllowances = await _context.VacationAllowances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacationAllowances == null)
            {
                return NotFound();
            }

            return View(vacationAllowances);
        }

        // POST: PayRoll/VacationAllowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacationAllowances = await _context.VacationAllowances.FindAsync(id);
            if (vacationAllowances != null)
            {
                _context.VacationAllowances.Remove(vacationAllowances);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationAllowancesExists(int id)
        {
            return _context.VacationAllowances.Any(e => e.Id == id);
        }
    }
}
