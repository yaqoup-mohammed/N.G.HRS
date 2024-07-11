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
    public class EmployeePerksController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeePerksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/EmployeePerks
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeePerks.Include(e => e.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/EmployeePerks/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePerks = await _context.EmployeePerks
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeePerks == null)
            {
                return NotFound();
            }

            return View(employeePerks);
        }

        // GET: PayRoll/EmployeePerks/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: PayRoll/EmployeePerks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Date,EmployeeId,Description,Amount,Percentage,Notes")] EmployeePerks employeePerks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeePerks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePerks.EmployeeId);
            return View(employeePerks);
        }

        // GET: PayRoll/EmployeePerks/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePerks = await _context.EmployeePerks.FindAsync(id);
            if (employeePerks == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePerks.EmployeeId);
            return View(employeePerks);
        }

        // POST: PayRoll/EmployeePerks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,EmployeeId,Description,Amount,Percentage,Notes")] EmployeePerks employeePerks)
        {
            if (id != employeePerks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeePerks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeePerksExists(employeePerks.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePerks.EmployeeId);
            return View(employeePerks);
        }

        // GET: PayRoll/EmployeePerks/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePerks = await _context.EmployeePerks
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeePerks == null)
            {
                return NotFound();
            }

            return View(employeePerks);
        }

        // POST: PayRoll/EmployeePerks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeePerks = await _context.EmployeePerks.FindAsync(id);
            if (employeePerks != null)
            {
                _context.EmployeePerks.Remove(employeePerks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeePerksExists(int id)
        {
            return _context.EmployeePerks.Any(e => e.Id == id);
        }
    }
}
