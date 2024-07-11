using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class SalariesController : Controller
    {
        private readonly AppDbContext _context;

        public SalariesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/Salaries
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Salaries.Include(s => s.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaries = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salaries == null)
            {
                return NotFound();
            }

            return View(salaries);
        }

        // GET: PayRoll/Salaries/Create
        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: PayRoll/Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Additinal,Salary,CurrencyId,allowances,SelectedMonth,Gratuities,Bonuses,Entitlements,Deductions,Another")] Salaries salaries,DateTime dateTime)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(salaries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", salaries.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", salaries.EmployeeId);
            return View(salaries);
        }
        public async Task<IActionResult> CalculateSelery(Employee employee,Salaries selectedMonth)
        {
            return View();
        }

        // GET: PayRoll/Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaries = await _context.Salaries.FindAsync(id);
            if (salaries == null)
            {
                return NotFound();
            }
            //ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", salaries.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", salaries.EmployeeId);
            return View(salaries);
        }

        // POST: PayRoll/Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Additinal,Salary,CurrencyId,allowances,SelectedMonth,Gratuities,Bonuses,Entitlements,Deductions,Another")] Salaries salaries)
        {
            if (id != salaries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salaries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalariesExists(salaries.Id))
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
            //ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", salaries.CurrencyId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", salaries.EmployeeId);
            return View(salaries);
        }

        // GET: PayRoll/Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaries = await _context.Salaries
                //.Include(s => s.Currency)
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salaries == null)
            {
                return NotFound();
            }

            return View(salaries);
        }

        // POST: PayRoll/Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salaries = await _context.Salaries.FindAsync(id);
            if (salaries != null)
            {
                _context.Salaries.Remove(salaries);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalariesExists(int id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }
    }
}
