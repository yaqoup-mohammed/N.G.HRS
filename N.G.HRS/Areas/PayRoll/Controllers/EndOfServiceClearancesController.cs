using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class EndOfServiceClearancesController : Controller
    {
        private readonly AppDbContext _context;

        public EndOfServiceClearancesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/EndOfServiceClearances
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EndOfServiceClearance.Include(e => e.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/EndOfServiceClearances/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endOfServiceClearance = await _context.EndOfServiceClearance
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endOfServiceClearance == null)
            {
                return NotFound();
            }

            return View(endOfServiceClearance);
        }

        // GET: PayRoll/EndOfServiceClearances/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: PayRoll/EndOfServiceClearances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,Date,EndOfServiceDate,EmployeeId,ReasonForClearance,LastApprovedSalary,ServicePeriodPerYear,EndOfServiceBenefits,AdvancesAndLoans,VacationEntitlements,Absence,OtherEntitlements,OtherDiscounts,Total")] EndOfServiceClearance endOfServiceClearance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(endOfServiceClearance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", endOfServiceClearance.EmployeeId);
            return View(endOfServiceClearance);
        }

        // GET: PayRoll/EndOfServiceClearances/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endOfServiceClearance = await _context.EndOfServiceClearance.FindAsync(id);
            if (endOfServiceClearance == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", endOfServiceClearance.EmployeeId);
            return View(endOfServiceClearance);
        }

        // POST: PayRoll/EndOfServiceClearances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,EndOfServiceDate,EmployeeId,ReasonForClearance,LastApprovedSalary,ServicePeriodPerYear,EndOfServiceBenefits,AdvancesAndLoans,VacationEntitlements,Absence,OtherEntitlements,OtherDiscounts,Total")] EndOfServiceClearance endOfServiceClearance)
        {
            if (id != endOfServiceClearance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endOfServiceClearance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EndOfServiceClearanceExists(endOfServiceClearance.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", endOfServiceClearance.EmployeeId);
            return View(endOfServiceClearance);
        }

        // GET: PayRoll/EndOfServiceClearances/Delete/5
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endOfServiceClearance = await _context.EndOfServiceClearance
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endOfServiceClearance == null)
            {
                return NotFound();
            }

            return View(endOfServiceClearance);
        }

        // POST: PayRoll/EndOfServiceClearances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]


        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endOfServiceClearance = await _context.EndOfServiceClearance.FindAsync(id);
            if (endOfServiceClearance != null)
            {
                _context.EndOfServiceClearance.Remove(endOfServiceClearance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EndOfServiceClearanceExists(int id)
        {
            return _context.EndOfServiceClearance.Any(e => e.Id == id);
        }
        [HttpGet]
        // جمع السلف والقروض

        public IActionResult GetEmployeeLoansAndAdvances(int employeeId)
        {
            try
            {
                // Fetching loan amount from the database
                var loans = _context.EmployeeLoans
                                   .Where(l => l.EmployeeId == employeeId)
                                   .Sum(l => l.Amount);

                // Fetching advance amount from the database
                var advances = _context.EmployeeAdvances
                                      .Where(a => a.EmployeeId == employeeId)
                                      .Sum(a => a.Amount);

                return Json(new { success = true, loans = loans, advances = advances });
            }
            catch (Exception ex)
            {
                // Handling errors
                return Json(new { success = false, message = ex.Message });
            }
        }
    }


}



